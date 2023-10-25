using System;
using AllBeginningsMod.Common.Graphics;
using AllBeginningsMod.Utilities;
using AllBeginningsMod.Utilities.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.NPCs.Enemies;

public sealed class SunVampire : ModNPC
{
    private const int ExplodeAfter = 110;

    private bool killed;
    private float sinTimer;

    private Player Target => Main.player[NPC.target];
    private ref float ExplodeTimer => ref NPC.ai[0];

    public override void SetDefaults() {
        NPC.width = 36;
        NPC.height = 36;
        NPC.damage = 25;
        NPC.defense = 20;
        NPC.lifeMax = 80;
        NPC.value = 67f;
        NPC.netAlways = true;
        NPC.noTileCollide = true;
        NPC.aiStyle = -1;
        NPC.noTileCollide = true;
        NPC.noGravity = true;
        NPC.knockBackResist = 0.25f;

        NPC.HitSound = SoundID.NPCHit23;
    }

    public override void AI() {
        if (killed) {
            NPC.friendly = true;
            if (ExplodeTimer++ > ExplodeAfter) {
                Explode();
            }

            if (Main.rand.NextBool((int)((1 - ExplodeTimer / ExplodeAfter) * 10) + 3)) {
                Dust.NewDust(NPC.Center + Main.rand.NextVector2Unit() * 20 * Main.rand.NextFloat(), 0, 0, DustID.Stone, Main.rand.NextFloatDirection() * 5, -5 * Main.rand.NextFloat());
            }

            if (!Main.dedServ) {
                float lightAmount = ExplodeTimer / ExplodeAfter;
                Lighting.AddLight(NPC.Center, 0.5f * lightAmount, 0.2f * lightAmount, 0.05f * lightAmount);
            }
        }
        else {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active || NPC.DistanceSQ(Target.Center) > MathF.Pow(16 * 30, 2)) {
                NPC.TargetClosest();
            }
            else if (NPC.velocity.LengthSquared() < 0.8f) {
                NPC.velocity += NPC.Center.DirectionTo(Target.Center) * 0.05f;
            }

            float sinCurve = MathF.Sin(sinTimer);
            NPC.Center += 0.25f * sinCurve * Vector2.UnitY;
            //NPC.rotation += Math.Abs(sinCurve) * NPC.direction * 0.5f;
            sinTimer += 0.025f;
        }

        NPC.rotation = NPC.velocity.X * 0.1f;
        NPC.velocity *= 0.95f;
    }

    private void Explode() {
        if (!Main.dedServ) {
            IEntitySource source = NPC.GetSource_Death();

            float angle = Main.rand.NextFloatDirection();

            for (int i = 0; i < 3; i++) {
                Vector2 pos = NPC.Center + Vector2.UnitX.RotatedBy(angle) * Main.rand.NextFloat() * 25;
                Gore gore = Gore.NewGoreDirect(source, pos, pos.DirectionFrom(NPC.Center) * Main.rand.NextFloat(6, 9), Mod.Find<ModGore>("SunVampire_Gore" + i).Type);
                gore.position -= new Vector2(gore.Width, gore.Height) * 0.5f;

                angle += MathHelper.TwoPi / 3f;
            }

            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, NPC.Center);
            DustUtils.NewDustCircular(NPC.Center, 30, d => Main.rand.NextFromList(DustID.Smoke, DustID.TreasureSparkle, DustID.YellowTorch), 14, angle, (4, 13));
        }

        if (Main.netMode != NetmodeID.MultiplayerClient) {
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player player = Main.player[i];
                if (player is null || !player.active || !player.Hitbox.Intersects(NPC.Center, 64)) {
                    break;
                }

                player.Hurt(PlayerDeathReason.ByNPC(NPC.whoAmI), 40, MathF.Sign(player.Center.X - NPC.Center.X));
            }
        }

        NPC.active = false;
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
        Texture2D tex = TextureAssets.Npc[Type].Value;

        float explodeProgress = ExplodeTimer / ExplodeAfter;
        float progSQ = MathF.Pow(explodeProgress, 2);

        Vector2 position = NPC.Center - screenPos + Vector2.UnitX * Main.rand.NextFloatDirection() * 8f * progSQ * (Main.rand.NextBool(3) ? 1 : 0);
        Vector2 scale = Vector2.One * (1 + 0.25f * progSQ);
        float rotation = NPC.rotation + (ExplodeTimer > 0 ? Main.rand.NextFloatDirection() * 0.07f * progSQ : 0);

        spriteBatch.Draw(
            tex,
            position,
            null,
            drawColor,
            rotation,
            tex.Size() * 0.5f,
            scale,
            SpriteEffects.None,
            0
        );

        SpriteBatchSnapshot snapshot = spriteBatch.Capture();

        spriteBatch.End();
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

        float bloomScaling = 0.2f;
        Texture2D texBloom0 = ModContent.Request<Texture2D>(Texture + "_Glow", AssetRequestMode.ImmediateLoad).Value;
        spriteBatch.Draw(
            texBloom0,
            position,
            null,
            Color.White * progSQ * 1.1f,
            rotation,
            texBloom0.Size() * 0.5f,
            scale,
            SpriteEffects.None,
            0
        );

        Texture2D texBloom1 = ModContent.Request<Texture2D>(Texture + "_Bloom", AssetRequestMode.ImmediateLoad).Value;
        spriteBatch.Draw(
            texBloom1,
            position,
            null,
            Color.White * (progSQ * 2f - 1f) * 1.1f,
            rotation,
            texBloom1.Size() * 0.5f,
            scale * bloomScaling,
            SpriteEffects.None,
            0
        );

        spriteBatch.End();
        spriteBatch.Begin(snapshot);

        return false;
    }

    public override bool CheckDead() {
        killed = true;
        NPC.life = 1;

        return false;
    }

    public override bool? CanBeHitByItem(Player player, Item item) {
        return !killed;
    }

    public override bool? CanBeHitByProjectile(Projectile projectile) {
        return !killed;
    }
}
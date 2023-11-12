using AllBeginningsMod.Common.PrimitiveDrawing;
using AllBeginningsMod.Content.Buffs;
using AllBeginningsMod.Content.Dusts;
using AllBeginningsMod.Utilities;
using AllBeginningsMod.Utilities.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Linq;
using System.Transactions;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.NPCs.Enemies.Bosses.Bastroboy;

internal class BastroboyExplodingProjectile : ModProjectile
{
    public override string Texture => base.Texture + "_Star";

    private PrimitiveTrail trail;
    private float random;
    private bool runOnSpawn;
    private const int MaxTimeLeft = 120;
    private const int ExplosionFrames = 12;
    private int routingTimer = 0;
    private bool IsStar => Projectile.ai[1] == 0;
    private float Progress => 1f - (float)Projectile.timeLeft / MaxTimeLeft;
    public override void SetStaticDefaults() {
        ProjectileID.Sets.TrailCacheLength[Type] = 30;
        ProjectileID.Sets.TrailingMode[Type] = 2;
    }

    public override void SetDefaults() {
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.tileCollide = false;
        Projectile.Size = new Vector2(25, 25);
        Projectile.timeLeft = MaxTimeLeft;

        trail = new PrimitiveTrail(
            ProjectileID.Sets.TrailCacheLength[Type], 
            factor => 20f * Projectile.scale * (factor < 0.1f ? -MathF.Pow(factor * 10f - 1f, 2) + 1 : 1.1f - factor), 
            static factor => Color.Lerp(Color.DeepPink, Color.LightBlue, factor.X) * (1f - factor.X) * 0.55f
        );
    }

    public override void AI() {
        if (!runOnSpawn) {
            runOnSpawn = true;
            for (int i = 0; i < Projectile.oldPos.Length; i++) {
                Projectile.oldPos[i] = Projectile.Center;
            }

            if (Projectile.ai[0] != -1) {
                Player player = Main.player[(int)Projectile.ai[0]];
                Projectile.velocity = -Projectile.Center.DirectionTo(player.Center);
            }

            random = Main.rand.NextFloat();
        }

        if (Projectile.ai[0] != -1 && routingTimer++ < 55) {
            Player player = Main.player[(int)Projectile.ai[0]];
            Projectile.velocity = Utils.AngleLerp(Projectile.velocity.ToRotation(), Projectile.Center.AngleTo(player.Center), 0.07f).ToRotationVector2() * 11f;
        }

        Projectile.rotation += 0.1f;
        if (Projectile.scale < 1f) {
            Projectile.scale += 0.5f;
        }

        if (Projectile.timeLeft >= ExplosionFrames) {
            Projectile.scale = 1.5f * (-MathF.Pow(Progress - 1f, 2) + 1f);
        }

        trail.Update(Projectile.oldPos.Select(position => position + Projectile.Size / 2f).ToArray());

        if (!Main.dedServ) {
            Lighting.AddLight(Projectile.Center, Color.LightBlue.ToVector3() * 0.8f);
        }

        if (Main.GameUpdateCount % 35 == 0) {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.MagicMirror);
        }
    }

    public override void OnKill(int timeLeft) {
        if (!Main.dedServ) {
            for (int i = 0; i < 9; i++) {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.MagicMirror);
                dust.velocity = (dust.position - Projectile.Center) * 0.2f;
            }

            Vector2[] dustPositions = Projectile.Center.PositionsAround(8, 5, out Vector2[] directions);
            for (int i = 0; i < dustPositions.Length; i++) {
                Dust.NewDustPerfect(
                    dustPositions[i],
                    ModContent.DustType<SmokeDust>(),
                    directions[i] * Main.rand.NextFloat(0.5f, 7f),
                    Main.rand.Next(20, 50),
                    Color.BlueViolet,
                    Main.rand.NextFloat(0.5f, 1.4f)
                );
            }

            //SoundEngine.PlaySound(SoundID.Thunder, Projectile.Center);
        }

        if (Main.netMode != NetmodeID.MultiplayerClient) {
            TargetingUtils.ForEachPlayerInRange(
                Projectile.Center,
                100f,
                player => {
                    player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, Projectile.whoAmI), Projectile.damage, MathF.Sign(player.Center.X - Projectile.Center.X));
                    OnHitPlayer(player, default);
                }
            );
        }
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info) {
        Projectile.penetrate--;
        if (IsStar) {
            //target.AddBuff(ModContent.BuffType<NoFlightDebuff>(), 400);
        } else {
            target.AddBuff(BuffID.WitheredWeapon, 400);
        }

        BastroboyNPC bastroboy = Main.npc.FirstOrDefault(npc => npc is not null && npc.type == ModContent.NPCType<BastroboyNPC>())?.ModNPC as BastroboyNPC ?? null;
        if (bastroboy is null) {
            return;
        }

        bastroboy.HitWithExplodingProjectile = true;
    }

    Effect effect;
    public override bool PreDraw(ref Color lightColor) {
        Texture2D texture = IsStar ? TextureAssets.Projectile[Type].Value 
            : ModContent.Request<Texture2D>(Texture.Replace("Star", "Crescent"), ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        Color color = Color.Lerp(Color.DeepPink, Color.LightBlue, (MathF.Sin(Main.GameUpdateCount * 0.03f + random * 2.541f) + 1f) / 2f);
        float squish = 0.2f * MathF.Sin(Main.GameUpdateCount * 0.25f + random);
        Vector2 scale = Projectile.scale * new Vector2(1f + squish, 1f - squish);
        effect ??= Mod.Assets.Request<Effect>("Assets/Effects/OutlinedTrailShader", AssetRequestMode.ImmediateLoad).Value;

        Main.spriteBatch.End();

        effect.Parameters["sampleTexture"].SetValue(TextureAssets.MagicPixel.Value);
        effect.Parameters["outlineWidth"].SetValue(0.05f);
        effect.Parameters["transformationMatrix"].SetValue(
            Matrix.CreateTranslation(-Main.screenPosition.X, -Main.screenPosition.Y, 0f)
            * Main.GameViewMatrix.TransformationMatrix
            * Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, -1, 1)
        );

        trail.Draw(effect);

        SpriteBatchSnapshot snapshot = Main.spriteBatch.Capture();
        Main.spriteBatch.Begin(default, BlendState.Additive, default, default, default);

        for (int i = 0; i < Projectile.oldPos.Length; i++) {
            Main.spriteBatch.Draw(
                texture,
                Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition,
                null,
                color * (1f - (float)i / Projectile.oldPos.Length) * 0.15f,
                Projectile.rotation,
                texture.Size() / 2f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        Main.spriteBatch.End();
        Main.spriteBatch.Begin(snapshot);

        Main.spriteBatch.Draw(
            texture,
            Projectile.Center + Main.rand.NextVector2Unit() * Progress * 2f - Main.screenPosition,
            null,
            color,
            Projectile.rotation,
            texture.Size() / 2f,
            scale,
            SpriteEffects.None,
            0f
        );

        return false;
    }
}

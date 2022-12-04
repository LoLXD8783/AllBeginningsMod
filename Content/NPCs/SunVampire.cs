using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using ReLogic.Content;
using AllBeginningsMod.Common.Graphics;
using AllBeginningsMod.Utilities;
using AllBeginningsMod.Utilities.Extensions;
using Terraria.DataStructures;

namespace AllBeginningsMod.Content.NPCs
{
    public class SunVampire : ModNPC
    {
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

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
        }

        private Player Target => Main.player[NPC.target];
        private float sinTimer;
        private ref float ExplodeTimer => ref NPC.ai[0];
        private const int EXPLODE_AFTER = 110;
        public override void AI() {
            if (killed) {
                if (ExplodeTimer++ > EXPLODE_AFTER) {
                    Explode();
                }

                if (Main.rand.NextBool((int)((1 - ExplodeTimer / EXPLODE_AFTER) * 10) + 3)) {
                    Dust.NewDust(NPC.Center + Main.rand.NextVector2Unit() * 20 * Main.rand.NextFloat(), 0, 0, DustID.Stone, Main.rand.NextFloatDirection() * 5, -5 * Main.rand.NextFloat());
                }

                if (!Main.dedServ) {
                    float lightAmount = ExplodeTimer / EXPLODE_AFTER;
                    Lighting.AddLight(NPC.Center, 0.5f * lightAmount, 0.2f * lightAmount, 0.05f * lightAmount);
                }
            }
            else {
                if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active || NPC.DistanceSQ(Target.Center) > MathF.Pow(16 * 45, 2)) {
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
                    Gore gore = Gore.NewGoreDirect(source, pos, pos.DirectionFrom(NPC.Center) * Main.rand.NextFloat(3, 9), Mod.Find<ModGore>("SunVampire_Gore" + i).Type);
                    gore.position -= new Vector2(gore.Width, gore.Height) * 0.5f;

                    angle += MathHelper.TwoPi / 3f;
                }

                DustUtils.NewDustCircular(NPC.Center, 30, d => Main.rand.NextFromList(DustID.Smoke, DustID.TreasureSparkle, DustID.YellowTorch), 14, angle, (4, 13));
            }

            NPC.active = false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
            Texture2D tex = TextureAssets.Npc[Type].Value;

            float explodeProgress = ExplodeTimer / EXPLODE_AFTER;
            float progSQ = MathF.Pow(explodeProgress, 2);

            Vector2 position = NPC.Center - screenPos + Main.rand.NextVector2Unit() * 3.5f * progSQ * (Main.rand.NextBool(3) ? 1 : 0);
            Vector2 scale = Vector2.One * (1 + 0.25f * progSQ);
            float rotation = NPC.rotation + (ExplodeTimer > 0 ? Main.rand.NextFloatDirection() * 0.25f * progSQ : 0);

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
            Texture2D texBloom0 = ModContent.Request<Texture2D>(Texture + "_Bloom0", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw(
                texBloom0,
                position,
                null,
                Color.White * progSQ * 1.1f,
                rotation,
                texBloom0.Size() * 0.5f,
                scale * bloomScaling,
                SpriteEffects.None,
                0
                );

            Texture2D texBloom1 = ModContent.Request<Texture2D>(Texture + "_Bloom1", AssetRequestMode.ImmediateLoad).Value;
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

        private bool killed;
        public override bool CheckDead() {
            killed = true;
            NPC.life = 1;
            return false;
        }
        public override bool? CanBeHitByItem(Player player, Item item) => !killed;
        public override bool? CanBeHitByProjectile(Projectile projectile) => !killed;
    }
}

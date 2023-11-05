using AllBeginningsMod.Common.Bases.NPCs;
using AllBeginningsMod.Utilities.Extensions;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using AllBeginningsMod.Content.Dusts;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.NPCs.Enemies
{
    internal class CaveVampire : VampireNPC
    {
        protected override void PostSetDefaults() {
            NPC.width = NPC.height = 45;
        }

        protected override void FollowBehaviour(Player target) {
            if (NPC.velocity.LengthSquared() > 1.8f) {
                return;
            }

            NPC.velocity += 0.08f * NPC.Center.DirectionTo(target.Center);
            NPC.direction = MathF.Sign(target.Center.X - NPC.Center.X);
        }

        protected override void ExplosionEffects() {
            IEntitySource source = NPC.GetSource_Death();
            Vector2[] dustPositions = NPC.Center.PositionsAround(12, 1f, out Vector2[] dustDirections);
            for (int i = 0; i < dustPositions.Length; i++) {
                Dust.NewDustPerfect(
                    dustPositions[i],
                    Main.rand.NextFromList(DustID.Smoke, DustID.TreasureSparkle, DustID.YellowTorch),
                    dustDirections[i] * Main.rand.NextFloat(3f, 7f)
                );

                if (i % 2 == 0) {
                    Dust.NewDustPerfect(
                        dustPositions[i],
                        ModContent.DustType<SmokeDust>(),
                        dustDirections[i] * Main.rand.NextFloat(0.4f, 8f),
                        Main.rand.Next(30, 50),
                        Color.Lerp(Main.rand.NextFromList(Color.SlateGray, Color.DimGray), Color.Black, Main.rand.NextFloat(0.2f)),
                        Main.rand.NextFloat(0.3f, 1.4f)
                    );
                }
            }

            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, NPC.Center);
        }

        protected override void Draw(SpriteBatch spriteBatch, Color drawColor, float explodingProgress) {
            Texture2D texture = TextureAssets.Npc[Type].Value;

            explodingProgress *= explodingProgress;

            Vector2 position = NPC.Center - Main.screenPosition;
            if (explodingProgress != 0f && Main.GameUpdateCount % (int)((1f - explodingProgress) * 10f + 1f) == 0) {
                position += Main.rand.NextVector2Unit() * explodingProgress * 3f;
            }

            Vector2 scale = Vector2.One * (1f + 0.25f * explodingProgress);
            float rotation = NPC.rotation + Main.rand.NextFloatDirection() * 0.09f * explodingProgress;

            spriteBatch.Draw(
                texture,
                position,
                null,
                drawColor,
                rotation,
                texture.Size() * 0.5f,
                scale,
                NPC.direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0
            );
        }
    }
}

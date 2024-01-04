using AllBeginningsMod.Content.Dusts;
using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles
{
    internal class ExplosionVFXProjectile : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_0";
        public static void Spawn(IEntitySource source, Vector2 position, Color flashColor, Color glowColor, Func<float, Color> smokeColor, int size, int timeLeft) {
            ExplosionVFXProjectile projectile = (ExplosionVFXProjectile)Projectile.NewProjectileDirect(
                source,
                position,
                Vector2.Zero,
                ModContent.ProjectileType<ExplosionVFXProjectile>(),
                0,
                0f
            ).ModProjectile;

            projectile.Projectile.timeLeft = projectile.maxTimeLeft = timeLeft;
            projectile.Projectile.width = projectile.Projectile.height = size;
            projectile.Projectile.Center = position;
            projectile.flashColor = flashColor;
            projectile.glowColor = glowColor;
            projectile.smokeColor = smokeColor;
            projectile.offsetRotation = Main.rand.NextFloatDirection() * MathHelper.PiOver4;
        }

        private int maxTimeLeft;
        private float offsetRotation;
        private Color flashColor;
        private Color glowColor;
        private Func<float, Color> smokeColor;
        private float Progress => 1f - (float)Projectile.timeLeft / maxTimeLeft;

        public override void SetDefaults() {
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.hide = true;
        }

        private bool runAIOnSpawn = true;
        public override void AI() {
            if (runAIOnSpawn) {
                for (int i = 0; i < Projectile.width * 0.2f; i++) {
                    Dust.NewDust(
                        Projectile.position,
                        Projectile.width,
                        Projectile.height,
                        Main.rand.NextFromList(DustID.Smoke, DustID.Torch),
                        Scale: Main.rand.NextFloat(1f, 1.5f)
                    );
                }

                if (!Main.dedServ) {
                    Lighting.AddLight(Projectile.Center, 0.05f * Projectile.width * flashColor.ToVector3());
                }

                runAIOnSpawn = false;
            }

            Projectile.velocity.Y -= 0.005f;
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI) {
            overWiresUI.Add(index);
        }

        private Texture2D glowTexture;
        private Texture2D flareTexture;
        private Texture2D smokeTexture;
        private Texture2D noiseTexture1;
        private Texture2D noiseTexture2;
        public override bool PreDraw(ref Color lightColor) {
            glowTexture ??= Mod.Assets.Request<Texture2D>("Assets/Images/Sample/Glow1", AssetRequestMode.ImmediateLoad).Value;
            flareTexture ??= Mod.Assets.Request<Texture2D>("Assets/Images/Sample/Flare1", AssetRequestMode.ImmediateLoad).Value;
            smokeTexture ??= Mod.Assets.Request<Texture2D>("Assets/Images/Sample/SmokeGlow", AssetRequestMode.ImmediateLoad).Value;
            noiseTexture1 ??= Mod.Assets.Request<Texture2D>("Assets/Images/Sample/PerlinNoise", AssetRequestMode.ImmediateLoad).Value;
            noiseTexture2 ??= Mod.Assets.Request<Texture2D>("Assets/Images/Sample/Noise2", AssetRequestMode.ImmediateLoad).Value;

            float flashScale = 1f - MathF.Min(0.07f, Progress) / 0.07f;

            Main.spriteBatch.End(out SpriteBatchSnapshot snapshot);

            Effect effect = Mod.Assets.Request<Effect>("Assets/Effects/ExplosionSmoke", AssetRequestMode.ImmediateLoad).Value;
            effect.Parameters["progress"].SetValue(Progress);
            effect.Parameters["time"].SetValue(Main.GameUpdateCount * 0.002f + offsetRotation);

            effect.Parameters["noiseTexture1"].SetValue(noiseTexture1);
            effect.Parameters["noiseScale1"].SetValue(0.2f);
            effect.Parameters["smokeCutSmoothness"].SetValue(0.4f);

            effect.Parameters["noiseTexture2"].SetValue(noiseTexture2);
            effect.Parameters["noiseScale2"].SetValue(0.3f);
            effect.Parameters["edgeColor"].SetValue(Color.Black.ToVector4());
            
            Main.spriteBatch.Begin(snapshot with { Effect = effect });
            Main.spriteBatch.Draw(
                smokeTexture,
                Projectile.Hitbox.Modified((int)-Main.screenPosition.X, (int)-Main.screenPosition.Y, 0, 0),
                smokeColor(Progress)
            );
            Main.spriteBatch.End();

            Main.spriteBatch.Begin(snapshot with { BlendState = BlendState.Additive });

            Main.spriteBatch.Draw(
                glowTexture,
                Projectile.Center - Main.screenPosition,
                null,
                glowColor * 0.4f,
                offsetRotation * 2.3f,
                glowTexture.Size() / 2f,
                0.02f * Projectile.width * flashScale * Main.rand.NextFloat(1.5f),
                SpriteEffects.None,
                0f
            );

            Main.spriteBatch.Draw(
                flareTexture,
                Projectile.Center - Main.screenPosition + Main.rand.NextVector2Unit() * 5f,
                null,
                flashColor * 0.65f,
                offsetRotation,
                flareTexture.Size() / 2f,
                0.02f * Projectile.width * flashScale * Main.rand.NextFloat(1.5f),
                SpriteEffects.None,
                0f
            );
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(snapshot);
            return false;
        }
    }
}

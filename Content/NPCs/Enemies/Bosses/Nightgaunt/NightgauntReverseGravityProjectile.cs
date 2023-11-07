using AllBeginningsMod.Common.Graphics;
using AllBeginningsMod.Utilities.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.NPCs.Enemies.Bosses.Nightgaunt
{
    internal class NightgauntReverseGravityProjectile : ModProjectile
    {
        public override void SetStaticDefaults() {
            ProjectileID.Sets.TrailCacheLength[Type] = 10;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults() {
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(25, 45);
            Projectile.timeLeft = 400;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
        }

        bool runAIOnSpawn;
        float rotationOffset;
        public override void AI() {
            if (!runAIOnSpawn) {
                runAIOnSpawn = true;
                rotationOffset = Main.rand.NextFloat(MathHelper.PiOver2);
            }

            Projectile.velocity.Y -= 0.08f;
            if (Projectile.timeLeft > 60) {
                if (Projectile.alpha > 0) {
                    Projectile.alpha -= 2;
                }
            } else {
                Projectile.alpha = 255 - (int)(255f * Projectile.timeLeft / 60f);
            }

            Projectile.frameCounter++;
            if (Projectile.frameCounter > 5) {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 5) {
                    Projectile.frame = 0;
                }
            }
            Projectile.rotation = MathF.Sin(Projectile.timeLeft * 0.05f + rotationOffset) * 0.2f;
            Projectile.position += Projectile.velocity.RotatedBy(Projectile.rotation);
        }

        public override bool ShouldUpdatePosition() {
            return false;
        }

        private Effect effect;
        public override bool PreDraw(ref Color lightColor) {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Texture2D colorMaskTexture = ModContent.Request<Texture2D>(Texture + "_ColorMask", AssetRequestMode.ImmediateLoad).Value;
            Texture2D glowTexture = Mod.Assets.Request<Texture2D>("Assets/Images/Glow2", AssetRequestMode.ImmediateLoad).Value;
            Rectangle source = new(0, Projectile.frame * 76, 50, 76);
            Color color = new(185, 140, 183);
            float alpha = 1f - Projectile.alpha / 255f;

            for (int i = 0; i < Projectile.oldPos.Length; i++) {
                float factor = (float)i / Projectile.oldPos.Length;
                Main.spriteBatch.Draw(
                    colorMaskTexture,
                    Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition + Vector2.UnitY * factor * 10f,
                    source,
                    Color.Black * alpha * (1f - factor) * 0.045f,
                    Projectile.oldRot[i],
                    source.Size() / 2f,
                    Projectile.scale,
                    SpriteEffects.None,
                    0f
                );
            }

            Main.spriteBatch.Draw(
                texture,
                Projectile.Center - Main.screenPosition,
                source,
                Color.White * alpha,
                Projectile.rotation,
                source.Size() / 2f,
                Projectile.scale,
                SpriteEffects.None,
                0f
            );

            SpriteBatchSnapshot snapshot = Main.spriteBatch.Capture();
            Main.spriteBatch.End();

            effect ??= Mod.Assets.Request<Effect>("Assets/Effects/JellyfishYe", AssetRequestMode.ImmediateLoad).Value;
            effect.Parameters["time"].SetValue(Projectile.timeLeft * 0.05f + rotationOffset * 4f);
            effect.Parameters["textureSize"].SetValue(colorMaskTexture.Size());
            effect.Parameters["sourceRectangle"].SetValue(new Vector4(source.X, source.Y, source.Width, source.Height));
            Main.spriteBatch.Begin(snapshot with { Effect = effect });

            Main.spriteBatch.Draw(
                colorMaskTexture,
                Projectile.Center - Main.screenPosition,
                source,
                color * alpha,
                Projectile.rotation,
                source.Size() / 2f,
                Projectile.scale,
                SpriteEffects.None,
                0f
            );

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Additive);

            Main.spriteBatch.Draw(
                glowTexture,
                Projectile.Center - Main.screenPosition - Vector2.UnitY * 20f,
                null,
                color * alpha * 0.5f,
                Projectile.rotation,
                glowTexture.Size() / 2f,
                Projectile.scale,
                SpriteEffects.None,
                0f

            );

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(snapshot);

            return false;
        }
    }
}

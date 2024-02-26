using AllBeginningsMod.Common.Loaders;
using AllBeginningsMod.Common.PrimitiveDrawing;
using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.NPCs.Enemies.Bosses.Bastroboy
{
    internal class BastroboyPhantomProjectile : ModProjectile
    {
        private int Style => (int)Projectile.ai[0];

        private PrimitiveTrail trail;
        private Texture2D trailTexture;
        private Effect effect;
        public override void SetStaticDefaults() {
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            Main.projFrames[Type] = 6;
        }

        public override void SetDefaults() {
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(25, 25);
            Projectile.timeLeft = 500;

            trail = new PrimitiveTrail(
                ProjectileID.Sets.TrailCacheLength[Type],
                factor => 20f,
                factor => GetAlpha(Lighting.GetColor(Projectile.Center.ToTileCoordinates())).Value
            );
        }

        public override void AI() {
            Projectile.rotation = Projectile.velocity.ToRotation();
            trail.Positions = Projectile.oldPos.Select(position => position + Projectile.Size / 2f).ToArray();

            Helper.ClosestPlayer(Projectile.Center, out Player player);
            Projectile.velocity += Projectile.Center.DirectionTo(player.Center) * 0.8f;
            Projectile.velocity *= 0.9f;

            int frameTime = 10;
            Projectile.frame = (Projectile.frameCounter / frameTime) + (Style == 1 ? 3 : 0);
            if (++Projectile.frameCounter >= 3 * frameTime) {
                Projectile.frameCounter = 0;
            }
        }

        public override Color? GetAlpha(Color lightColor) {
            return lightColor;
        }

        public override bool PreDraw(ref Color lightColor) {
            effect ??= EffectLoader.GetEffect("Trail::Default");
            trailTexture ??= ModContent.Request<Texture2D>(Texture + "_Trail", AssetRequestMode.ImmediateLoad).Value;

            effect.Parameters["sampleTexture"].SetValue(trailTexture);
            effect.Parameters["transformationMatrix"].SetValue(
                Matrix.CreateTranslation(-Main.screenPosition.X, -Main.screenPosition.Y, 0f)
                * Main.GameViewMatrix.TransformationMatrix
                * Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, -1, 1)
            );

            trail.Draw(effect);

            Texture2D texture = ModContent.Request<Texture2D>(Texture, AssetRequestMode.ImmediateLoad).Value;
            Rectangle source = Projectile.SourceRectangle();
            Main.spriteBatch.Draw(
                texture,
                Projectile.Center - Main.screenPosition,
                source,
                GetAlpha(lightColor).Value,
                Projectile.rotation + MathHelper.PiOver2,
                source.Size() / 2f,
                Projectile.scale,
                SpriteEffects.None,
                0f
            );
            return false;
        }
    }
}

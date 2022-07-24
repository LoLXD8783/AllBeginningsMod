using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace AllBeginningsMod.Utility
{
    public static class ProjectileUtils
    {
        public static void DrawAfterimage(Projectile projectile, Color color, Vector2 origin = default, float initialOpacity = 0.8f, float opacityDegrade = 0.1f, int stepSize = 1, Texture2D texture = null) {
            SpriteEffects effects = projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            texture ??= TextureAssets.Projectile[projectile.type].Value;

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i += stepSize) {
                Vector2 position = projectile.oldPos[i] - Main.screenPosition + origin + new Vector2(0f, projectile.gfxOffY);
                float alpha = initialOpacity - opacityDegrade * (i / stepSize);
                Main.EntitySpriteDraw(texture, position, null, projectile.GetAlpha(color) * alpha, projectile.oldRot[i], origin, projectile.scale, effects, 0);
            }
        }
    }
}
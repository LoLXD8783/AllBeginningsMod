using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace AllBeginningsMod.Utility.Extensions;

public static class ProjectileExtensions
{
    public static void DrawAfterimage(
        this Projectile projectile,
        Color color,
        Vector2 origin = default,
        float initialOpacity = 0.8f,
        float opacityDegrade = 0.1f,
        int stepSize = 1,
        Texture2D texture = null
    ) {
        texture ??= TextureAssets.Projectile[projectile.type].Value;
        
        SpriteEffects effects = projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        Rectangle frame = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);

        for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i += stepSize) {
            Vector2 position = projectile.oldPos[i] - Main.screenPosition + origin + new Vector2(0f, projectile.gfxOffY);
            float alpha = initialOpacity - opacityDegrade * (i / (float) stepSize);

            Main.EntitySpriteDraw(texture, position, frame, projectile.GetAlpha(color) * alpha, projectile.oldRot[i], origin, projectile.scale, effects, 0);
        }
    }

    public static void DrawProjectile(this Projectile projectile, Color color, Vector2 origin = default, Texture2D texture = null) {
        texture ??= TextureAssets.Projectile[projectile.type].Value;

        SpriteEffects effects = projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        Vector2 position = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
        Rectangle frame = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);

        Main.EntitySpriteDraw(texture, position, frame, projectile.GetAlpha(color), projectile.rotation, origin, projectile.scale, effects, 0);
    }
}
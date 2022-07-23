using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public sealed class LeekShieldReturningProjectile : ModProjectile
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Leek Shield");

            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 10;
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.penetrate = -1;

            Projectile.aiStyle = ProjAIStyleID.Boomerang;
            AIType = ProjectileID.WoodenBoomerang;
        }

        public override bool PreDraw(ref Color lightColor) {
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = Projectile.Hitbox.Size() / 2f;

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type]; i += 2) {
                Vector2 position = Projectile.oldPos[i] - Main.screenPosition + origin + new Vector2(0f, Projectile.gfxOffY);
                float alpha = 0.8f - 0.2f * (i / 2f);
                Main.EntitySpriteDraw(texture, position, null, lightColor * alpha, Projectile.oldRot[i], origin, Projectile.scale, effects, 0);
            }

            return true;
        }
    }
}
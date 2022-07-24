using AllBeginningsMod.Utility;
using Microsoft.Xna.Framework;
using Terraria;
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
            ProjectileUtils.DrawAfterimage(Projectile, lightColor, Projectile.Hitbox.Size() / 2f, 0.8f, 0.1f, 2);
            return true;
        }
    }
}
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Projectiles
{
    public abstract class HeldProjectileBase : ModProjectile
    {
        protected Player Owner => Main.player[Projectile.owner];

        public override void AI() {
            Owner.heldProj = Projectile.whoAmI;

            Projectile.Center = Owner.Center;
        }
    }
}
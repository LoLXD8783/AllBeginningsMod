using AllBeginningsMod.Common.Bases.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public sealed class LeekSwordProjectile : HeldProjectileBase
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Leek Sword");
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.tileCollide = false;

            Projectile.width = 46;
            Projectile.height = 48;

            Projectile.timeLeft = 20;

            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            AIType = -1;
        }

        public override void AI() {
            base.AI();

            Owner.armorEffectDrawShadow = true;
            Owner.immune = true;
            Owner.immuneTime = Projectile.timeLeft;

            Owner.velocity = Owner.DirectionTo(Main.MouseWorld) * Projectile.timeLeft * 2f;

            if (Projectile.timeLeft == 20) {
                Projectile.rotation = Owner.AngleTo(Main.MouseWorld) + MathHelper.ToRadians(135f);

                if (Projectile.spriteDirection == 1) {
                    Projectile.rotation -= MathHelper.PiOver2;
                }
            }

            Projectile.Center = Owner.Center;
        }

        public override void Kill(int timeLeft) {
            Owner.velocity = Vector2.Zero;
        }
    }
}
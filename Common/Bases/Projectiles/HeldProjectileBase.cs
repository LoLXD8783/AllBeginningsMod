using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Projectiles
{
    public abstract class HeldProjectileBase : ModProjectile
    {
        protected Player Owner => Main.player[Projectile.owner];

        protected Vector2 HoldoutOffset;
        protected bool ChannelDependant;
        protected bool ChangeOwnerDirection;

        public override void AI() {
            if (ChannelDependant && !Owner.channel) {
                Projectile.Kill();
                return;
            }

            if (ChangeOwnerDirection) {
                int direction = Math.Sign(Main.MouseWorld.X - Owner.Center.X);
                Owner.ChangeDir(direction);
            }

            Owner.heldProj = Projectile.whoAmI;
            Owner.itemTime = 2;
            Owner.itemAnimation = 2;
            Owner.itemRotation = Projectile.rotation;

            Projectile.Center = Owner.RotatedRelativePoint(Owner.Center) + HoldoutOffset;
            Projectile.direction = Owner.direction;
            Projectile.spriteDirection = Owner.direction;
        }
    }
}
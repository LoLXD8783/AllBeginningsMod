using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Projectiles
{
    public abstract class HeldProjectileBase : ModProjectile
    {
        public Player Owner => Main.player[Projectile.owner];

        public bool ChannelDependant;
        public bool ChangeOwnerItemData;
        public bool ChangeOwnerDirection;
        public bool StickToOwnerCenter = true;
        public Vector2 HoldoutOffset;

        public override void AI() {
            if (ChannelDependant && !Owner.channel) {
                Projectile.Kill();
                return;
            }

            if (ChangeOwnerDirection) {
                int direction = Math.Sign(Main.MouseWorld.X - Owner.Center.X);
                Owner.ChangeDir(direction);
            }

            if (ChangeOwnerItemData) {
                Owner.itemTime = 2;
                Owner.itemAnimation = 2;
                Owner.itemRotation = Projectile.rotation;
            }

            if (StickToOwnerCenter) {
                Projectile.Center = Owner.MountedCenter + HoldoutOffset;
            }

            Owner.heldProj = Projectile.whoAmI;

            Projectile.direction = Owner.direction;
            Projectile.spriteDirection = Owner.direction;
        }
    }
}
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Projectiles;

public abstract class HeldProjectileBase : ModProjectileBase
{
    public bool ChangeOwnerDirection;
    public bool ChangeOwnerItemData;

    public bool ChannelDependant;

    public Vector2 HoldoutOffset;
    public Player Owner => Main.player[Projectile.owner];

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

        Owner.heldProj = Projectile.whoAmI;

        Projectile.Center = Owner.MountedCenter + HoldoutOffset;
        Projectile.direction = Owner.direction;
        Projectile.spriteDirection = Owner.direction;
    }
}
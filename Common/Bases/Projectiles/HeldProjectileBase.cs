using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Common.Bases.Projectiles;

public abstract class HeldProjectileBase : ModProjectileBase
{
    /// <summary>
    /// Represents whether this projectile sets its owner's direction based on the mouse position or not.
    /// </summary>
    public bool ChangeOwnerDirection;
    
    /// <summary>
    /// Represents whether this projectile sets its owner's item animation data or not.
    /// </summary>
    public bool ChangeOwnerItemData;

    /// <summary>
    /// Represents whether this projectile remains active if its owner is channeling or not.
    /// </summary>
    public bool ChannelDependant;

    /// <summary>
    /// Represents the hold offset of this projectile from the owner's center.
    /// </summary>
    public Vector2 HoldoutOffset;

    public override bool PreAI() {
        return CheckActive();
    }

    public override void AI() {
        Owner.heldProj = Projectile.whoAmI;

        Projectile.Center = Owner.MountedCenter + HoldoutOffset;
        Projectile.direction = Owner.direction;
        Projectile.spriteDirection = Owner.direction;
        
        if (ChangeOwnerDirection) {
            int direction = Math.Sign(Main.MouseWorld.X - Owner.Center.X);
            Owner.ChangeDir(direction);
        }

        if (ChangeOwnerItemData) {
            Owner.itemTime = 2;
            Owner.itemAnimation = 2;
            Owner.itemRotation = Projectile.rotation;
        }
    }

    /// <summary>
    /// Represents whether this projectile meets all conditions for staying active or not.
    /// </summary>
    /// <returns></returns>
    protected bool CheckActive() {
        bool active = ChannelDependant && !Owner.channel;
        
        if (!active)
            Projectile.Kill();
        
        return active;
    }
}
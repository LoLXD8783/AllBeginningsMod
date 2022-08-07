using System;
using System.Collections.Generic;
using AllBeginningsMod.Common.Graphics.Primitives;
using AllBeginningsMod.Common.Graphics.Snapshots;
using AllBeginningsMod.Utility;
using AllBeginningsMod.Utility.Extensions;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Projectiles;

public abstract class GreatswordProjectileBase : ModProjectileBase
{
    public override string Texture => base.Texture.Replace("/Projectiles/", "/Items/Weapons/").Replace("GreatswordProjectile", "GreatswordItem");

    protected Player Player => Main.player[Projectile.owner];

    private readonly Func<float, float> easeOut = value => MathF.Log10(9f * value + 1f);

    private readonly Func<float, float> easeIn = value => MathF.Pow(value, 3); //x^3 or x^6?

    private readonly Func<float, float> cooldownSmoothCurve = value => MathF.Sin(MathHelper.Pi * value / 2f);
    
    private Vector2 unitVectorToMouse;
    
    private float shiftedRotation;
    private float transitionAngle;
    
    private int direction;
    private int oldDirection;
    
    private int associatedItemType = -1;

    // Variables to tweak motion -> abstract because I don't want to give it 'default' values.

    /// <summary>
    ///     The maximum upper angle (in radians) the arm will bend backwards when holding the projectile above its head.
    /// </summary>
    protected abstract float ChargeUpBehindHeadAngle { get; }

    /// <summary>
    ///     The swing arc in radians. The swing starts wherever <see cref="ChargeUpBehindHeadAngle" /> is defined at.
    /// </summary>
    protected abstract float SwingArc { get; }

    /// <summary>
    ///     How far should the projectile be from the player.
    /// </summary>
    protected abstract int HoldingRadius { get; }

    /// <summary>
    ///     The point which the texture should rotate around.
    /// </summary>
    protected abstract Vector2 RotationOrigin { get; }

    /// <summary>
    ///     How long the charge up animation will last.
    /// </summary>
    protected abstract int MaxChargeTimer { get; }

    /// <summary>
    ///     How long the attack animation will last.
    /// </summary>
    protected abstract int MaxAttackTimer { get; }

    /// <summary>
    ///     How long the cooldown animation will last.
    /// </summary>
    protected abstract int MaxCooldownTimer { get; }

    /// <summary>
    ///     How long the smooth animation will last.
    /// </summary>
    protected abstract int MaxSmoothTimer { get; }

    protected State CurrentState {
        get => (State) Projectile.ai[0];
        set => Projectile.ai[0] = (float) value;
    }

    public override void SetDefaults() {
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.aiStyle = -1;
    }

    protected float Timer {
        get => Projectile.ai[1];
        set => Projectile.ai[1] = value;
    }

    public override bool PreAI() {
        return !TryKillProjectile();
    }

    public override void AI() {
        Player.heldProj = Projectile.whoAmI;
        Projectile.active = true;

        // TODO: Change timers to reflect relevant player multipliers such as attack speed, damage..

        float rotationFix = MathHelper.PiOver2 * direction;
        float angleFix = direction == -1 ? MathHelper.Pi : 0f;

        switch (CurrentState) {
            case State.Holding:
                // Unit vector from player's center to mouse rotated by 90deg (rotationFix) so that (0,-1) is 0deg.
                unitVectorToMouse = Vector2.Lerp(unitVectorToMouse, Player.MountedCenter.DirectionTo(Main.MouseWorld).RotatedBy(rotationFix).SafeNormalize(Vector2.UnitY), 0.25f);
                shiftedRotation = unitVectorToMouse.ToRotation();
                FixDirection();
                break;

            case State.ChargingUp:
                // Angle between holding position and the target ChargeUpBehindHeadAngle value.
                if (Timer == 0) 
                    transitionAngle = -ChargeUpBehindHeadAngle * direction - MathHelper.WrapAngle(shiftedRotation + rotationFix + angleFix);

                float chargeProgress = easeOut(Timer / MaxChargeTimer);
                shiftedRotation = unitVectorToMouse.ToRotation() + transitionAngle * chargeProgress;

                if (Timer++ >= MaxChargeTimer) {
                    CurrentState = State.Attacking;
                    Timer = 0;

                    // Setting the fixed direction vector to match the rotation of the last frame of the charge up animation.
                    unitVectorToMouse = shiftedRotation.ToRotationVector2();
                }

                break;

            case State.Attacking:
                float attackProgress = easeIn(Timer / MaxAttackTimer);
                shiftedRotation = unitVectorToMouse.ToRotation() + SwingArc * attackProgress * direction;

                if (Timer++ >= MaxAttackTimer) {
                    CurrentState = State.Cooldown;
                    Timer = 0;

                    // Setting the fixed direction vector to match the rotation of the last frame of the charge up animation.
                    unitVectorToMouse = shiftedRotation.ToRotationVector2();
                }

                break;

            case State.Cooldown:
                float cooldownProgress = cooldownSmoothCurve(Timer / MaxCooldownTimer);
                shiftedRotation = unitVectorToMouse.ToRotation() + MathHelper.ToRadians(5) * cooldownProgress * direction;

                if (Timer++ >= MaxCooldownTimer) {
                    CurrentState = State.Holding;
                    Timer = 0;

                    FixDirection();
                    unitVectorToMouse = Player.MountedCenter.DirectionTo(Main.MouseWorld).RotatedBy(rotationFix).SafeNormalize(Vector2.UnitY);
                    shiftedRotation = unitVectorToMouse.ToRotation();
                }

                break;
        }

        // Revert rotation shift before drawing.
        shiftedRotation -= rotationFix;

        // Arm rotation.
        float frontArmRotation = shiftedRotation - MathHelper.PiOver2 * direction + angleFix;
        float backArmRotation = shiftedRotation - MathHelper.PiOver4 * direction + angleFix;

        Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, frontArmRotation);
        Player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.ThreeQuarters, backArmRotation);

        // Torso position and rotation?

        // Projectile position and rotation.
        Vector2 armRotationOrigin = new(-4f * direction, -2f);
        Projectile.Center = Player.MountedCenter + armRotationOrigin + shiftedRotation.ToRotationVector2() * HoldingRadius;

        // Projectile rotation is rotated by 45deg to compensate for the 45deg tilt the sprite has.
        Projectile.rotation = shiftedRotation - MathHelper.PiOver4 * direction + angleFix;

        void FixDirection() {
            direction = (Main.MouseWorld.X >= Player.Center.X).ToDirectionInt();

            Player.ChangeDir(direction);
            
            if (direction != oldDirection) {
                oldDirection = direction;

                rotationFix = MathHelper.PiOver2 * direction;
                angleFix = direction == -1 ? MathHelper.Pi : 0f;

                unitVectorToMouse = Player.MountedCenter.DirectionTo(Main.MouseWorld).RotatedBy(rotationFix).SafeNormalize(Vector2.UnitY);
                shiftedRotation = unitVectorToMouse.ToRotation();
            }
        }
    }
    
    public override bool PreDraw(ref Color lightColor) {
        // Making sure movement keys don't take precedence over our desired direction for the player's direction.
        Player.ChangeDir(direction);

        SpriteEffects spriteEffects = SpriteEffects.None;
        if (direction == -1)
            spriteEffects = SpriteEffects.FlipHorizontally;

        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        Rectangle frame = new(0, 0, texture.Width, texture.Height);
        Color color = Projectile.GetAlpha(lightColor);

        Vector2 rotationOrigin = new(direction == 1 ? RotationOrigin.X : Projectile.width - RotationOrigin.X, RotationOrigin.Y);
        
        // I'm not sure why this is required? Maybe some bias to the left side.
        if (direction == 1)
            rotationOrigin += Vector2.One;

        Main.EntitySpriteDraw(
            texture,
            Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
            frame,
            color,
            Projectile.rotation,
            rotationOrigin,
            Projectile.scale,
            spriteEffects,
            0
        );

        return false;
    }

    public void TryAttacking() {
        if (CurrentState == State.Holding)
            CurrentState = State.ChargingUp;
    }

    private bool TryKillProjectile() {
        if ((Player.HeldItem.type != associatedItemType && Projectile.active) ||
            !Player.active ||
            Player.dead ||
            Player.noItems ||
            Player.CCed) {
            Projectile.Kill();
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Item type required by the player to be held to not despawn the projectile
    /// </summary>
    public void SetAssociatedItemType(int itemType) {
        associatedItemType = itemType;
    }

    protected enum State
    {
        Holding,
        ChargingUp,
        Attacking,
        Cooldown
    }
}
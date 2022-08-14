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
using Terraria.Audio;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Projectiles;

// TODO: Documentation
public abstract class GreatswordProjectileBase : HeldProjectileBase
{
    public const float HoldingState = 0f;
    public const float ChargingUpState = 1f;
    public const float AttackingState = 2f;
    public const float CooldownState = 3f;

    public ref float CurrentState => ref Projectile.ai[0];

    public ref float Timer => ref Projectile.ai[1];

    public abstract float ChargeUpBehindHeadAngle { get; }

    public abstract float SwingArc { get; }

    public abstract int HoldingRadius { get; }

    public abstract Vector2 RotationOrigin { get; }

    public abstract int MaxChargeTimer { get; }
    public abstract int MaxAttackTimer { get; }
    public abstract int MaxCooldownTimer { get; }
    public abstract int MaxSmoothTimer { get; }

    private static readonly SoundStyle swingSound = new($"{AllBeginningsMod.ModName}/Assets/Sounds/Item/GreatswordSwing") {
        PitchVariance = 0.5f
    };

    // TODO: Setup for public use, other content sets could benefit from easing as well.
    private readonly Func<float, float> easeOut = value => MathF.Log10(9f * value + 1f);

    private readonly Func<float, float> easeIn = value => MathF.Pow(value, 3);

    private readonly Func<float, float> cooldownSmoothCurve = value => MathF.Sin(MathHelper.Pi * value / 2f);

    private Vector2 unitVectorToMouse;

    private float shiftedRotation;
    private float transitionAngle;

    private int direction;
    private int oldDirection;

    private int associatedItemType = -1;

    public override string Texture => base.Texture.Replace("/Projectiles/", "/Items/Weapons/").Replace("GreatswordProjectile", "GreatswordItem");

    public override void SetDefaults() {
        Projectile.tileCollide = false;

        Projectile.penetrate = -1;
        Projectile.aiStyle = -1;
    }

    public override void ModifyDamageHitbox(ref Rectangle hitbox) {
        if (CurrentState != AttackingState)
            return;

        float offset = Projectile.direction == -1 ? 0f : MathHelper.PiOver2;

        hitbox.X += (int) (MathF.Cos(Projectile.rotation + offset) * Projectile.width);
        hitbox.Y += (int) (MathF.Sin(Projectile.rotation + offset) * Projectile.width);
    }

    public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) {
        hitDirection = direction;
    }

    public override bool PreAI() {
        return !TryKillProjectile();
    }

    public override void AI() {
        // TODO: Change timers to reflect relevant player multipliers such as attack speed, damage..

        Projectile.friendly = CurrentState == AttackingState;

        float rotationFix = MathHelper.PiOver2 * direction;
        float angleFix = direction == -1 ? MathHelper.Pi : 0f;

        float attackSpeed = Owner.GetAttackSpeed(DamageClass.Melee);

        switch (CurrentState) {
            case HoldingState:
                // Unit vector from player's center to mouse rotated by 90deg (rotationFix) so that (0,-1) is 0deg.
                unitVectorToMouse = Vector2.Lerp(unitVectorToMouse, Owner.MountedCenter.DirectionTo(Main.MouseWorld).RotatedBy(rotationFix).SafeNormalize(Vector2.UnitY), 0.25f);
                shiftedRotation = unitVectorToMouse.ToRotation();

                break;

            case ChargingUpState:
                // Angle between holding position and the target ChargeUpBehindHeadAngle value.
                if (Timer == 0f)
                    transitionAngle = -ChargeUpBehindHeadAngle * direction - MathHelper.WrapAngle(shiftedRotation + rotationFix + angleFix);

                float chargeProgress = easeOut(Timer / MaxChargeTimer);
                shiftedRotation = unitVectorToMouse.ToRotation() + transitionAngle * chargeProgress;

                Timer++;

                if (Timer >= MaxChargeTimer) {
                    CurrentState = AttackingState;
                    Timer = 0f;

                    // Setting the fixed direction vector to match the rotation of the last frame of the charge up animation.
                    unitVectorToMouse = shiftedRotation.ToRotationVector2();

                    SoundEngine.PlaySound(swingSound, Projectile.Center);
                }

                break;

            case AttackingState:
                float attackProgress = easeIn(Timer / MaxAttackTimer);
                shiftedRotation = unitVectorToMouse.ToRotation() + SwingArc * attackProgress * direction;

                Timer++;

                if (Timer >= MaxAttackTimer) {
                    CurrentState = CooldownState;
                    Timer = 0f;

                    // Setting the fixed direction vector to match the rotation of the last frame of the charge up animation.
                    unitVectorToMouse = shiftedRotation.ToRotationVector2();
                }

                break;

            case CooldownState:
                float cooldownProgress = cooldownSmoothCurve(Timer / MaxCooldownTimer);
                shiftedRotation = unitVectorToMouse.ToRotation() + MathHelper.ToRadians(5f) * cooldownProgress * direction;

                Timer++;

                if (Timer >= MaxCooldownTimer) {
                    CurrentState = HoldingState;
                    Timer = 0f;

                    unitVectorToMouse = Owner.MountedCenter.DirectionTo(Main.MouseWorld).RotatedBy(rotationFix).SafeNormalize(Vector2.UnitY);
                    shiftedRotation = unitVectorToMouse.ToRotation();
                }

                break;
        }

        // Revert rotation shift before drawing.
        shiftedRotation -= rotationFix;

        // Arm rotation.
        float frontArmRotation = shiftedRotation - MathHelper.PiOver2 * direction + angleFix;
        float backArmRotation = shiftedRotation - MathHelper.PiOver4 * direction + angleFix;

        Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, frontArmRotation);
        Owner.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.ThreeQuarters, backArmRotation);

        // Projectile position and rotation.
        Vector2 armRotationOrigin = new(-4f * direction, -2f);
        Vector2 offset = armRotationOrigin + shiftedRotation.ToRotationVector2() * HoldingRadius;
        HoldoutOffset = armRotationOrigin + shiftedRotation.ToRotationVector2() * HoldingRadius;

        // Projectile rotation is rotated by 45deg to compensate for the 45deg tilt the sprite has.
        Projectile.rotation = shiftedRotation - MathHelper.PiOver4 * direction + angleFix;

        direction = (Main.MouseWorld.X >= Owner.Center.X).ToDirectionInt();

        Owner.ChangeDir(direction);

        if (direction != oldDirection) {
            oldDirection = direction;

            rotationFix = MathHelper.PiOver2 * direction;
            angleFix = direction == -1 ? MathHelper.Pi : 0f;

            unitVectorToMouse = Owner.MountedCenter.DirectionTo(Main.MouseWorld).RotatedBy(rotationFix).SafeNormalize(Vector2.UnitY);
            shiftedRotation = unitVectorToMouse.ToRotation();
        }

        base.AI();
    }

    public override bool PreDraw(ref Color lightColor) {
        SpriteEffects spriteEffects = direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

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

    public void SetAssociatedItemType(int itemType) {
        associatedItemType = itemType;
    }

    public void TryAttacking() {
        if (CurrentState == HoldingState)
            CurrentState = ChargingUpState;
    }

    private bool TryKillProjectile() {
        if ((Owner.HeldItem.type != associatedItemType && Projectile.active) ||
            !Owner.active ||
            Owner.dead ||
            Owner.noItems ||
            Owner.CCed) {
            Projectile.Kill();
            return true;
        }

        return false;
    }
}
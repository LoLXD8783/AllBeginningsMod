using System;
using AllBeginningsMod.Common.Graphics.Primitives;
using AllBeginningsMod.Utility;
using AllBeginningsMod.Utility.Extensions;
using IL.Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Projectiles;

public abstract class GreatswordProjectileBase : ModProjectileBase
{
    public override string Texture => GetType().FullName.GetAssetPath().Replace("/Projectiles/", "/Items/Weapons/").Replace("GreatswordProjectile", "GreatswordItem");

    private readonly Func<float, float> EaseOut = value => (float) Math.Log10(9f * value + 1);

    // private readonly Func<float, float> EaseIn = value => (float) Math.Exp(0.7f * Math.Pow(value, 6)) - 1f;
    private readonly Func<float, float> EaseIn = value => (float) Math.Pow(value, 3); //x^3 or x^6?

    private readonly Func<float, float> CooldownSmoothCurve = value => (float) Math.Sin(Math.PI * value / 2f);

    private int associatedItemType = -1;
    private Vector2 unitVectorToMouse;
    private float shiftedRotation = 0f;
    private float transitionAngle = 0f;
    private int direction;
    private int oldDirection = 0;

    protected Player player => Main.player[Projectile.owner];

    //Variables to tweak motion -> abstract because I don't want to give it 'default' values

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
        player.heldProj = Projectile.whoAmI;
        Projectile.active = true;

        //TODO: Change timers to reflect relevant player multipliers such as attack speed, damage..

        float rotationFix = MathHelper.PiOver2 * direction;
        float angleFix = direction == -1 ? MathHelper.Pi : 0f;

        switch (CurrentState) {
            case State.Holding:
                //Unit vector from player's center to mouse rotated by 90deg (rotationFix) so that (0,-1) is 0deg
                unitVectorToMouse = Vector2.Lerp(unitVectorToMouse, player.MountedCenter.DirectionTo(Main.MouseWorld).RotatedBy(rotationFix).SafeNormalize(Vector2.UnitY), 0.25f);
                shiftedRotation = unitVectorToMouse.ToRotation();
                FixDirection();
                break;

            case State.ChargingUp:
                if (Timer == 0) //Angle between holding position and the target ChargeUpBehindHeadAngle value
                    transitionAngle = -ChargeUpBehindHeadAngle * direction - MathHelper.WrapAngle(shiftedRotation + rotationFix + angleFix);

                float chargeProgress = EaseOut(Timer / MaxChargeTimer);
                shiftedRotation = unitVectorToMouse.ToRotation() + transitionAngle * chargeProgress;

                if (Timer++ >= MaxChargeTimer) {
                    CurrentState = State.Attacking;
                    Timer = 0;

                    //Setting the fixed direction vector to match the rotation of the last frame of the charge up animation
                    unitVectorToMouse = shiftedRotation.ToRotationVector2();
                }

                break;

            case State.Attacking:
                float attackProgress = EaseIn(Timer / MaxAttackTimer);
                shiftedRotation = unitVectorToMouse.ToRotation() + SwingArc * attackProgress * direction;

                if (Timer++ >= MaxAttackTimer) {
                    CurrentState = State.Cooldown;
                    Timer = 0;

                    //Setting the fixed direction vector to match the rotation of the last frame of the charge up animation
                    unitVectorToMouse = shiftedRotation.ToRotationVector2();
                }

                break;

            case State.Cooldown:
                float cooldownProgress = CooldownSmoothCurve(Timer / MaxCooldownTimer);
                shiftedRotation = unitVectorToMouse.ToRotation() + MathHelper.ToRadians(5) * cooldownProgress * direction;

                if (Timer++ >= MaxCooldownTimer) {
                    CurrentState = State.Holding;
                    Timer = 0;

                    FixDirection();
                    unitVectorToMouse = player.MountedCenter.DirectionTo(Main.MouseWorld).RotatedBy(rotationFix).SafeNormalize(Vector2.UnitY);
                    shiftedRotation = unitVectorToMouse.ToRotation();
                }

                break;
        }

        //Revert rotation shift before drawing
        shiftedRotation -= rotationFix;

        //Arm rotation
        float frontArmRotation = shiftedRotation - MathHelper.PiOver2 * direction + angleFix;
        float backArmRotation = shiftedRotation - MathHelper.PiOver4 * direction + angleFix;

        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, frontArmRotation);
        player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.ThreeQuarters, backArmRotation);

        //Torso position and rotation?

        //Projectile position and rotation
        Vector2 armRotationOrigin = new(-4f * direction, -2f);
        Projectile.Center = player.MountedCenter + armRotationOrigin + shiftedRotation.ToRotationVector2() * HoldingRadius;

        //Projectile rotation is rotated by 45deg to compensate for the 45deg tilt the sprite has
        Projectile.rotation = shiftedRotation - MathHelper.PiOver4 * direction + angleFix;

        void FixDirection() {
            direction = (Main.MouseWorld.X >= player.Center.X).ToDirectionInt();

            player.ChangeDir(direction);
            if (direction != oldDirection) {
                oldDirection = direction;

                rotationFix = MathHelper.PiOver2 * direction;
                angleFix = direction == -1 ? MathHelper.Pi : 0f;

                unitVectorToMouse = player.MountedCenter.DirectionTo(Main.MouseWorld).RotatedBy(rotationFix).SafeNormalize(Vector2.UnitY);
                shiftedRotation = unitVectorToMouse.ToRotation();
            }
        }
    }
    
    public override bool PreDraw(ref Color lightColor) {
        // Arc slash drawing
        TriangleShape shape = new(Projectile.Center, Color.White, 64f, 64f);

        Effect effect = Mod.Assets.Request<Effect>("Assets/Effects/TexturePrimitive", AssetRequestMode.ImmediateLoad).Value;
        
        effect.Parameters["worldViewProjection"].SetValue(DrawUtils.WorldViewProjection);
        effect.Parameters["sampleTexture"].SetValue(Mod.Assets.Request<Texture2D>("Assets/Extras/Samples/GreatswordSlash", AssetRequestMode.ImmediateLoad).Value);
        
        PrimitiveDrawing.DrawPrimitiveShape(shape, effect);
        
        //Making sure movement keys don't take precedence over our desired direction for the player's direction
        player.ChangeDir(direction);

        SpriteEffects spriteEffects = SpriteEffects.None;
        if (direction == -1)
            spriteEffects = SpriteEffects.FlipHorizontally;

        Asset<Texture2D> texture = ModContent.Request<Texture2D>(Texture);
        Rectangle sourceRect = new(0, 0, texture.Width(), texture.Height());
        Color color = Projectile.GetAlpha(lightColor);

        Vector2 rotationOrigin = new(direction == 1 ? RotationOrigin.X : Projectile.width - RotationOrigin.X, RotationOrigin.Y);
        //I'm not sure why this is required? Maybe some bias to the left side
        if (direction == 1)
            rotationOrigin += Vector2.One;

        Main.EntitySpriteDraw(
            texture.Value,
            Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
            sourceRect,
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
        if ((player.HeldItem.type != associatedItemType && Projectile.active) ||
            !player.active ||
            player.dead ||
            player.noItems ||
            player.CCed) {
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
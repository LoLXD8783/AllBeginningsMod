using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Projectiles.Melee;

public abstract class BaseSwingableGreatswordProjectile : ModProjectile
{
    protected Player player => Main.player[Projectile.owner];
    
    //Variables to tweak motion
    protected float ChargeUpBehindHeadAngle { get; set; } = MathHelper.Pi / 6f; //30deg
    protected float HoldingAngleArmDown { get; set; } = MathHelper.Pi / 12f; //15deg
    protected float SwingArc { get; set; } = 4 * MathHelper.Pi / 3f; //240deg
    protected int MaxChargeTimer { get; set; } = 45;
    protected int MaxAttackTimer { get; set; } = 15;
    protected int MaxCooldownTimer { get; set; } = 15;
    protected int HoldingRadius { get; set; } = 14;

    //From end to beginning
    private Func<float, float> InvertedEaseIn =
        value => (float) Math.Cos(1.5f * Math.Pow(value, 2));
    
    private Func<float, float> EaseOut = 
        value => (float) Math.Log(2 * value + 1);

    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.aiStyle = -1;
    }

    public void DoAttack()
    {
        if (CurrentState == State.Holding)
            CurrentState = State.ChargingUp;
    }

    protected enum State : int
    {
        Holding,
        ChargingUp,
        Attacking,
        CooldownToMouse
    }

    protected State CurrentState
    {
        get => (State) (float) Projectile.ai[0];
        set => Projectile.ai[0] = (float) value;
    }
    
    protected float Timer
    {
        get => Projectile.ai[1];
        set => Projectile.ai[1] = value;
    }


    private float transitionAngle = 0f;
    private Vector2 swingStartVelocity;
    public override void AI()
    {
        player.heldProj = Projectile.whoAmI;
        Projectile.active = true;
        
        float angle = player.direction == -1 ? MathHelper.Pi : 0f;
        Vector2 swingVelocity = Vector2.Zero;
        
        //Rotating here because we want the rotation axis at (-1,-1) to be 0
        //This means that at, i.e., (0, 1) we have 90deg regardless of direction
        float rotationFixUpwards = MathHelper.PiOver2 * -player.direction + angle;

        Projectile.spriteDirection = -player.direction;
        // DrawOriginOffsetX = 14 * player.direction;
        // DrawOriginOffsetY = -14;

        switch (CurrentState) {
            case State.Holding:
                swingStartVelocity = Main.MouseWorld - player.Center;

                float tempSwingStartRotation = swingStartVelocity.RotatedBy(rotationFixUpwards * -1).ToRotation();
                // Console.WriteLine($"{tempSwingStartRotation} {-ChargeUpBehindHeadAngle * player.direction}");
                // Console.WriteLine($"{MathHelper.ToDegrees(tempSwingStartRotation)} {MathHelper.ToDegrees(-ChargeUpBehindHeadAngle * player.direction)}");
                
                if ((player.direction == 1 && tempSwingStartRotation <= -ChargeUpBehindHeadAngle && tempSwingStartRotation >= -MathHelper.PiOver2) ||
                    (player.direction == -1 && tempSwingStartRotation >= ChargeUpBehindHeadAngle && tempSwingStartRotation <= MathHelper.PiOver2))
                    swingStartVelocity = (-ChargeUpBehindHeadAngle * player.direction).ToRotationVector2().RotatedBy(rotationFixUpwards * player.direction + angle);
                else if((player.direction == 1 && tempSwingStartRotation >= -MathHelper.Pi + HoldingAngleArmDown && tempSwingStartRotation <= -MathHelper.PiOver2) ||
                        (player.direction == -1 && tempSwingStartRotation <= MathHelper.Pi - HoldingAngleArmDown && tempSwingStartRotation >= MathHelper.PiOver2))
                    swingStartVelocity = (MathHelper.Pi + HoldingAngleArmDown * player.direction).ToRotationVector2().RotatedBy(rotationFixUpwards * player.direction + angle);

                swingStartVelocity.Normalize();
                swingVelocity = swingStartVelocity;
                break;
            
            case State.ChargingUp:
                if (Timer == 0)
                {
                    swingStartVelocity = swingStartVelocity.RotatedBy(rotationFixUpwards * -1);
                    swingStartVelocity.Normalize();

                    //In case the click is in the opposite direction the player is facing
                    float rotationValue = swingStartVelocity.ToRotation();
                    
                    float value = 0f;
                    if (player.direction == 1 && rotationValue < -MathHelper.PiOver2)
                        value = -MathHelper.Pi + swingStartVelocity.ToRotation();
                    else if (player.direction == -1 && rotationValue > MathHelper.PiOver2)
                        value = MathHelper.Pi + swingStartVelocity.ToRotation();
                    
                    transitionAngle = -ChargeUpBehindHeadAngle * player.direction - rotationValue + value;
                }

                float chargeProgress = EaseOut(Timer / (float) MaxChargeTimer);
                swingVelocity = swingStartVelocity.RotatedBy(transitionAngle * chargeProgress - MathHelper.PiOver2);

                if (Timer++ == MaxChargeTimer)
                {
                    swingStartVelocity = swingVelocity;
                    CurrentState = State.Attacking;
                    Timer = 0;
                }
                break;
            
            case State.Attacking:
                float attackProgress = InvertedEaseIn(((MaxAttackTimer - Timer) / (float) MaxAttackTimer));
                swingVelocity = swingStartVelocity.RotatedBy((SwingArc * attackProgress) * player.direction);

                if (Timer++ == MaxAttackTimer)
                {
                    CurrentState = State.CooldownToMouse;
                    Timer = 0;

                    //Saving end of swing vector
                    swingStartVelocity = swingVelocity;
                    swingStartVelocity.Normalize();
                }
                break;
            
            case State.CooldownToMouse:
                if (Timer == 0) {
                    float mouseRotation = (Main.MouseWorld - player.Center).RotatedBy(rotationFixUpwards).ToRotation();
                    float endOfSwingRotation = swingStartVelocity.RotatedBy(rotationFixUpwards).ToRotation();
                    
                    transitionAngle = mouseRotation - endOfSwingRotation;
                    
                    //In case the arm was going to rotate in a non natural way (behind the player's back)
                    if (player.direction == 1 && mouseRotation > MathHelper.PiOver2)
                        transitionAngle = (mouseRotation - endOfSwingRotation + MathHelper.PiOver2) * -1;
                    else if (player.direction == 1 && mouseRotation < MathHelper.PiOver2 && mouseRotation > 0)
                        transitionAngle = -0.15f;
                    else if (player.direction == -1 && mouseRotation < -MathHelper.PiOver2)
                        transitionAngle = (mouseRotation - endOfSwingRotation - MathHelper.PiOver2) * -1;
                    else if (player.direction == -1 && mouseRotation > -MathHelper.PiOver2 && mouseRotation < 0)
                        transitionAngle = 0.15f;
                }

                float cooldownProgress = (Timer / (float) MaxCooldownTimer);
                swingVelocity = swingStartVelocity.RotatedBy(transitionAngle * cooldownProgress);

                if (Timer++ == MaxCooldownTimer) 
                {
                    CurrentState = State.Holding;
                    Timer = 0;

                    swingStartVelocity = swingVelocity;
                }
                break;
        }

        //Arm + Projectile rotation based on current swing velocity
        float frontArmRotation = swingVelocity.RotatedBy(MathHelper.PiOver2 * -player.direction).ToRotation() + angle;
        float backArmRotation = swingVelocity.RotatedBy((MathHelper.PiOver2 / 2f) * -player.direction).ToRotation() + angle;

        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, frontArmRotation);
        player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.ThreeQuarters, backArmRotation);
            
        Vector2 rotationCenter = player.Center + new Vector2(-18f * player.direction, -2f);
        Projectile.Center = rotationCenter + swingVelocity * HoldingRadius;
        Projectile.rotation = swingVelocity.ToRotation() + angle + MathHelper.PiOver4 * player.direction;
    }
}
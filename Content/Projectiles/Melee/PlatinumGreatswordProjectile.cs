using AllBeginningsMod.Common.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee;

public class PlatinumGreatswordProjectile : BaseSwingableGreatswordProjectile
{
    public override void SetStaticDefaults() => DisplayName.SetDefault("Platinum Greatsword");

    public override void SetDefaults() 
    {
        base.SetDefaults();
        
        Projectile.width = 46;
        Projectile.height = 46;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.damage = 22;
        
        //Greatsword properties
        ChargeUpBehindHeadAngle = MathHelper.Pi / 6f; //30deg
        HoldingAngleArmDown = MathHelper.Pi / 12f; //15deg
        SwingArc = 4 * MathHelper.Pi / 3f; //240deg
        MaxChargeTimer = 45;
        MaxAttackTimer = 15;
        MaxCooldownTimer = 15;
        HoldingRadius = 14;
    }

    public override void AI() 
    {
        DrawOriginOffsetX = 14 * player.direction;
        DrawOriginOffsetY = -14;
        
        base.AI();
    }

    public override void ModifyDamageHitbox(ref Rectangle hitbox)
    {
        //Change depending on arm rotation.. or projectile rotation
    }
}
using AllBeginningsMod.Common.Bases.Projectiles;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Melee;

public class GoldGreatswordProjectile : GreatswordProjectileBase
{
    public override void SetStaticDefaults() => DisplayName.SetDefault("Gold Greatsword");

    public override void SetDefaults() {
        base.SetDefaults();

        Projectile.width = 46;
        Projectile.height = 46;

        //Greatsword properties
        ChargeUpBehindHeadAngle = MathHelper.Pi / 6f; //30deg
        HoldingAngleArmDown = MathHelper.Pi / 12f; //15deg
        SwingArc = 4 * MathHelper.Pi / 3f; //240deg
        HoldingRadius = 14;
        MaxChargeTimer = 45;
        MaxAttackTimer = 15;
        MaxCooldownTimer = 15;
    }

    public override void AI() {
        DrawOriginOffsetX = 14 * player.direction;
        DrawOriginOffsetY = -14;

        base.AI();
    }
}
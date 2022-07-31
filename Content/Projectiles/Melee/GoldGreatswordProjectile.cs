using AllBeginningsMod.Common.Bases.Projectiles;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Melee;

public class GoldGreatswordProjectile : GreatswordProjectileBase
{
    protected override float ChargeUpBehindHeadAngle => MathHelper.Pi / 6f;
    protected override float SwingArc => 4 * MathHelper.Pi / 3f;
    protected override int HoldingRadius => 14;
    protected override Vector2 RotationOrigin => new(10f, 38f);
    protected override int MaxChargeTimer => 30;
    protected override int MaxAttackTimer => 10;
    protected override int MaxCooldownTimer => 15;
    protected override int MaxSmoothTimer => 10;

    public override void SetStaticDefaults() {
        base.SetStaticDefaults();

        DisplayName.SetDefault("Gold Greatsword");
    }

    public override void SetDefaults() {
        base.SetDefaults();

        Projectile.width = 46;
        Projectile.height = 46;
    }

    public override void AI() {
        base.AI();
    }
}
using AllBeginningsMod.Common.Bases.Projectiles;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Melee;

public class BorealWoodGreatswordProjectile : GreatswordProjectileBase
{
    public override float ChargeUpBehindHeadAngle => MathHelper.Pi / 6f;

    public override float SwingArc => 7f * MathHelper.PiOver4;

    public override int HoldingRadius => 14;

    public override Vector2 RotationOrigin => new(10f, 38f);

    public override int MaxChargeTimer => 40;
    public override int MaxAttackTimer => 10;
    public override int MaxCooldownTimer => 15;
    public override int MaxSmoothTimer => 15;

    public override void SetDefaults() {
        base.SetDefaults();

        Projectile.width = 52;
        Projectile.height = 52;
    }
}
using AllBeginningsMod.Common.Bases.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee;

public class ArcticClaymoreProjectile : GreatswordProjectileBase
{
    public override float ChargeUpBehindHeadAngle => MathHelper.Pi / 6f;

    public override float SwingArc => 7f * MathHelper.PiOver4;

    public override int HoldingRadius => 14;

    public override Vector2 RotationOrigin => new(10f, 38f);

    public override int MaxChargeTimer => 20;
    public override int MaxAttackTimer => 10;
    public override int MaxCooldownTimer => 15;
    public override int MaxSmoothTimer => 15;

    public override void SetDefaults() {
        base.SetDefaults();
        Projectile.width = 50;
        Projectile.height = 50;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].Center, Vector2.Normalize(Projectile.DirectionTo(Main.MouseWorld)) * 8f, ModContent.ProjectileType<ArcticClaymoreWaveProjectile>(), Projectile.damage, Projectile.knockBack, Main.player[Projectile.owner].whoAmI);
    }
}

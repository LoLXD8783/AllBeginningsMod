using AllBeginningsMod.Common.Bases;

namespace AllBeginningsMod.Content.Projectiles.Melee;

public sealed class PlatinumGreatswordProjectile : GreatswordProjectileBase
{
    public override void SetDefaults() {
        Projectile.width = 55;
        Projectile.height = 55;

        base.SetDefaults();
    }
}
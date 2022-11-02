using AllBeginningsMod.Common.Bases;

namespace AllBeginningsMod.Content.Projectiles.Melee;

public sealed class SilverGreatswordProjectile : GreatswordProjectileBase
{
    public override void SetDefaults() {
        Projectile.width = 50;
        Projectile.height = 50;

        base.SetDefaults();
    }
}
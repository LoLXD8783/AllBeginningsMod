using AllBeginningsMod.Common.Bases;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Summon;

public sealed class PlumeWhipProjectile : WhipProjectileBase
{
    public override int HeadHeight => 24;

    public override int ChainHeight => 12;

    public override int HandleWidth => 20;
    public override int HandleHeight => 26;

    public override Color BackLineColor => new(202, 151, 100);

    public override void SetDefaults() {
        base.SetDefaults();

        Projectile.WhipSettings.Segments = 14;
        Projectile.WhipSettings.RangeMultiplier = 1f;
    }
}
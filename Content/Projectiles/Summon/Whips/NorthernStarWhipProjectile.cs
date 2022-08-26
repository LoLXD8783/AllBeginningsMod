using AllBeginningsMod.Common.Bases.Projectiles;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Summon.Whips;

public sealed class NorthernStarWhipProjectile : WhipProjectileBase
{
    public override int HeadHeight => 34;

    public override int ChainHeight => 14;

    public override int HandleWidth => 20;
    public override int HandleHeight => 20;

    public override Color BackLineColor => new(102, 255, 255);

    public override void SetDefaults() {
        base.SetDefaults();

        Projectile.WhipSettings.Segments = 16;
        Projectile.WhipSettings.RangeMultiplier = 1.5f;
    }
}
using AllBeginningsMod.Common.Bases.Projectiles;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Summon.Whips
{
    public sealed class TimberWhipProjectile : WhipProjectileBase
    {
        public override int HeadHeight => 48;

        public override int ChainHeight => 14;

        public override int HandleWidth => 14;
        public override int HandleHeight => 32;

        public override Color BackLineColor => new(102, 49, 25);

        public override void SetStaticDefaults() {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Timber Whip");
        }

        public override void SetDefaults() {
            base.SetDefaults();

            Projectile.WhipSettings.Segments = 12;
            Projectile.WhipSettings.RangeMultiplier = 1f;
        }
    }
}
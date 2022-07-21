using AllBeginningsMod.Common.Bases.Projectiles;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Summon
{
    public sealed class TimberWhipProjectile : WhipProjectileBase
    {
        public override int HeadHeight { get; } = 48;
        public override int ChainHeight { get; } = 14;
        public override int HandleWidth { get; } = 14;
        public override int HandleHeight { get; } = 32;
        public override Color BackLineColor { get; } = new(102, 49, 25);

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
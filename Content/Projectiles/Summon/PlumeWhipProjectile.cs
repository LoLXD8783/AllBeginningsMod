using AllBeginningsMod.Common.Bases.Projectiles;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Summon
{
    public sealed class PlumeWhipProjectile : WhipProjectileBase
    {
        public override int HeadHeight { get; } = 24;

        public override int ChainHeight { get; } = 12;

        public override int HandleWidth { get; } = 20;
        public override int HandleHeight { get; } = 26;

        public override Color BackLineColor { get; } = new(202, 151, 100);

        public override void SetStaticDefaults() {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Plume Whip");
        }

        public override void SetDefaults() {
            base.SetDefaults();

            Projectile.WhipSettings.Segments = 14;
            Projectile.WhipSettings.RangeMultiplier = 1f;
        }
    }
}
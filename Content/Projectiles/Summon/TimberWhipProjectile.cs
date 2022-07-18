using AllBeginningsMod.Common.Bases.Projectiles;

namespace AllBeginningsMod.Content.Projectiles.Summon
{
    public sealed class TimberWhipProjectile : WhipProjectileBase
    {
        public override int HandleWidth => 14;
        public override int HandleHeight => 32;

        public override void SetStaticDefaults() {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Timber Whip");
        }
    }
}
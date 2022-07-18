using AllBeginningsMod.Common.Bases.Projectiles;

namespace AllBeginningsMod.Content.Projectiles.Summon
{
    public sealed class PlumeWhipProjectile : WhipProjectileBase
    {
        public override int HandleWidth => 20;
        public override int HandleHeight => 26;

        public override void SetStaticDefaults() {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Plume Whip");
        }
    }
}
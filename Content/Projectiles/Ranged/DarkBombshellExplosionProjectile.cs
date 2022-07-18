using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Ranged
{
    public sealed class DarkBombshellProjectileExplosion : ModProjectile
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Dark Bombshell");
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.hostile = true;

            Projectile.width = 128;
            Projectile.height = 128;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 5;
            Projectile.aiStyle = -1;
            AIType = -1;
        }
    }
}
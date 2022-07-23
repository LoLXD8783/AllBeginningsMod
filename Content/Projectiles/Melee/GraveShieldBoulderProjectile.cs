using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public sealed class GraveShieldBoulderProjectile : ModProjectile
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Grave Boulder");

            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 10;

            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults() {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 50;
            Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
        }
        public override void Kill(int timeLeft) {
            for (int i = 0; i < Main.rand.Next(4); i++) {
                Dust.NewDust(Projectile.Center, 1, 1, DustID.Stone, Main.rand.Next(-3, 3), Main.rand.Next(-3, 3));
            }
        }
    }
}

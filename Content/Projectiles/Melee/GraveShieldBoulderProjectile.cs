using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public sealed class GraveShieldBoulderProjectile : ModProjectile
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Grave Boulder");

            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 50;
            Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
        }

        public override void Kill(int timeLeft) {
            int dustCount = Main.rand.Next(2, 5);

            for (int i = 0; i < dustCount; i++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Stone);
            }

            Projectile.netUpdate = true;
        }
    }
}

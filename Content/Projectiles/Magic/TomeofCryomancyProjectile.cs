using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using AllBeginningsMod.Common.Bases.Projectiles;


namespace AllBeginningsMod.Content.Projectiles.Magic
{
    public sealed class TomeofCryomancyProjectile : ModProjectileBase {
        public override void SetStaticDefaults() {
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults() {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 120;
            Projectile.aiStyle = -1;
            Projectile.frame = Main.rand.Next(3);
        }
        int timer;
        public override void AI() {
            timer++;
            if (timer > 2) {
                Dust.NewDustDirect(Projectile.Center, 1, 1, DustID.Snow, 0, 0).noGravity = true;
                timer = 0;
            }
            if (Projectile.timeLeft == 120) {
                for (int i = 0; i < 30; i++) {
                    float rotation = MathHelper.TwoPi * i / 30;
                    Vector2 velocity = rotation.ToRotationVector2() * 3;
                    Dust.NewDustDirect(Projectile.Center, 1, 1, DustID.Snow, velocity.X, velocity.Y).noGravity = true;
                }
            }
            Projectile.velocity *= 0.96f;
            Projectile.rotation += Projectile.velocity.X * 0.1f;
        }
        public override void Kill(int timeLeft) {
            for (int i = 0; i < Main.rand.Next(3, 9); i++) {
                Dust.NewDustDirect(Projectile.Center, 1, 1, DustID.Snow, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)).noGravity = true;
            }
        }
    }
}

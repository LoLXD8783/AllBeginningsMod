using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Summon
{
    public sealed class PlumeWhipFeather : ModProjectile
    {
        public override void SetDefaults() {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.timeLeft = 30;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = 0;
        }

        public override void AI() {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.velocity *= 0.95f;
            Projectile.alpha += 8;
        }
    }
}

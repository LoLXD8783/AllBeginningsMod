using AllBeginningsMod.Common.Bases.Projectiles;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public sealed class ArcticClaymoreWaveProjectile : ModProjectileBase
    {
        public override void SetDefaults() {
            Projectile.width = 80;
            Projectile.height = 46;
            Projectile.timeLeft = 60;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
        }
        public override void AI() {
            Projectile.alpha += 4;
            Projectile.velocity *= 0.95f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
    }
}

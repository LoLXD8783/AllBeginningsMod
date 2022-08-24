using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using AllBeginningsMod.Common.Bases.Projectiles;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public sealed class ArcticClaymoreProjectile : ModProjectileBase
    {
        public override void SetDefaults() {
            Projectile.width = 80;
            Projectile.height = 46;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
            Projectile.aiStyle = -1;
            Projectile.scale = 0.8f;
        }
        float rotationValue = 0.65f;
        public override void AI() {
            rotationValue -= 0.01f;
            Projectile.rotation += rotationValue;
            Projectile.velocity *= 0.95f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.AddBuff(BuffID.Frostburn, 120);
        }
    }
}

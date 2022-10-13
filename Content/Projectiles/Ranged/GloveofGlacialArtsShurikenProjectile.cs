using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AllBeginningsMod.Common.Bases.Projectiles;

namespace AllBeginningsMod.Content.Projectiles.Ranged
{
    public sealed class GloveofGlacialArtsShurikenProjectile : ModProjectileBase
    {
        public override void SetDefaults() {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.timeLeft = 40;
            Projectile.friendly = true;
            Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.AddBuff(BuffID.Frostburn, 120);
        }
        public override void Kill(int timeLeft) {
            for (int i = 0; i < Main.rand.Next(3, 9); i++) {
                Dust.NewDustDirect(Projectile.Center, 1, 1, DustID.Snow, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)).noGravity = true;
            }
        }
    }
}

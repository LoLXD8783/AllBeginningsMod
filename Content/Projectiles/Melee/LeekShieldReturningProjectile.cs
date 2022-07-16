using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public sealed class LeekShieldReturningProjectile : ModProjectile
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Leek Shield");
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;

            Projectile.width = 30;
            Projectile.height = 30;

            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;

            Projectile.aiStyle = 3;
            AIType = ProjectileID.WoodenBoomerang;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.AddBuff(BuffID.DryadsWardDebuff, 120);
        }
    }
}
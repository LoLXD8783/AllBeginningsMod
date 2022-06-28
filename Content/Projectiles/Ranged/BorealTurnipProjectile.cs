using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Ranged
{
    public class BorealTurnipProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 160;
            Projectile.aiStyle = 2;
        }
        int dustTimer;
        int ricochet;
        public override void AI()
        {
            dustTimer++;
            if (dustTimer > 8)
            {
                Dust.NewDustDirect(Projectile.Center, 1, 1, DustID.Snow, 0, 0).noGravity = true;
            }

            if (ricochet == 1)
            {
                Projectile.velocity = new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-8, -5));
                Projectile.timeLeft = 120;
                ricochet += 1;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            ricochet += 1;
            target.AddBuff(BuffID.Frostburn, 135);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDust(Projectile.Center, 1, 1, DustID.Snow, Main.rand.Next(-3, 3), Main.rand.Next(-3, 3));
            }
        }
    }
}

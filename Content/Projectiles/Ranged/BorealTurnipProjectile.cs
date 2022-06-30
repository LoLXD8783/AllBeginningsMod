using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Ranged
{
    public sealed class BorealTurnipProjectile : ModProjectile
    {
        private int ricochet;

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 160;
            Projectile.aiStyle = 2;
        }

        public override void AI()
        {
            if (Main.GameUpdateCount % 10f == 0f)
            {
                Dust.NewDustDirect(Projectile.Center, 1, 1, DustID.Snow, 0, 0).noGravity = true;
            }

            if (ricochet == 1)
            {
                Projectile.velocity = new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-8f, -5f));
                Projectile.timeLeft = 120;

                ricochet++;
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
                Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Snow, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f)).noGravity = true;
            }
        }
    }
}
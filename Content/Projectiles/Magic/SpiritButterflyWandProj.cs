using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Magic
{
    public class SpiritButterflyWandProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 50;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 1.2f;
        }
        int timer;
        public override void AI()
        {
            timer++;
            if (timer >= 2)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Enchanted_Pink, Vector2.Zero).noGravity = true;
                timer = 0;
            }
            Projectile.velocity *= 0.97f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.Center, 1, 1, DustID.Enchanted_Pink, 0, 0);
            }
        }
    }
}

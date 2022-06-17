using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public class NightPiercerThrown : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 24;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 30;
            Projectile.tileCollide = true;
            Projectile.aiStyle = 1;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 9; i++)
            {
                Dust.NewDust(Projectile.Center, 1, 1, DustID.PurpleCrystalShard);
            }
        }
    }
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public class LeekShieldProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 800;
            Projectile.aiStyle = 3;
            AIType = ProjectileID.WoodenBoomerang;
        }
    }
}

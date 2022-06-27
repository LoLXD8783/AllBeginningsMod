using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public class LeekSwordProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 46;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 15;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
        }
        public override void AI()
        {
            Player player = Main.LocalPlayer;
            int acc = Projectile.timeLeft * 2;
            player.velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * acc;
            player.armorEffectDrawShadow = true;

            if (Projectile.timeLeft == 15)
            {
                Projectile.rotation = (Main.MouseWorld - player.Center).ToRotation() + MathHelper.PiOver4 + MathHelper.Pi;
            }
            Projectile.Center = player.Center;
        }
    }
}

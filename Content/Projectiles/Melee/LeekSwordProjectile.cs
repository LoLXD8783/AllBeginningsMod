using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public sealed class LeekSwordProjectile : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Leek Sword");

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.hostile = false;
            
            Projectile.width = 40;
            Projectile.height = 46;

            Projectile.timeLeft = 15;            
            Projectile.penetrate = -1;

            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            Player player = Main.LocalPlayer;
            player.heldProj = Projectile.whoAmI;
            player.velocity = player.DirectionTo(Main.MouseWorld) * Projectile.timeLeft * 2f;

            player.armorEffectDrawShadow = true;

            if (Projectile.timeLeft == 15)
            {
                Projectile.rotation = (Main.MouseWorld - player.Center).ToRotation() + MathHelper.ToRadians(135f);
            }

            Projectile.Center = player.Center;
        }
    }
}

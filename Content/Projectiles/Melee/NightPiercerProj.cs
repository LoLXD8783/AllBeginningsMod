using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public class NightPiercerProj : Spear
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night Piercer");
		}
		public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 62;
            Projectile.aiStyle = 19;
            Projectile.penetrate = -1;
            Projectile.scale = 1.2f;
            Projectile.alpha = 0;

            Projectile.hide = true;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
        }
    }
}

using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public abstract class Spear : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.aiStyle = 19;
            Projectile.penetrate = -1;
            Projectile.scale = 1.3f;
            Projectile.alpha = 0;

            Projectile.hide = true;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            float progress = 1 - owner.itemAnimation / (float)owner.itemAnimationMax; // 0 to 1
            progress = progress * 2 - 1; // -1 to 1
            Projectile.Center = Vector2.SmoothStep(Projectile.Center + Vector2.Normalize(Projectile.velocity) * (Projectile.width + owner.width), Projectile.Center, Math.Abs(progress));
            if (owner.itemAnimation == 1)
                Projectile.Kill();
        }
    }
}

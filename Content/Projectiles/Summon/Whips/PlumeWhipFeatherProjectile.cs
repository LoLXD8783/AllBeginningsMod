using AllBeginningsMod.Common.Bases.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Content.Projectiles.Summon.Whips;

public sealed class PlumeWhipFeatherProjectile : ModProjectileBase
{
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("Plume Whip Feather");
    }

    public override void SetDefaults() {
        Projectile.friendly = true;
        Projectile.tileCollide = false;

        Projectile.width = 16;
        Projectile.height = 16;

        Projectile.timeLeft = 60;
        Projectile.penetrate = -1;

        Projectile.aiStyle = -1;
        AIType = -1;
    }

    public override void AI() {
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

        Projectile.velocity *= 0.95f;

        Projectile.alpha += 5;

        if (Projectile.alpha > 255)
            Projectile.Kill();

        Projectile.friendly = Projectile.alpha < 200;
    }
}
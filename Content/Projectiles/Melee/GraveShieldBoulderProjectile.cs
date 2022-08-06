using AllBeginningsMod.Common.Bases.Projectiles;
using Terraria;
using Terraria.ID;

namespace AllBeginningsMod.Content.Projectiles.Melee;

public sealed class GraveShieldBoulderProjectile : ModProjectileBase
{
    public override void SetStaticDefaults() {
        Main.projFrames[Projectile.type] = 3;
    }

    public override void SetDefaults() {
        Projectile.friendly = true;
        Projectile.hostile = false;

        Projectile.width = 16;
        Projectile.height = 16;

        Projectile.penetrate = -1;
        Projectile.timeLeft = 50;
        Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;

        Projectile.frame = Main.rand.Next(3);
    }

    public override void AI() {
        Projectile.alpha += 4;
    }
}
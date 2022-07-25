using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee;

public sealed class GraveShieldBoulderProjectile : ModProjectile
{
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("Grave Boulder");

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

    public override void AI() => Projectile.alpha += 4;
}
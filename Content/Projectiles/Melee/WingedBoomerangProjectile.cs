using AllBeginningsMod.Content.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee;

public sealed class WingedBoomerangProjectile : ModProjectile
{
    public override void SetDefaults() {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.tileCollide = false;
        Projectile.timeLeft = 800;
        Projectile.penetrate = -1;
        Projectile.aiStyle = 3;
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
        for (int i = 0; i < Main.rand.Next(4); i++)
            Dust.NewDust(Projectile.Center, 1, 1, ModContent.DustType<FeatherDust>(), Main.rand.Next(-4, 4), Main.rand.Next(-4, 4));
    }
}
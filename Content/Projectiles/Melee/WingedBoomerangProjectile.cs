using AllBeginningsMod.Common.Bases.Projectiles;
using AllBeginningsMod.Content.Dusts;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Melee;

public sealed class WingedBoomerangProjectile : ModProjectileBase
{
    public override void SetDefaults() {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.penetrate = -1;
        Projectile.aiStyle = -1;
    }

    private float Timer {
        get => Projectile.ai[1];
        set => Projectile.ai[1] = value;
    }

    private float value = 0.1f;

    public override void AI() {
        Player player = Main.player[Projectile.owner];
        Vector2 mouse = Main.MouseWorld;
        Projectile.rotation += 0.6f;

        if (Timer <= 30)
            value += 0.05f;
        else
            value -= 0.05f;

        if (Vector2.Distance(Projectile.Center, mouse) > 3 * 16)
            Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(mouse) * 10, MathHelper.Clamp(value, 0, 1));

        if (Timer++ >= 60)
            Timer = 0;

        if (!player.channel)
            Projectile.Kill();
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
        for (int i = 0; i < Main.rand.Next(4); i++)
            Dust.NewDust(Projectile.Center, 1, 1, ModContent.DustType<FeatherDust>(), Main.rand.Next(-4, 4), Main.rand.Next(-4, 4));
    }

    public override void Kill(int timeLeft) {
        for (int i = 0; i < Main.rand.Next(4, 9); i++)
            Dust.NewDust(Projectile.Center, 1, 1, ModContent.DustType<FeatherDust>(), Main.rand.Next(-2, 2), Main.rand.Next(-4, 4));
    }
}
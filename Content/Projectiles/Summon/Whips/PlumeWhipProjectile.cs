using AllBeginningsMod.Common.Bases.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Summon.Whips;

public sealed class PlumeWhipProjectile : WhipProjectileBase
{
    public override int HeadHeight => 24;

    public override int ChainHeight => 12;

    public override int HandleWidth => 20;
    public override int HandleHeight => 26;

    public override Color BackLineColor => new(202, 151, 100);

    public override void SetDefaults() {
        base.SetDefaults();

        Projectile.WhipSettings.Segments = 14;
        Projectile.WhipSettings.RangeMultiplier = 1f;
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
        int featherCount = Main.rand.Next(2, 5);

        for (int i = 0; i < featherCount; i++) {
            Vector2 velocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(60f));
            Projectile.NewProjectile(
                Projectile.GetSource_OnHit(target),
                target.Center,
                velocity,
                ModContent.ProjectileType<PlumeWhipFeatherProjectile>(),
                damage / featherCount + 1,
                Projectile.knockBack,
                Projectile.owner
            );
        }

        Projectile.netUpdate = true;
    }
}
using AllBeginningsMod.Common.Bases.Projectiles;
using AllBeginningsMod.Utility;
using AllBeginningsMod.Utility.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee;

public sealed class GraveShieldThrownProjectile : ModProjectileBase
{
    public override void SetStaticDefaults() {
        ProjectileID.Sets.TrailingMode[Type] = 2;
        ProjectileID.Sets.TrailCacheLength[Type] = 10;
    }

    public override void SetDefaults() {
        Projectile.friendly = true;
        Projectile.hostile = false;

        Projectile.width = 32;
        Projectile.height = 32;

        Projectile.timeLeft = 300;
        Projectile.aiStyle = ProjAIStyleID.Boomerang;
        AIType = ProjectileID.WoodenBoomerang;
    }

    public override void AI() {
        if (Main.GameUpdateCount % 3f == 0f)
            Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, -Projectile.velocity / 2f).noGravity = true;
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
        DustUtils.SpawnCircle(Projectile.Center, DustID.AmberBolt, 30, 5f);

        for (int i = 0; i < 3; i++)
            Projectile.NewProjectile(
                Projectile.GetSource_OnHit(target),
                Projectile.Center,
                new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-5f, -1f)),
                ModContent.ProjectileType<GraveShieldBoulderProjectile>(),
                Projectile.damage,
                Projectile.knockBack * 2f,
                Projectile.owner
            );
    }

    public override bool OnTileCollide(Vector2 oldVelocity) {
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);

        return true;
    }

    public override bool PreDraw(ref Color lightColor) {
        Projectile.DrawAfterimage(lightColor, Projectile.Hitbox.GetCenterOrigin(), 0.8f, 1f, 2);

        return true;
    }
}
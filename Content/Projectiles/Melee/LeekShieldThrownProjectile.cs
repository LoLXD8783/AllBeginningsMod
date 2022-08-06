using AllBeginningsMod.Common.Bases.Projectiles;
using AllBeginningsMod.Utility;
using AllBeginningsMod.Utility.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee;

public sealed class LeekShieldThrownProjectile : ModProjectileBase
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

        Projectile.timeLeft = 45;
        Projectile.aiStyle = -1;
        AIType = -1;
    }

    public override void AI() {
        Projectile.direction = Projectile.velocity.X < 0f ? -1 : 1;
        Projectile.spriteDirection = Projectile.direction;

        Projectile.velocity *= 0.95f;
        Projectile.rotation += Projectile.velocity.X * 0.1f;
    }

    public override bool OnTileCollide(Vector2 oldVelocity) {
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);

        return true;
    }

    public override void Kill(int timeLeft) {
        SoundEngine.PlaySound(SoundID.Grass, Projectile.position);

        int splitCount = Main.rand.Next(3, 5);

        for (int i = 0; i < splitCount; i++)
            Projectile.NewProjectile(
                Projectile.GetSource_Death(),
                Projectile.Center,
                new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f)),
                ModContent.ProjectileType<LeekShieldReturningProjectile>(),
                Projectile.damage / splitCount,
                Projectile.knockBack,
                Projectile.owner
            );

        for (int i = 0; i < 10; i++) {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Grass);
            dust.noGravity = true;
        }
    }

    public override bool PreDraw(ref Color lightColor) {
        Projectile.DrawAfterimage(lightColor, Projectile.Hitbox.Size() / 2f, 0.8f, 0.1f, 2);

        return true;
    }
}
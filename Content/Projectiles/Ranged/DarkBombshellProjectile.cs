using AllBeginningsMod.Content.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Ranged
{
    public sealed class DarkBombshellProjectile : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Dark Bombshell");

        public override void SetDefaults()
        {
            Projectile.friendly = true;

            Projectile.DamageType = DamageClass.Ranged;
            
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.timeLeft = 180;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.Center, Projectile.velocity, Projectile.width, Projectile.height);
            
            if (MathF.Abs(Projectile.velocity.X - oldVelocity.X) > 0f)
            {
                Projectile.velocity.X = -oldVelocity.X * 0.8f;
            }
            
            if (MathF.Abs(Projectile.velocity.Y - oldVelocity.Y) > 0f)
            {
                Projectile.velocity.Y = -oldVelocity.Y * 0.8f;
            }

            return false;
        }

        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.1f;
            Projectile.velocity.Y += 0.2f;

            if (Main.GameUpdateCount % 5f == 0f)
            {
                Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<DarkBombshellSparkDust>(), new Vector2(2f).RotatedBy(Projectile.rotation));
            }
        }

        public override void Kill(int timeLeft) => Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position, Vector2.Zero, ModContent.ProjectileType<DarkBombshellExplosionProjectile>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
    }
}
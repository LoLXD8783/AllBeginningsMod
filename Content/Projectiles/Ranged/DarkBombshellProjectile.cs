using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Ranged
{
    public sealed class DarkBombshellProjectile : ModProjectile
    {
        private static readonly SoundStyle explosionSound = new("AllBeginningsMod/Assets/Sounds/Item/DarkBombshellExplosion") {
            PitchVariance = 0.5f
        };

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Dark Bombshell");
        }

        public override void SetDefaults() {
            Projectile.friendly = true;

            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.timeLeft = 180;
            Projectile.aiStyle = 0;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Collision.HitTiles(Projectile.Center, Projectile.velocity, Projectile.width, Projectile.height);

            if (Projectile.velocity.X != oldVelocity.X && MathF.Abs(oldVelocity.X) > 0f) {
                Projectile.velocity.X = -oldVelocity.X * 0.75f;
            }

            if (Projectile.velocity.Y != oldVelocity.Y && MathF.Abs(oldVelocity.Y) > 0f) {
                Projectile.velocity.Y = -oldVelocity.Y * 0.75f;
            }

            return false;
        }

        public override void AI() {
            Projectile.velocity.Y += 0.2f;
            Projectile.rotation += Projectile.velocity.X * 0.05f;
        }

        public override void Kill(int timeLeft) {
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position, Vector2.Zero, ModContent.ProjectileType<DarkBombshellProjectileExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            SoundEngine.PlaySound(explosionSound, Projectile.position);
        }
    }
}
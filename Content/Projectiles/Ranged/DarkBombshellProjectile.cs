using AllBeginningsMod.Common.CustomEntities.Particles;
using AllBeginningsMod.Content.CustomEntities.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Ranged
{
    public sealed class DarkBombshellProjectile : ModProjectile
    {
        private static readonly SoundStyle explosionSound = new($"{nameof(AllBeginningsMod)}/Assets/Sounds/Item/DarkBombshellExplosion")
        {
            PitchVariance = 0.5f
        };

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Bombshell");

            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;

            Projectile.DamageType = DamageClass.Ranged;
            
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.timeLeft = 180;
            Projectile.penetrate = -1;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity.Y = -oldVelocity.Y / 2f;
            return false;
        }
        
        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.1f;
            Projectile.velocity.Y += 0.2f;

            if (Projectile.timeLeft <= 5) 
            {
                Projectile.alpha = 255; 
                Projectile.Resize(100, 100);
            }
        }

        public override void Kill(int timeLeft)
        {
            Projectile.Resize(10, 10);

            for (int i = 0; i < 16; i++)
            {
                ParticleSystem.Spawn(new DarkBombshellParticle
                {
                    Position = Projectile.Center,
                    Velocity = Main.rand.NextVector2Circular(-4f, 4f)
                }); 
            }

            SoundEngine.PlaySound(explosionSound, Projectile.Center);
        }
    }
}
using AllBeginningsMod.Common.CustomEntities.Particles;
using AllBeginningsMod.Common.CustomEntities.PrimitiveTrails;
using AllBeginningsMod.Content.CustomEntities.Particles;
using AllBeginningsMod.Content.CustomEntities.PrimitiveTrails;
using AllBeginningsMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Ranged
{
    public sealed class DarkBombshellProjectile : ModProjectile
    {
        private PrimitiveTrail trail;
        private bool spawnedTrail;

        private static readonly SoundStyle explosionSound = new($"{nameof(AllBeginningsMod)}/Assets/Sounds/Item/DarkBombshellExplosion")
        {
            PitchVariance = 0.5f
        };

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Bombshell");

            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 21;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;

            Projectile.DamageType = DamageClass.Ranged;
            
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.scale = 0.85f;
            
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
            if (!spawnedTrail)
            {
                trail = PrimitiveTrailSystem.Spawn(new ColorPrimitiveTrail(Projectile, 21, new Color(127, 203, 192), 1f, 2.5f));
                spawnedTrail = true;
            }

            Projectile.rotation += Projectile.velocity.X * 0.1f;
            Projectile.velocity.Y += 0.2f;

            if (Projectile.timeLeft <= 5)
            {
                Projectile.hostile = true;

                Projectile.alpha = 255;
                Projectile.Resize(100, 100);
            }
        }

        public override void Kill(int timeLeft)
        {
            if (spawnedTrail)
            {
                PrimitiveTrailSystem.Kill(trail);
            }
            
            Projectile.Resize(16, 16);

            ParticleSystem.Spawn(new DarkBombshellSkullParticle
            {
                Position = Projectile.Center
            });

            for (int i = 0; i < 16; i++)
            {
                ParticleSystem.Spawn(new DarkBombshellSmokeParticle
                {
                    Position = Projectile.Center, 
                    Velocity = Main.rand.NextVector2Circular(-4f, 4f)
                });

                float rotation = i / 16f * MathHelper.TwoPi;
                Vector2 velocity = rotation.ToRotationVector2() * 10f;

                Dust.NewDust(Projectile.Center, 0, 0, ModContent.DustType<DarkBombshellSparkDust>(), velocity.X, velocity.Y);
            }

            SoundEngine.PlaySound(explosionSound, Projectile.Center);
            Collision.HitTiles(Projectile.Center, Projectile.velocity, Projectile.width, Projectile.height);
        }
        
        public override void OnHitPvp(Player target, int damage, bool crit) => Projectile.timeLeft = 5;

        public override void OnHitPlayer(Player target, int damage, bool crit) => Projectile.timeLeft = 5;

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => Projectile.timeLeft = 5;
    }
}
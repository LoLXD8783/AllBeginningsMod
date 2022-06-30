using AllBeginningsMod.Common.CustomEntities.Particles;
using AllBeginningsMod.Content.CustomEntities.Particles;
using AllBeginningsMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Ranged
{
    public sealed class DarkBombshellExplosionProjectile : ModProjectile
    {
        private static readonly SoundStyle explosionSound = new($"{nameof(AllBeginningsMod)}/Assets/Sounds/Item/DarkBombshellExplosion")
        {
            PitchVariance = 0.5f
        };

        public override void SetStaticDefaults() => DisplayName.SetDefault("Dark Bombshell");
        
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.friendly = true;

            Projectile.DamageType = DamageClass.Ranged;

            Projectile.width = 100;
            Projectile.height = 100;

            Projectile.timeLeft = 5;
            Projectile.penetrate = -1;
        }

        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 0; i < 16; i++)
            {
                ParticleSystem.Spawn(new DarkBombshellSmokeParticle
                {
                    Position = Projectile.Center,
                    Velocity = Main.rand.NextVector2Circular(-4f, 4f)
                });

                float dustRotation = i / 16f * MathHelper.TwoPi;
                Vector2 dustVelocity = dustRotation.ToRotationVector2() * 10f;

                Dust.NewDust(Projectile.Center, 0, 0, ModContent.DustType<DarkBombshellSparkDust>(), dustVelocity.X, dustVelocity.Y);
            }

            SoundEngine.PlaySound(explosionSound, Projectile.Center);
        }
    }
}
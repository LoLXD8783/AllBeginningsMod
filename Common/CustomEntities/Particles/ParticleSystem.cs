using AllBeginningsMod.Common.Config;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.CustomEntities.Particles
{        
    [Autoload(Side = ModSide.Client)]
    public sealed class ParticleSystem : ModSystem
    {
        public static Particle[] Particles { get; private set; }

        public static Queue<int> FreeIndices { get; private set; }

        public override void OnModLoad()
        {
            int maxParticles = AllBeginningsClientConfig.Instance.MaxParticles;

            Particles = new Particle[maxParticles];
            FreeIndices = new Queue<int>(maxParticles);
            
            for (int i = 0; i < maxParticles; i++)
            {
                FreeIndices.Enqueue(i);
            }

            On.Terraria.Main.DrawDust += Main_DrawDust;
        }

        public override void Unload()
        {
            On.Terraria.Main.DrawDust -= Main_DrawDust;

            Particles = null;
            FreeIndices = null;
        }

        public override void PostUpdateDusts()
        {
            for (int i = 0; i < Particles.Length; i++)
            {
                Particles[i]?.OnUpdate();
            }
        }
        
        public static Particle Spawn(Particle particle)
        {
            if (FreeIndices.TryDequeue(out int index))
            {
                Particles[index] = particle;
                Particles[index].WhoAmI = index;
                Particles[index].OnSpawn();
            }

            return particle;
        }

        public static void Kill(Particle particle)
        {
            Particles[particle.WhoAmI].OnKill();
            Particles[particle.WhoAmI] = null;
            
            FreeIndices.Enqueue(particle.WhoAmI);
        }

        private static void Main_DrawDust(On.Terraria.Main.orig_DrawDust orig, Main self)
        {
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointClamp, default, default, default, Main.GameViewMatrix.TransformationMatrix);
            
            for (int i = 0; i < Particles.Length; i++)
            {
                Particle particle = Particles[i];

                if (particle != null && particle.IsAdditive)
                {
                    particle.OnDraw();
                }
            }

            Main.spriteBatch.End();
            
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, default, default, default, Main.GameViewMatrix.TransformationMatrix);
            
            for (int i = 0; i < Particles.Length; i++)
            {
                Particle particle = Particles[i];

                if (particle != null && !particle.IsAdditive)
                {
                    particle.OnDraw();
                }
            }

            Main.spriteBatch.End();

            orig(self);
        }
    }
}
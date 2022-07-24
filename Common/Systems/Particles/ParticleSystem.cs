using AllBeginningsMod.Common.Config;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Particles
{
    [Autoload(Side = ModSide.Client)]
    public sealed class ParticleSystem : ModSystem
    {
        public static int MaxParticles => ClientSideConfig.Instance.MaxParticles;

        public static List<Particle> Particles { get; private set; }

        public override void OnModLoad() {
            Particles = new List<Particle>();

            On.Terraria.Main.DrawDust += DrawParticles;
        }

        public override void OnModUnload() {
            Particles?.Clear();
            Particles = null;

            On.Terraria.Main.DrawDust -= DrawParticles;
        }

        public override void OnWorldUnload() {
            Particles?.Clear();
        }

        public override void PostUpdateDusts() { 
            for (int i = 0; i < Particles.Count; i++) {
                if (Particles[i] is not Particle particle) {
                    continue;
                }
               
                particle.Update();
            }
        }

        public static Particle SpawnParticle(Particle particle) {
            if (MaxParticles == -1 || Particles.Count < MaxParticles) {
                particle.OnSpawn();
                Particles.Add(particle);
            }
            return particle; 
        }

        public static bool RemoveParticle(Particle particle) {
            particle.OnKill();
            return Particles.Remove(particle);
        }

        private static void DrawParticles(On.Terraria.Main.orig_DrawDust orig, Main self) {
            orig(self);

            SpriteBatch spriteBatch = Main.spriteBatch;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointClamp, default, Main.Rasterizer, default, Main.GameViewMatrix.TransformationMatrix);

            for (int i = 0; i < Particles.Count; i++) {
                if (Particles[i] is not Particle particle) {
                    continue;
                } 

                particle.Draw();
            }

            spriteBatch.End();
        }
    }
}
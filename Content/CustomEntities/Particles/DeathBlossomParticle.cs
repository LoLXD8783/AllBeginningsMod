using AllBeginningsMod.Common.CustomEntities.Particles;
using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Content.CustomEntities.Particles
{
    public sealed class DeathBlossomParticle : Particle
    {
        public readonly Vector2 MovementCenter;
        
        private int timeLeft = 180;

        public DeathBlossomParticle(Vector2 movementCenter) => MovementCenter = movementCenter;

        public override void OnSpawn()
        {
            Scale = new Vector2(Main.rand.NextFloat(0.2f, 0.4f));
            Color = new Color(92, 71, 232);
            Origin = Texture.Size() / 2f;
            Alpha = 0f;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            Vector2 position = MovementCenter + Vector2.One.RotatedBy(timeLeft * 0.05f) * Main.rand.NextFloat(10f, 20f);
            Position = Vector2.SmoothStep(Position, position, 0.1f);

            timeLeft--;

            if (timeLeft <= 0)
            {
                Alpha -= 0.05f;

                if (Alpha <= 0f)
                {
                    ParticleSystem.Kill(this);
                }
            }
            else
            {
                Alpha += 0.05f;

                if (Alpha > 1f)
                {
                    Alpha = 1f;
                }
            }
        }
    }
}
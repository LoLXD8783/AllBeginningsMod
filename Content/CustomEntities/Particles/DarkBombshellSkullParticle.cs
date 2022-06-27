using AllBeginningsMod.Common.CustomEntities.Particles;
using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Content.CustomEntities.Particles
{
    public sealed class DarkBombshellSkullParticle : Particle
    {
        public override void OnSpawn()
        {
            IsAdditive = false;

            Origin = Texture.Size() / 2f;

            Rotation = Main.rand.NextFloat(MathHelper.TwoPi);

            Alpha = 0.9f;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            Velocity *= 0.9f;

            Scale.X += 0.01f;
            Scale.Y += 0.01f;

            Color = Color.Lerp(new Color(127, 203, 192), Color.Black, 1f - Alpha);
            Alpha -= 0.01f;

            if (Alpha <= 0f)
            {
                ParticleSystem.Kill(this);
            }
        }
    }
}
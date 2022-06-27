using AllBeginningsMod.Common.CustomEntities.Particles;
using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Content.CustomEntities.Particles
{
    public sealed class DarkBombshellSmokeParticle : Particle
    {
        public override void OnSpawn()
        {
            IsAdditive = false;
            
            Frame = new Rectangle(0, Main.rand.Next(2) * 34, 26, 34);
            
            Origin = Frame.Value.Size() / 2f;

            Rotation = Main.rand.NextFloat(MathHelper.TwoPi);

            Alpha = 0.9f;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            Rotation += Velocity.X * 0.1f;
            
            Velocity.X *= 0.95f;
            Velocity.Y -= 0.025f;
        
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
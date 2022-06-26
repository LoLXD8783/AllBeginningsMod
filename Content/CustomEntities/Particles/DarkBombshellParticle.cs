using AllBeginningsMod.Common.CustomEntities.Particles;
using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Content.CustomEntities.Particles
{
    public sealed class DarkBombshellParticle : Particle
    {
        public override void OnSpawn()
        {
            Frame = new Rectangle(0, Main.rand.Next(2) * 34, 26, 34);
            Origin = Frame.Value.Size() / 2f;
            Rotation = Main.rand.NextFloat(MathHelper.TwoPi);
            Alpha = Main.rand.NextFloat(0.7f, 0.9f);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            Rotation += Velocity.X * 0.1f;
            Velocity *= 0.95f;

            Color = Color.Lerp(Color, new Color(53, 20, 132), 0.1f);
            
            Scale.X += 0.01f;
            Scale.Y += 0.01f;
            
            Alpha -= 0.01f;

            if (Alpha <= 0f)
            {
                ParticleSystem.Kill(this);
            }
        }
    }
}
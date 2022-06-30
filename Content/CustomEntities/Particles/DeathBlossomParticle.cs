using AllBeginningsMod.Common.CustomEntities.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.CustomEntities.Particles
{
    public sealed class DeathBlossomParticle : Particle
    {
        private int timeLeft = 300;
        private int sineProgress;

        public override void OnSpawn()
        {
            IsAdditive = true;

            Scale = new Vector2(Main.rand.NextFloat(0.6f, 1.2f));
            Color = new Color(92, 71, 232);
            
            Origin = Texture.Size() / 2f;

            Rotation = Main.rand.NextFloat(MathHelper.TwoPi);
            Alpha = 0f;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            sineProgress++;

            Velocity.X = MathF.Sin(sineProgress * 0.075f) * 0.75f;
            Velocity.Y -= 0.005f;

            if (Velocity.Y < -1f)
            {
                Velocity.Y = -1f;
            }

            if (timeLeft > 0)
            {
                Alpha += 0.01f;

                if (Alpha >= 1f)
                {
                    Alpha = 1f;
                }
            }
            else
            {
                Alpha -= 0.01f;

                if (Alpha <= 0f)
                {
                    ParticleSystem.Kill(this);
                }
            }

            timeLeft--;

            Lighting.AddLight(Position, 0.1f * (1f - Alpha), 0, 0.4f * (1f - Alpha));
        }

        public override void OnDraw()
        {
            Texture2D bloomTexture = ModContent.Request<Texture2D>($"{nameof(AllBeginningsMod)}/Content/CustomEntities/Particles/DeathBlossomParticle_Bloom").Value;

            Vector2 position = Position - Main.screenPosition;
            Color color = Color * Alpha;

            Main.spriteBatch.Draw(bloomTexture, position, null, color, Rotation, bloomTexture.Size() / 2f, Scale, SpriteEffects.None, 0f);

            base.OnDraw();
        }
    }
}
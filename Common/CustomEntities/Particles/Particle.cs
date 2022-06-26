using AllBeginningsMod.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.CustomEntities.Particles
{
    public abstract class Particle
    {
        public virtual Texture2D Texture => ModContent.Request<Texture2D>(GetType().FullName.Replace('.', '/')).Value;

        public int WhoAmI;

        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Origin;
        public Vector2 Scale = Vector2.One;

        public Color Color = Color.White;

        public Rectangle? Frame;
        
        public float Rotation;
        public float Alpha = 1f;
        
        public virtual void OnUpdate()
        {
            if (!ScreenUtils.WorldScreenRectangle.Contains(Position.ToPoint()))
            {
                ParticleSystem.Kill(this);
                return;
            }

            Position += Velocity;
        }

        public virtual void OnDraw()
        {
            Vector2 position = Position - Main.screenPosition;
            Color color = Color * Alpha;

            Main.spriteBatch.Draw(Texture, position, Frame, color, Rotation, Origin, Scale, SpriteEffects.None, 0f);
        }

        public virtual void OnSpawn() { }

        public virtual void OnKill() { }
    }
}
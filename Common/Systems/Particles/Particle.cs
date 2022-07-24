using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Common.Systems.Particles
{
    public abstract class Particle
    {
        public virtual string TexturePath => GetType().FullName.Replace('.', '/');

        public SpriteEffects Effects;

        public Color Color = Color.White;

        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Origin;

        public float Alpha = 1f;
        public float Scale = 1f;
        public float Rotation;

        public virtual void Update() { }

        public virtual void Draw() { }

        public virtual void OnKill() { }

        public virtual void OnSpawn() { }
    }
}
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Common.Systems.Physics.Verlet
{
    public record class VerletPoint(bool Pinned)
    {
        public Vector2 Position;
        public Vector2 OldPosition;
        public Vector2 Velocity;
    }
}
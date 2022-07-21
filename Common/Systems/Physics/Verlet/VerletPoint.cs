using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Common.Systems.Physics.Verlet
{
    public sealed class VerletPoint
    {
        public Vector2 Position;
        public Vector2 OldPosition;
        public Vector2 Velocity;

        public bool Pinned;

        public VerletPoint(Vector2 position, bool pinned) {
            Position = position;
            OldPosition = position;
            Pinned = pinned;
        }
    }
}
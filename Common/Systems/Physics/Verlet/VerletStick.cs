using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Common.Systems.Physics.Verlet
{
    public record class VerletStick(VerletPoint Start, VerletPoint End)
    {
        public float Length = Vector2.Distance(Start.Position, End.Position);
    }
}
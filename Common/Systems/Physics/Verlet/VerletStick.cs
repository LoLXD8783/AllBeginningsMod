using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Common.Systems.Physics.Verlet
{
    public sealed class VerletStick
    {
        public readonly VerletPoint Start;
        public readonly VerletPoint End;

        public readonly float Length;

        public VerletStick(VerletPoint start, VerletPoint end) {
            Start = start;
            End = end;
            Length = Vector2.Distance(start.Position, end.Position);
        }
    }
}
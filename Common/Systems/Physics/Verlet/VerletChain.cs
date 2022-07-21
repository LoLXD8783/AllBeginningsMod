using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Common.Systems.Physics.Verlet
{
    public sealed class VerletChain : PhysicsModule
    {
        public readonly int Length;

        public readonly float Gravity;
        public readonly float Friction;

        public readonly VerletPoint[] Points;
        public readonly VerletStick[] Sticks;

        public VerletChain(int length, float gravity = 0.3f, float friction = 0.999f) {
            Length = length;
            Gravity = gravity;
            Friction = friction;
            Points = new VerletPoint[length];
            Sticks = new VerletStick[length - 1];
        }

        public override void Update() {
            for (int i = 0; i < 2; i++) {
                UpdatePoints();
                UpdateSticks();
            }
        }

        public void SetSticks() {
            for (int i = 0; i < Sticks.Length; i++) {
                VerletPoint start = Points[i];
                VerletPoint end = Points[i + 1];
                Sticks[i] = new VerletStick(start, end);
            }
        }

        private void UpdatePoints() {
            for (int i = 0; i < Points.Length; i++) {
                VerletPoint point = Points[i];

                if (point == null) {
                    continue;
                }

                Vector2 distance = point.Position - point.OldPosition;
                Vector2 velocity = distance * Friction;

                point.OldPosition = point.Position;

                if (point.Pinned) {
                    return;
                }

                point.Velocity = velocity;
                point.Position += point.Velocity;
                point.Position.Y += Gravity;
            }
        }

        private void UpdateSticks() {
            for (int i = 0; i < Sticks.Length; i++) {
                VerletStick stick = Sticks[i];

                if (stick == null) {
                    continue;
                }

                Vector2 distance = stick.End.Position - stick.Start.Position;

                float length = distance.Length();
                float difference = stick.Length - length;
                float percent = difference / length / 2f;

                Vector2 offset = distance * percent;

                if (!stick.Start.Pinned) {
                    stick.Start.Position -= offset;
                }

                if (!stick.End.Pinned) {
                    stick.End.Position += offset;
                }
            }
        }
    }
}
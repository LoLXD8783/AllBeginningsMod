using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace AllBeginningsMod.Utilities
{
    internal static class PhysicsUtils
    {
        public static Vector2 InitialVelocityRequiredToHitPosition(Vector2 initialPosition, Vector2 targetPosition, float gravity, float initialSpeed, bool secondAngle = false) {
            Vector2 localTargetPosition = targetPosition - initialPosition;
            localTargetPosition.X = MathF.Abs(localTargetPosition.X);
            float randomShit = MathF.Pow(initialSpeed, 4) - gravity * (gravity * MathF.Pow(localTargetPosition.X, 2) + 2f * localTargetPosition.Y * MathF.Pow(initialSpeed, 2));
            float angle = MathF.Atan(
                (MathF.Pow(initialSpeed, 2) + MathF.Sqrt(Math.Max(randomShit, 0f)) * (secondAngle ? -1 : 1))
                / (gravity * localTargetPosition.X)
            );

            Vector2 velocity = angle.ToRotationVector2() * initialSpeed;
            velocity.Y = -velocity.Y;
            velocity.X *= MathF.Sign(targetPosition.X - initialPosition.X);

            return velocity;
        }
    }
}

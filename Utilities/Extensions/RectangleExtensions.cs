using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace AllBeginningsMod.Utilities.Extensions
{
    internal static class RectangleExtensions
    {
        public static bool Intersects(this Rectangle rectangle, Vector2 center, float radius) {
            Vector2 hitboxCenter = rectangle.Center.ToVector2();
            float distanceX = MathF.Abs(center.X - hitboxCenter.X);
            float distanceY = MathF.Abs(center.Y - hitboxCenter.Y);
            float halfWidth = rectangle.Width / 2f;
            float halfHeight = rectangle.Height / 2f;

            if (distanceX > halfWidth + radius || distanceY > halfHeight + radius) {
                return false;
            }

            if (distanceX < halfWidth || distanceY < halfHeight) {
                return true;
            }

            float distanceSquared = MathF.Pow(distanceX - halfWidth, 2) + MathF.Pow(distanceY - halfHeight, 2);
            return distanceSquared < MathF.Pow(radius, 2);
        }

        public static bool Intersects(this Rectangle rectangle, Point center, float radius) {
            return rectangle.Intersects(center.ToVector2(), radius);
        }
    }
}

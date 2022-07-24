using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;

namespace AllBeginningsMod.Utility
{
    public static class DrawUtils
    {
        public static Vector2 ScreenSize => new(Main.screenWidth, Main.screenHeight);

        public static Rectangle ScreenRectangle => new(0, 0, Main.screenWidth, Main.screenHeight);

        public static int GetPrimitiveCount(int vertexCount, PrimitiveType type) {
            return type switch {
                PrimitiveType.TriangleList => vertexCount / 3,
                PrimitiveType.TriangleStrip => vertexCount - 2,
                PrimitiveType.LineList => vertexCount / 2,
                PrimitiveType.LineStrip => vertexCount - 1,
                PrimitiveType.PointListEXT => vertexCount / 3,
                _ => throw new ArgumentException($"Unsupported primitive type: {type}", nameof(type))
            };
        }

        public static bool OnScreen(Vector2 position) {
            return position.X > 0f && position.X < Main.screenWidth && position.Y > 0f && position.Y < Main.screenHeight;
        }

        public static bool WorldOnScreen(Vector2 position) {
            return position.X > Main.screenPosition.X && position.X < Main.screenPosition.X + Main.screenWidth && position.Y > Main.screenPosition.Y && position.Y < Main.screenPosition.Y + Main.screenHeight;
        }
    }
}
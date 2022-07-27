using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace AllBeginningsMod.Utility;

public static class DrawUtils
{
    public static Rectangle ScreenRectangle => new(0, 0, Main.screenWidth, Main.screenHeight);
    
    public static Vector2 ScreenSize => new(Main.screenWidth, Main.screenHeight);

    public static Matrix World => Matrix.CreateTranslation(new Vector3(-Main.screenPosition, 0f));

    public static Matrix View => Main.GameViewMatrix.TransformationMatrix;

    public static Matrix Projection => Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, -1f, 1f);

    public static Matrix WorldViewProjection => World * View * Projection;

    public static int GetPrimitiveCount(int vertexCount, PrimitiveType type) => type switch {
        PrimitiveType.TriangleList => vertexCount / 3,
        PrimitiveType.TriangleStrip => vertexCount - 2,
        PrimitiveType.LineList => vertexCount / 2,
        PrimitiveType.LineStrip => vertexCount - 1,
        PrimitiveType.PointListEXT => vertexCount / 3,
        _ => throw new ArgumentException($"Unsupported primitive type: {type}", nameof(type))
    };

    public static bool OnScreen(Vector2 position)
        => position.X > 0f && position.X < Main.screenWidth && position.Y > 0f && position.Y < Main.screenHeight;

    public static bool WorldOnScreen(Vector2 position) => position.X > Main.screenPosition.X
        && position.X < Main.screenPosition.X + Main.screenWidth
        && position.Y > Main.screenPosition.Y
        && position.Y < Main.screenPosition.Y + Main.screenHeight;
}
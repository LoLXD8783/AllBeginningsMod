using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace AllBeginningsMod.Utility;

public static class DrawUtils
{
    private static GraphicsDevice Device => Main.graphics.GraphicsDevice;

    public static Rectangle ScreenRectangle => new(0, 0, Main.screenWidth, Main.screenHeight);

    public static Vector2 ScreenSize => new(Main.screenWidth, Main.screenHeight);

    public static Matrix World => Matrix.CreateTranslation(-Main.screenPosition.X, -Main.screenPosition.Y, 0f);

    public static Matrix View => Main.GameViewMatrix.TransformationMatrix;

    public static Matrix Projection => Matrix.CreateOrthographicOffCenter(0f, Main.screenWidth, Main.screenHeight, 0f, -1f, 1f);

    public static Matrix WorldViewProjection => World * View * Projection;
}
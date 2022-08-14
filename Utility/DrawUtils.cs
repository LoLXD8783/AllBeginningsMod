using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace AllBeginningsMod.Utility;

public static class DrawUtils
{
    private static GraphicsDevice Device => Main.graphics.GraphicsDevice;

    /// <summary>
    /// Represents the full area of the screen.
    /// </summary>
    public static Rectangle ScreenRectangle => new(0, 0, Main.screenWidth, Main.screenHeight);

    /// <summary>
    /// Represents the size of the screen.
    /// </summary>
    public static Vector2 ScreenSize => new(Main.screenWidth, Main.screenHeight);

    /// <summary>
    /// Represents the world matrix of the game.
    /// </summary>
    public static Matrix World => Matrix.CreateTranslation(-Main.screenPosition.X, -Main.screenPosition.Y, 0f);

    /// <summary>
    /// Represents the view matrix of the game.
    /// </summary>
    public static Matrix View => Main.GameViewMatrix.TransformationMatrix;

    /// <summary>
    /// Represents the projection matrix of the game.
    /// </summary>
    public static Matrix Projection => Matrix.CreateOrthographicOffCenter(0f, Main.screenWidth, Main.screenHeight, 0f, -1f, 1f);

    /// <summary>
    /// Represents the product of the world, view and projection matrices of the game.
    /// </summary>
    public static Matrix WorldViewProjection => World * View * Projection;
}
using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Utility.Extensions;

public static class RectangleExtensions
{
    /// <summary>
    /// Gets the center of a rectangle for rotation origins.
    /// </summary>
    /// <param name="rectangle">The rectangle.</param>
    /// <returns></returns>
    public static Vector2 GetCenterOrigin(this Rectangle rectangle) {
        return rectangle.Size() / 2f;
    }
}
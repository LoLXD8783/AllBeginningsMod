using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Utility.Extensions;

public static class RectangleExtensions
{
    public static Vector2 GetCenterOrigin(this Rectangle rectangle) {
        return rectangle.Size() / 2f;
    }
}
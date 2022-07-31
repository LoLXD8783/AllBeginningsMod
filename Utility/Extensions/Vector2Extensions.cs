using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Utility.Extensions;

public static class Vector2Extensions
{
    public static bool IsOnScreen(this Vector2 position, int extraWidth = 0, int extraHeight = 0) {
        return position.X > -extraWidth && position.X < Main.screenWidth + extraWidth && position.Y > -extraHeight && position.Y < Main.screenHeight + extraHeight;
    }

    public static bool IsWorldOnScreen(this Vector2 position, int extraWidth = 0, int extraHeight = 0) {
        return position.X - extraWidth > Main.screenPosition.X &&
            position.X + extraWidth < Main.screenPosition.X + Main.screenWidth &&
            position.Y - extraWidth > Main.screenPosition.Y &&
            position.Y + extraHeight < Main.screenPosition.Y + Main.screenHeight;
    }
}
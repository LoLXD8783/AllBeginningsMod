using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Utility.Extensions;

public static class Vector2Extensions
{
    public static bool IsOnScreen(Vector2 position) => position.X > 0f && position.X < Main.screenWidth && position.Y > 0f && position.Y < Main.screenHeight;

    public static bool WorldIsOnScreen(Vector2 position) => position.X > Main.screenPosition.X
        && position.X < Main.screenPosition.X + Main.screenWidth
        && position.Y > Main.screenPosition.Y
        && position.Y < Main.screenPosition.Y + Main.screenHeight;
}
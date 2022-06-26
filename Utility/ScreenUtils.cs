using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Utility
{
    public static class ScreenUtils
    {
        public static Vector2 ScreenSize => new(Main.screenWidth, Main.screenHeight);

        public static Rectangle ScreenRectangle => new(0, 0, Main.screenWidth, Main.screenHeight);
        public static Rectangle WorldScreenRectangle => new((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
    }
}
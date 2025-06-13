using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Utilities;

public static class Texture2DExtensions {
    public static Vector2 GetCenter(this Texture2D tex)
        => new Vector2(tex.Width / 2, tex.Height / 2);
}
﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace AllBeginningsMod.Utility.Extensions;

public static class Texture2DExtensions
{
    public static Vector2 GetCenterOrigin(this Texture2D texture) {
        return texture.Size() / 2f;
    }
}
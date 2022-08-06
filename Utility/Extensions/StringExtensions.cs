using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AllBeginningsMod.Utility.Extensions;

public static class StringExtensions
{
    public static string GetAssetPath(this string value) {
        return value.Replace('.', '/').Replace("Content", "Assets");
    }

    public static string GetTexturePath(this string value) {
        string texturePath = value.GetAssetPath();
        
        if (!ModContent.RequestIfExists<Texture2D>(texturePath, out _)) {
            AllBeginningsMod.Instance.Logger.Warn($"Expected to find a texture at: {texturePath} @ Fallback to a placeholder instead...");
            return AllBeginningsMod.FallbackPlaceholder;
        }

        return texturePath;
    }
}
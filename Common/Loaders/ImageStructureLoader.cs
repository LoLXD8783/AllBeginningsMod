using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Loaders;

public class ImageStructureLoader : ILoadable
{
    private static readonly Dictionary<string, ImageData> Images = new();

    public void Load(Mod mod) {
        foreach (string filePath in mod.GetFileNames()) {
            if (!filePath.StartsWith("Assets/Images/Structures") || !filePath.EndsWith(".rawimg")) {
                continue;
            }

            Texture2D texture = mod.Assets.Request<Texture2D>(filePath[..^7], AssetRequestMode.ImmediateLoad).Value;

            Color[] textureData = new Color[texture.Width * texture.Height];
            Main.RunOnMainThread(() => texture.GetData(textureData)).Wait();

            Images[Path.GetFileName(filePath)] = new ImageData(texture.Width, textureData);
        }
    }

    public static ImageData Get(string structureName) {
        return Images[structureName];
    }

    public void Unload() { }
}

public record ImageData(int Width, Color[] Data)
{
    public int Height => Data.Length / Width;
    public void EnumeratePixels(Action<int, int, Color> action) {
        for (int i = 0; i < Data.Length; i++) {
            action(i % Width, i / Width, Data[i]);
        }
    }
}
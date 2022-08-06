using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Linq;
using System.Reflection;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace AllBeginningsMod.Core.Graphics;

[Autoload(Side = ModSide.Client)]
public sealed class ScreenShaderLoader : ILoadable
{
    void ILoadable.Load(Mod mod) {
        MethodInfo info = typeof(Mod).GetProperty("File", BindingFlags.NonPublic | BindingFlags.Instance).GetGetMethod(true);
        TmodFile file = (TmodFile) info.Invoke(mod, null);

        IEnumerable<TmodFile.FileEntry> shaders = file.Where(entry => entry.Name.StartsWith("Assets/Effects/Screen/") && entry.Name.EndsWith(".xnb"));

        foreach (TmodFile.FileEntry entry in shaders) {
            string name = Path.GetFileNameWithoutExtension(entry.Name);

            LoadScreenShader(name);
        }
    }

    void ILoadable.Unload() { }

    private static void LoadScreenShader(string name) {
        Ref<Effect> effect = new(ModContent.Request<Effect>($"{AllBeginningsMod.ModName}/Assets/Effects/Screen/" + name, AssetRequestMode.ImmediateLoad).Value);
        Filter filter = new(new ScreenShaderData(effect, name + "Pass"), EffectPriority.High);
        filter.Load();

        Filters.Scene[AllBeginningsMod.ModPrefix + name] = filter;
    }
}
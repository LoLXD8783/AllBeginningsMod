using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Graphics;

[Autoload(Side = ModSide.Client)]
public sealed class ScreenShaderSystem : ModSystem
{
    [ScreenShader("Vignette")]
    public static Effect Vignette { get; private set; }

    public override void Load() {
        PropertyInfo[] properties = typeof(ScreenShaderSystem).GetProperties().ToArray();

        foreach (PropertyInfo info in properties) {
            ScreenShaderAttribute attribute = info.GetCustomAttribute<ScreenShaderAttribute>();

            if (attribute == null || info.PropertyType != typeof(Effect)) {
                continue;
            }

            info.SetValue(null, LoadScreenShader(info.Name));
        }
    }

    public override void Unload() {
        Vignette?.Dispose();
        Vignette = null;
    }

    private static Effect LoadScreenShader(string name) {
        string effectPath = $"{AllBeginningsMod.ModName}/Assets/Effects/{name}";

        Effect effect = ModContent.Request<Effect>(effectPath, AssetRequestMode.ImmediateLoad).Value;

        string prefixName = AllBeginningsMod.ModPrefix + name;
        string passName = name + "Pass";

        Filters.Scene[prefixName] = new Filter(new ScreenShaderData(new Ref<Effect>(effect), passName));
        Filters.Scene[prefixName].Load();

        return effect;
    }
}
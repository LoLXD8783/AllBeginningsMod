using AllBeginningsMod.Common.Configuration;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common;

[Autoload(Side = ModSide.Client)]
public sealed class CustomWindowTitles : ModSystem
{
    public const int MaxTitles = 16;

    public override void OnWorldLoad() {
        if (!ClientSideConfiguration.Instance.CustomWindowTitles)
            return;

        string selectedTitle = Language.GetTextValue($"Mods.{AllBeginningsMod.ModName}.CustomWindowTitles.Title{Main.rand.Next(MaxTitles)}");
        Main.instance.Window.Title = $"All Beginnings: {selectedTitle}";
    }

    public override void OnWorldUnload() {
        if (!ClientSideConfiguration.Instance.CustomWindowTitles)
            return;

        Main.changeTheTitle = true;
    }
}
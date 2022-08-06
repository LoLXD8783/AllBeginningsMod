using AllBeginningsMod.Common.Configuration;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common;

[Autoload(Side = ModSide.Client)]
public sealed class CustomWindowTitles : ILoadable
{
    public const int MaxTitles = 9;
    
    void ILoadable.Load(Mod mod) {
        if (!ClientSideConfiguration.Instance.CustomWindowTitles)
            return;
        
        string selectedTitle = Language.GetTextValue($"Mods.{AllBeginningsMod.ModName}.CustomWindowTitles.Title{Main.rand.Next(MaxTitles)}");
        Main.instance.Window.Title = $"All Beginnings: {selectedTitle}";
    }

    void ILoadable.Unload() {
        if (!ClientSideConfiguration.Instance.CustomWindowTitles)
            return;
        
        Main.changeTheTitle = true;
    }
}
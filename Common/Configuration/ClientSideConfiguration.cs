using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace AllBeginningsMod.Common.Configuration;

[BackgroundColor(85, 32, 46)]
public sealed class ClientSideConfiguration : ModConfig
{
    public static ClientSideConfiguration Instance => ModContent.GetInstance<ClientSideConfiguration>();

    public override ConfigScope Mode => ConfigScope.ClientSide;

    [Header($"$Mods.{AllBeginningsMod.ModName}.Configuration.VisualsHeader")]
    
    [DefaultValue(1000)]
    [Range(0, 8000)]
    [Increment(100)]
    [Slider]
    [SliderColor(255, 236, 134)]
    [BackgroundColor(255, 69, 103)]
    [Label($"$Mods.{AllBeginningsMod.ModName}.Configuration.MaxParticles.Label")]
    [Tooltip($"$Mods.{AllBeginningsMod.ModName}.Configuration.MaxParticles.Tooltip")]
    public int MaxParticles;

    [DefaultValue(true)]
    [BackgroundColor(255, 69, 103)]
    [Label($"$Mods.{AllBeginningsMod.ModName}.Configuration.CustomWindowTitles.Label")]
    [Tooltip($"$Mods.{AllBeginningsMod.ModName}.Configuration.CustomWindowTitles.Tooltip")]
    public bool CustomWindowTitles;
}
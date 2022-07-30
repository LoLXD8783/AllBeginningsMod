using Terraria.ModLoader;

namespace AllBeginningsMod;

public sealed class AllBeginningsMod : Mod
{
    public const string ModPrefix = nameof(AllBeginningsMod);
    
    public const string AssetsPath = ModPrefix + "Assets/";

    public const string SoundsPath = AssetsPath + "Sounds/";
    public const string ExtrasPath = AssetsPath + "Extras/";
    public const string EffectsPath = AssetsPath + "Effects/";

    public static AllBeginningsMod Instance => ModContent.GetInstance<AllBeginningsMod>();
}
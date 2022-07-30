using Terraria.ModLoader;

namespace AllBeginningsMod;

public sealed class AllBeginningsMod : Mod
{
    public const string ModName = nameof(AllBeginningsMod);
    
    public const string AssetsPath = ModName + "/Assets/";

    public const string SoundsPath = AssetsPath + "Sounds/";
    public const string ExtrasPath = AssetsPath + "Extras/";
    public const string EffectsPath = AssetsPath + "Effects/";

    public const string SamplesPath = ExtrasPath + "Samples/";
    public const string BossChecklistPath = ExtrasPath + "BossChecklist/";

    public static AllBeginningsMod Instance => ModContent.GetInstance<AllBeginningsMod>();
}
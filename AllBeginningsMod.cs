using Terraria.ModLoader;

namespace AllBeginningsMod;

public sealed class AllBeginningsMod : Mod
{
    public const string ModName = nameof(AllBeginningsMod);
    public const string ModPrefix = ModName + ":";

    public static AllBeginningsMod Instance => ModContent.GetInstance<AllBeginningsMod>();
}
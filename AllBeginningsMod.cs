using AllBeginningsMod.Common.Graphics;
using Terraria.ModLoader;

namespace AllBeginningsMod;

public sealed class AllBeginningsMod : Mod
{
    public static PrimitiveDrawing PrimitiveDrawing => ModContent.GetInstance<PrimitiveDrawing>();
    
    public const string ModName = nameof(AllBeginningsMod);
    public const string ModPrefix = ModName + ":";
}
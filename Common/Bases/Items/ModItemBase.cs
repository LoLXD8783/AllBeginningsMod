using AllBeginningsMod.Utility.Extensions;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Items;

public abstract class ModItemBase : ModItem
{
    public override string Texture => GetType().FullName.GetTexturePath();
}
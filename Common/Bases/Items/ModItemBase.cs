using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Items;

public abstract class ModItemBase : ModItem
{
    public override string Texture => $"{GetType().Namespace.Replace(".", "/").Replace("Content", "Assets")}/{GetType().Name}";
}
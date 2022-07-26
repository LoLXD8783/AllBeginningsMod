using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Dusts;

public abstract class ModDustBase : ModDust
{
    public override string Texture => $"{GetType().Namespace.Replace(".", "/").Replace("Content", "Assets")}/{GetType().Name}";
}
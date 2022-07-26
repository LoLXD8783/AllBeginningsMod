using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Buffs;

public abstract class ModBuffBase : ModBuff
{
    public override string Texture => $"{GetType().Namespace.Replace(".", "/").Replace("Content", "Assets")}/{GetType().Name}";
}
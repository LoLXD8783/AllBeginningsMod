using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Buffs;

public abstract class ModBuffBase : ModBuff
{
    public override string Texture => GetType().FullName.Replace('.', '/').Replace("Content", "Assets");
}
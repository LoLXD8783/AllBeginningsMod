using AllBeginningsMod.Utility.Extensions;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Dusts;

public abstract class ModDustBase : ModDust
{
    public override string Texture => GetType().FullName.Replace('.', '/').Replace("Content", "Assets");
}
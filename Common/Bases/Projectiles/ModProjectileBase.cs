using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Projectiles;

public abstract class ModProjectileBase : ModProjectile
{
    public override string Texture => GetType().FullName.Replace('.', '/').Replace("Content", "Assets");
}
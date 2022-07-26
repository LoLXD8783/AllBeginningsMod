using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Projectiles;

public abstract class ModProjectileBase : ModProjectile
{
    public override string Texture => $"{GetType().Namespace.Replace(".", "/").Replace("Content", "Assets")}/{GetType().Name}";
}
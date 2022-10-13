using AllBeginningsMod.Utility.Extensions;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Projectiles;

public abstract class ModProjectileBase : ModProjectile
{
    /// <summary>
    /// Represents the owner of this projectile.
    /// </summary>
    protected Player Owner => Main.player[Projectile.owner];
    
    public override string Texture => GetType().FullName.Replace('.', '/').Replace("Content", "Assets");
}
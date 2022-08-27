using AllBeginningsMod.Common.Bases.Projectiles;
using AllBeginningsMod.Content.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Ranged
{
    public sealed class IcicleClimbingAxeClusterProjectile : ModProjectileBase
    {
        public override void SetDefaults() {
            Projectile.width = 92;
            Projectile.height = 102;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 20;
            Projectile.penetrate = -1;
            Projectile.scale = 0.5f;
            Projectile.aiStyle = -1;
        }
        public override void AI() {
            Projectile.scale += 0.05f;
            Projectile.alpha += 12;
        }
    }
}

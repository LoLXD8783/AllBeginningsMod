using AllBeginningsMod.Common.Bases.Projectiles;
using Terraria;
using Terraria.ID;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public sealed class LeekSwordProjectile : HeldProjectileBase
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Leek Sword");
        }

        public override void SetDefaults() {
            ChannelDependant = true;

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;

            Projectile.width = 40;
            Projectile.height = 46;

            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.AddBuff(BuffID.DryadsWardDebuff, 120);
            target.AddBuff(BuffID.BrokenArmor, 360);
        }
    }
}
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public sealed class LeekSwordProjectile : ModProjectile
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Leek Sword");
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;

            Projectile.width = 40;
            Projectile.height = 46;

            Projectile.timeLeft = 20;
            Projectile.penetrate = -1;

            Projectile.aiStyle = -1;
        }

        public override void AI() {
            Player player = Main.player[Projectile.owner];

            player.armorEffectDrawShadow = true;
            player.immune = true;

            player.heldProj = Projectile.whoAmI;
            player.velocity = player.DirectionTo(Main.MouseWorld) * Projectile.timeLeft * 2f;

            player.immuneTime = Projectile.timeLeft;

            if (Projectile.timeLeft == 15) {
                Projectile.rotation = (Main.MouseWorld - player.Center).ToRotation() + MathHelper.PiOver4 + MathHelper.Pi;
            }

            Projectile.Center = player.Center;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.AddBuff(BuffID.DryadsWardDebuff, 120);
            target.AddBuff(BuffID.BrokenArmor, 360);
        }

        public override void Kill(int timeLeft) {
            Player player = Main.LocalPlayer;
            player.velocity = Vector2.Zero;
        }
    }
}
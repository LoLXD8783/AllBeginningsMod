using AllBeginningsMod.Common.Bases.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Summon
{
    public sealed class PlumeWhipProjectile : WhipProjectileBase
    {
        public override int HeadHeight { get; } = 24;

        public override int ChainHeight { get; } = 12;

        public override int HandleWidth { get; } = 20;
        public override int HandleHeight { get; } = 26;

        public override Color BackLineColor { get; } = new(202, 151, 100);

        public override void SetStaticDefaults() {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Plume Whip");
        }

        public override void SetDefaults() {
            base.SetDefaults();

            Projectile.WhipSettings.Segments = 14;
            Projectile.WhipSettings.RangeMultiplier = 1f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            Player player = Main.player[Projectile.owner];
            for (int i = 0; i < 2; i++) {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), target.Center, new Vector2(Main.rand.Next(3, 5) * player.direction, Main.rand.Next(-4, 4)), ModContent.ProjectileType<PlumeWhipFeather>(), damage/2, 0);
            }
        }
    }
}
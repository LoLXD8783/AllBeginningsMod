using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public sealed class LeekShieldProjectile : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Leek Shield");

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.width = 30;
            Projectile.height = 30;

            Projectile.timeLeft = 45;

            Projectile.aiStyle = -1;
        }
        public override void AI()
        {
            Projectile.velocity *= 0.94f;
            Projectile.rotation += 0.2f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => target.AddBuff(BuffID.DryadsWardDebuff, 120);
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < Main.rand.Next(3, 5); i++)
            {
                Player player = Main.player[Projectile.owner];
                Vector2 vel = new Vector2(Main.rand.Next(-6, 6), Main.rand.Next(-6, 6));
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<LeekShieldReturningProjectile>(), Projectile.damage/2, Projectile.knockBack, player.whoAmI);
            }

            for (int i = 0; i < Main.rand.Next(5, 8); i++)
            {
                Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.Grass, Main.rand.Next(-3, 3), Main.rand.Next(-2, 2)).noGravity = true;
            }
        }
    }
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public sealed class LeekShieldProjectile : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Leek Shield");

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;

            Projectile.width = 30;
            Projectile.height = 30;

            Projectile.timeLeft = 800;
            Projectile.penetrate = -1;

            Projectile.aiStyle = ProjAIStyleID.Boomerang;
            AIType = ProjectileID.WoodenBoomerang;
        }
    }
}

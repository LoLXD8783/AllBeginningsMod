using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public sealed class GraveShieldThrownProjectile : ModProjectile
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Grave Shield");

            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 10;
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.width = 32;
            Projectile.height = 32;

            Projectile.timeLeft = 800;
            Projectile.aiStyle = ProjAIStyleID.Boomerang;
            AIType = ProjectileID.WoodenBoomerang;
        }

        int timer;
        public override void AI() {
            timer++;
            if (timer > 3)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, -Projectile.velocity/2).noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            Player player = Main.player[Projectile.owner];

            for (int i = 0; i < 30; i++) {
                float rotation = MathHelper.TwoPi * i / 30;
                Vector2 velocity = rotation.ToRotationVector2() * 5;
                Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, velocity).noGravity = true;
            }

            for (int i = 0; i < 3; i++) {
                Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(Main.rand.Next(-3, 3), Main.rand.Next(-5, -1)), ModContent.ProjectileType<GraveShieldBoulderProjectile>(), Projectile.damage, Projectile.knockBack*2, player.whoAmI).frame = Main.rand.Next(3);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity) {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            return true;
        }

        public override bool PreDraw(ref Color lightColor) {
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = Projectile.Hitbox.Size() / 2f;

            for (var i = 0; i < ProjectileID.Sets.TrailCacheLength[Type]; i += 2) {
                Vector2 position = Projectile.oldPos[i] - Main.screenPosition + origin + new Vector2(0f, Projectile.gfxOffY);
                var alpha = 0.8f - 0.2f * (i / 2f);

                Main.EntitySpriteDraw(texture, position, null, lightColor * alpha, Projectile.oldRot[i], origin, Projectile.scale, effects, 0);
            }

            return true;
        }
    }
}
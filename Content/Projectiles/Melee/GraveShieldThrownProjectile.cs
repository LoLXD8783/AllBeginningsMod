using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
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

            Projectile.timeLeft = 300;
            Projectile.aiStyle = ProjAIStyleID.Boomerang;
            AIType = ProjectileID.WoodenBoomerang;
        }

        public override void AI() {
            if (Main.GameUpdateCount % 3f == 0f) {
                Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, -Projectile.velocity / 2f).noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            for (int i = 0; i < 30; i++) {
                float rotation = MathHelper.TwoPi * i / 30f;
                Vector2 velocity = rotation.ToRotationVector2() * 5f;

                Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, velocity).noGravity = true;
            }

            for (int i = 0; i < 3; i++) {
                IEntitySource source = new EntitySource_OnHit(Projectile, target);
                Vector2 velocity = new(Main.rand.Next(-3, 3), Main.rand.Next(-5, -1));

                Projectile.NewProjectile(source, Projectile.Center, velocity, ModContent.ProjectileType<GraveShieldBoulderProjectile>(), Projectile.damage, Projectile.knockBack * 2f, Projectile.owner);
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

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type]; i += 2) {
                Vector2 position = Projectile.oldPos[i] - Main.screenPosition + origin + new Vector2(0f, Projectile.gfxOffY);
                float alpha = 0.8f - 0.2f * (i / 2f);

                Main.EntitySpriteDraw(texture, position, null, lightColor * alpha, Projectile.oldRot[i], origin, Projectile.scale, effects, 0);
            }
            return true;
        }
    }
}
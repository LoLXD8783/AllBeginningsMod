using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee.Miscellaneous
{
    public sealed class LeekShieldThrownProjectile : ModProjectile
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Leek Shield");

            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 10;
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.width = 32;
            Projectile.height = 32;

            Projectile.timeLeft = 45;
            Projectile.aiStyle = -1;
            AIType = -1;
        }

        public override void AI() {
            Projectile.direction = Projectile.velocity.X < 0f ? -1 : 1;
            Projectile.spriteDirection = Projectile.direction;

            Projectile.velocity *= 0.95f;
            Projectile.rotation += Projectile.velocity.X * 0.1f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            return true;
        }

        public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Grass, Projectile.position);

            var splitCount = Main.rand.Next(3, 5);

            for (var i = 0; i < splitCount; i++) {
                var velocity = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, ModContent.ProjectileType<LeekShieldReturningProjectile>(), Projectile.damage / splitCount, Projectile.knockBack, Projectile.owner);
            }

            for (var i = 0; i < 10; i++) {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Grass);
                dust.noGravity = true;
            }
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
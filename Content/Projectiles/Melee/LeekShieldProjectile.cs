using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee
{
    public sealed class LeekShieldProjectile : ModProjectile
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Leek Shield");

            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 10;
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.width = 30;
            Projectile.height = 30;

            Projectile.timeLeft = 45;
            Projectile.aiStyle = -1;
        }

        public override void AI() {
            Projectile.velocity *= 0.95f;
            Projectile.rotation += Projectile.velocity.X * 0.1f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.AddBuff(BuffID.DryadsWardDebuff, 120);
        }

        public override void Kill(int timeLeft) {
            int splitCount = Main.rand.Next(3, 5);

            for (int i = 0; i < splitCount; i++) {
                Vector2 velocity = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, ModContent.ProjectileType<LeekShieldReturningProjectile>(), Projectile.damage / splitCount, Projectile.knockBack, Projectile.owner);
            }

            for (int i = 0; i < 10; i++) {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Grass);
                dust.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor) {
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = Projectile.Hitbox.Size() / 2f;

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type]; i += 2) {
                Vector2 position = Projectile.oldPos[i] - Main.screenPosition + origin + new Vector2(0f, Projectile.gfxOffY);
                float alpha = 0.9f - 0.2f * (i / 2f);

                Main.EntitySpriteDraw(texture, position, null, lightColor * alpha, Projectile.oldRot[i], origin, Projectile.scale, effects, 0);
            }

            return true;
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Ranged
{
    public sealed class BorealTurnipProjectile : ModProjectile
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Boreal Turnip");

            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 10;
        }

        public override void SetDefaults() {
            Projectile.width = 32;
            Projectile.height = 32;

            Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            return true;
        }

        public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            float maxNPCDistance = 512f * 512f;

            for (int i = 0; i < Main.maxNPCs; i++) {
                NPC npc = Main.npc[i];
                
                if (npc != target && npc.CanBeChasedBy()) {
                    float currentNPCDistance = npc.DistanceSQ(Projectile.Center);

                    if (currentNPCDistance < maxNPCDistance) {
                        maxNPCDistance = currentNPCDistance;

                        Vector2 velocity = Projectile.DirectionTo(npc.Center);
                        velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5f));
                        velocity *= Projectile.velocity.Length();
                        Projectile.velocity = velocity;
                    }
                }
            }

            target.AddBuff(BuffID.Frostburn, 135);
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
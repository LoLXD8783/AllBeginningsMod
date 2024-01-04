using AllBeginningsMod.Common.Bases.Projectiles;
using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Ranged
{
    internal class BIGGunHeldProjectile : HeldProjectile
    {
        private int aiTimer;
        private float maxNormalShootTime = 18f;
        private float maxAltShootTime = 60f;
        private float altReleaseTime;
        private Vector2 recoil;
        private BIGGunBigRocketProjectile bigRocket;

        public override string Texture => base.Texture.Replace("HeldProjectile", "Item");

        public override void SetDefaults_HeldProjectile() {
            despawnMode = DespawnMode.Manual;
        }

        public override void AI() {
            SetRelativePosition(-14 * Player.direction);

            if (Player.altFunctionUse == 2) {
                DoAltShoot();
                recoil *= 0.95f;
            } else {
                DoNormalShoot();
                recoil *= 0.8f;
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + recoil.Y;
            Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.PiOver2);

            aiTimer++;
        }

        private void DoNormalShoot() {
            if (Main.myPlayer == Player.whoAmI) {
                if (aiTimer < maxNormalShootTime && aiTimer % 6 == 0) {
                    Helper.NewProjectileCheckCollision(
                        Projectile.GetSource_FromAI(),
                        Projectile.Center,
                        Projectile.velocity * 30f,
                        Projectile.velocity * 18f,
                        ModContent.ProjectileType<BIGGunSmallRocketProjectile>(),
                        Projectile.damage,
                        Projectile.knockBack,
                        Projectile.owner
                    );

                    SoundEngine.PlaySound(SoundID.DD2_BallistaTowerShot, Projectile.Center);

                    recoil = new Vector2(10f, -0.05f * Player.direction);
                }
            }

            if (aiTimer > maxNormalShootTime + 5) {
                if (ShouldDespawn(DespawnMode.Channel)) {
                    Despawn();
                } else {
                    aiTimer = -1;
                    Player.direction = MathF.Sign(Main.MouseWorld.X - Player.Center.X);
                }
            }
        }

        private void DoAltShoot() {
            Vector2 bigRocketPosition = Projectile.Center + Projectile.velocity * 110f;
            bigRocket ??= Projectile.NewProjectileDirect(
                Projectile.GetSource_FromAI(),
                bigRocketPosition,
                Vector2.Zero,
                ModContent.ProjectileType<BIGGunBigRocketProjectile>(),
                Projectile.damage * 10,
                Projectile.knockBack * 2f,
                Projectile.owner
            ).ModProjectile as BIGGunBigRocketProjectile;

            if (bigRocket.isConnected) {
                bigRocket.Projectile.spriteDirection = Player.direction;
                bigRocket.Projectile.Center = bigRocketPosition;
                bigRocket.Projectile.velocity = Projectile.velocity * 15f;
            }

            Player.direction = MathF.Sign(Main.MouseWorld.X - Player.Center.X);

            if (ShouldDespawn(DespawnMode.Channel)) {
                if (aiTimer >= maxAltShootTime) {
                    if (altReleaseTime == 0) {
                        altReleaseTime = aiTimer;
                        bigRocket.isConnected = false;
                        bigRocket.Projectile.tileCollide = true;
                        bigRocket.Projectile.friendly = true;
                        recoil = new Vector2(30, -0.3f * Player.direction);
                    }
                } else {
                    Despawn();
                }
            }

            if (altReleaseTime != 0 && aiTimer > altReleaseTime + 30) {
                Despawn();
            }
        }

        public override void OnKill(int timeLeft) {
            if (bigRocket is not null && bigRocket.isConnected) {
                bigRocket.Projectile.active = false;
            }
        }

        public override bool PreDraw(ref Color lightColor) {
            Vector2 origin = new(10 + recoil.X, 6);
            DrawHeldProjectile(
                TextureAssets.Projectile[Type].Value, 
                Projectile.Center - Main.screenPosition, 
                lightColor,
                Projectile.rotation,
                origin
            );

            if (Player.altFunctionUse == 2) {
                Texture2D glowTexture = ModContent.Request<Texture2D>(Texture + "_Glow", AssetRequestMode.ImmediateLoad).Value;
                DrawHeldProjectile(
                    glowTexture,
                    Projectile.Center - Main.screenPosition,
                    Color.White * (aiTimer / maxAltShootTime) * (bigRocket.isConnected ? 1f : 0f),
                    Projectile.rotation,
                    origin
                );
            } else {
                Texture2D glowTexture = ModContent.Request<Texture2D>(Texture + "_Glow", AssetRequestMode.ImmediateLoad).Value;
                DrawHeldProjectile(
                    glowTexture,
                    Projectile.Center - Main.screenPosition,
                    Color.White * (recoil.X / 10f),
                    Projectile.rotation,
                    origin
                );
            }

            return false;
        }
    }
}

using AllBeginningsMod.Content.CameraModifiers;
using AllBeginningsMod.Content.Projectiles;
using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Ranged
{
    internal class BIGGunSmallRocketProjectile : ModProjectile
    {
        public override void SetDefaults() {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 800;
        }

        public override void AI() {
            Projectile.velocity.Y += 0.2f;
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void OnKill(int timeLeft) {
            Main.instance.CameraModifiers.Add(new ExplosionShakeCameraModifier(4f, 0.9f, Projectile.Center, 10_000));

            if (Main.myPlayer != Projectile.owner) {
                return;
            }

            Helper.ForEachNPCInRange(
                Projectile.Center,
                150,
                npc => {
                    Player player = Main.player[Projectile.owner];
                    if (!npc.CanBeDamagedByPlayer(player)) {
                        return;
                    }

                    player.ApplyDamageToNPC(
                        npc,
                        Projectile.damage,
                        Projectile.knockBack,
                        MathF.Sign(Projectile.Center.X - npc.Center.X),
                        false,
                        DamageClass.Ranged
                    );
                }
            );

            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.Center);

            for (int i = 0; i < 15; i++) {
                Dust.NewDust(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    Main.rand.NextFromList(DustID.Dirt, DustID.Stone),
                    Main.rand.NextFloat(-10f, 10f),
                    Main.rand.NextFloat(-8f, -2f)
                );
            }

            ExplosionVFXProjectile.Spawn(
                Projectile.GetSource_Death(),
                Projectile.Center,
                Color.Yellow,
                Color.Yellow,
                factor => Color.Lerp(Color.Orange, Color.Black, factor),
                Main.rand.Next(120, 180),
                Main.rand.Next(80, 90)
            );
        }

        public override bool PreDraw(ref Color lightColor) {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Main.spriteBatch.Draw(
                texture,
                Projectile.Center - Main.screenPosition,
                null,
                lightColor,
                Projectile.rotation,
                texture.Size() / 2f,
                Projectile.scale,
                SpriteEffects.None,
                0f
            );
            return false;
        }
    }
}

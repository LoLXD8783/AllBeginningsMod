using AllBeginningsMod.Content.CameraModifiers;
using AllBeginningsMod.Content.Projectiles;
using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;

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
            Main.instance.CameraModifiers.Add(new ExplosionShakeCameraModifier(3f, 0.9f));

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

            ExplosionVFXProjectile.Spawn(
                Projectile.GetSource_Death(),
                Projectile.Center,
                Color.Yellow,
                Color.Yellow,
                factor => Color.Lerp(Color.Orange, Color.Black, factor),
                150,
                Main.rand.Next(140, 170)
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

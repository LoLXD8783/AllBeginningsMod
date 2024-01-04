using AllBeginningsMod.Content.CameraModifiers;
using AllBeginningsMod.Content.Projectiles;
using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
using static tModPorter.ProgressUpdate;

namespace AllBeginningsMod.Content.Items.Weapons.Ranged
{
    internal class BIGGunBigRocketProjectile : ModProjectile
    {
        public bool isConnected = true;
        public override void SetDefaults() {
            Projectile.width = 70;
            Projectile.height = 70;
            Projectile.aiStyle = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.timeLeft = 800;
        }

        public override bool ShouldUpdatePosition() {
            return !isConnected;
        }

        public override void AI() {
            if (!isConnected) {
                Projectile.velocity.Y += 0.2f;
            }

            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void OnKill(int timeLeft) {
            Main.instance.CameraModifiers.Add(new ExplosionShakeCameraModifier(25f, 0.9f));
            if (Main.myPlayer != Projectile.owner) {
                return;
            }

            Helper.ForEachNPCInRange(
                Projectile.Center,
                800,
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
                factor => Color.Lerp(Color.Black, Color.SlateGray, factor), 
                1400,
                Main.rand.Next(140, 170)
            );
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI) {
            overPlayers.Add(index);
        }

        public override bool PreDraw(ref Color lightColor) {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Main.spriteBatch.Draw(
                texture,
                Projectile.Center - Main.screenPosition,
                null,
                lightColor,
                Projectile.spriteDirection == 1 ? Projectile.rotation : Projectile.rotation + MathHelper.Pi,
                texture.Size() / 2f,
                Projectile.scale,
                Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                0f
            );
            return false;
        }
    }
}

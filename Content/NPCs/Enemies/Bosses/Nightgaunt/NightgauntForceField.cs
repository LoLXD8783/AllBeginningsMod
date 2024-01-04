using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Reflection.Metadata;
using AllBeginningsMod.Utilities;

namespace AllBeginningsMod.Content.NPCs.Enemies.Bosses.Nightgaunt
{
    internal class NightgauntForceField : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_0";
        public override void SetDefaults() {
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.width = Projectile.height = 0;
            Projectile.timeLeft = 2;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
        }

        private bool dissapear;
        public override void AI() {
            NPC npc = Main.npc[(int)Projectile.ai[0]];
            if (npc is null || npc.ModNPC is not NightgauntNPC nightgaunt || !npc.active) {
                Projectile.active = false;
                return;
            }

            Projectile.timeLeft = 2;

            Projectile.Center = nightgaunt.NPC.Center;
            if (nightgaunt.Attack == NightgauntNPC.Attacks.Shield && !dissapear) {
                if (Projectile.alpha > 0) {
                    Projectile.alpha -= 10;
                }
            } else {
                dissapear = true;
            }

            Projectile.width = Projectile.height = (int)MathHelper.Lerp(0, 400, 1f - Projectile.alpha / 255f);

            if (dissapear) {
                Projectile.alpha += 10;
                if (Projectile.alpha >= 255) {
                    Projectile.active = false;
                    return;
                }
            }

            for (int i = 0; i < Main.maxProjectiles; i++) {
                Projectile projectile = Main.projectile[i];
                if (projectile is null || !projectile.active || projectile.whoAmI == Projectile.whoAmI || projectile.hostile) {
                    continue;
                }

                if (Colliding(Projectile.Hitbox, projectile.Hitbox).Value) {
                    Vector2 direction = Projectile.Center.DirectionTo(projectile.Center);
                    npc.ReflectProjectile(projectile);
                }
            }

            if (!Main.dedServ) {
                Lighting.AddLight(Projectile.Center, new Vector3(4f, 4f, 5f) * (1f - Projectile.alpha / 255f));
            }
        }

        public override bool ShouldUpdatePosition() {
            return false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
            return targetHitbox.Intersects(projHitbox.Center, Projectile.width / 2);
        }

        private Effect effect;
        public override bool PreDraw(ref Color lightColor) {
            Color color = new Color(185, 140, 183) * (1f - Projectile.alpha / 255f) * 0.3f;
            Texture2D noiseTexture = Mod.Assets.Request<Texture2D>("Assets/Images/Sample/Noise6", AssetRequestMode.ImmediateLoad).Value;

            SpriteBatchSnapshot snapshot = Main.spriteBatch.Capture();
            effect ??= Mod.Assets.Request<Effect>("Assets/Effects/ForceField", AssetRequestMode.ImmediateLoad).Value;
            effect.Parameters["size"].SetValue(0.3f);
            effect.Parameters["time"].SetValue(Main.GameUpdateCount * 0.08f);
            effect.Parameters["intensity"].SetValue(2f);
            effect.Parameters["fishEye"].SetValue(1.5f);
            effect.Parameters["sampleOpacity"].SetValue(0.35f);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(snapshot with { Effect = effect });

            Main.spriteBatch.Draw(
                noiseTexture,
                Projectile.Hitbox.Modified((int)-Main.screenPosition.X, (int)-Main.screenPosition.Y, 0, 0),
                color
            );

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(snapshot);
            return false;
        }
    }
}

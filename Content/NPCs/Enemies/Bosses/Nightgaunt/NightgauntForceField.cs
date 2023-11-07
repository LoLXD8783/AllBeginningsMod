using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using AllBeginningsMod.Utilities.Extensions;
using Terraria;
using Terraria.GameContent;
using AllBeginningsMod.Common.Graphics;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Reflection.Metadata;

namespace AllBeginningsMod.Content.NPCs.Enemies.Bosses.Nightgaunt
{
    internal class NightgauntForceField : ModProjectile
    {
        private List<(int timeLeft, float angle)> hits;
        private const int MaxTimeLeftHit = 60;
        public override string Texture => "Terraria/Images/Item_0";
        public override void SetDefaults() {
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.width = Projectile.height = 400;
            Projectile.timeLeft = 2;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;

            hits = new();
        }

        private bool dissapear;
        public override void AI() {
            NPC npc = Main.npc[(int)Projectile.ai[0]];
            if (npc is null || npc.ModNPC is not NightgauntNPC nightgaunt) {
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

            if (dissapear) {
                Projectile.alpha += 10;
                if (Projectile.alpha >= 255) {
                    Projectile.active = false;
                    return;
                }
            }

            for (int i = 0; i < Main.maxProjectiles; i++) {
                Projectile projectile = Main.projectile[i];
                if (projectile is null || !projectile.active || projectile.whoAmI == Projectile.whoAmI) {
                    continue;
                }

                if (Colliding(Projectile.Hitbox, projectile.Hitbox).Value) {
                    Vector2 direction = Projectile.Center.DirectionTo(projectile.Center);
                    projectile.velocity = direction * projectile.velocity.Length();
                    hits.Add((MaxTimeLeftHit, direction.ToRotation()));
                }
            }

            for (int i = 0; i < hits.Count; i++) {
                hits[i] = hits[i] with { timeLeft = hits[i].timeLeft - 1 };
                if (hits[i].timeLeft <= 0) {
                    hits.RemoveAt(i);
                    i--;
                }
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
            Color color = new Color(185, 140, 183) * (1f - Projectile.alpha / 255f) * 0.5f;
            Texture2D noiseTexture = Mod.Assets.Request<Texture2D>("Assets/Images/Pebbles", AssetRequestMode.ImmediateLoad).Value;

            SpriteBatchSnapshot snapshot = Main.spriteBatch.Capture();
            effect ??= Mod.Assets.Request<Effect>("Assets/Effects/ForceField", AssetRequestMode.ImmediateLoad).Value;
            effect.Parameters["size"].SetValue(0.2f);
            effect.Parameters["time"].SetValue(Main.GameUpdateCount * 0.08f);
            effect.Parameters["intensity"].SetValue(2f);
            effect.Parameters["fishEye"].SetValue(-0.2f);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(snapshot with { Effect = effect, BlendState = BlendState.Additive });

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

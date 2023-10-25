using AllBeginningsMod.Common.Graphics;
using AllBeginningsMod.Utilities.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.NPCs.Enemies.Bosses.Bastroboy
{
    internal class BastroboyOrbitingProjectile : ModProjectile
    {
        public override void SetStaticDefaults() {
            ProjectileID.Sets.TrailCacheLength[Type] = 10;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override string Texture => base.Texture.Replace("OrbitingProjectile", "_Star");
        private Effect effect;
        public override void SetDefaults() {
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(20, 20);
            Projectile.penetrate = -1;
            Projectile.hide = true;
        }

        private readonly float travelSpeed = 0.08f;
        private readonly float travelRadius = 80f;
        public override void AI() {
            NPC bastroboyNPC = null;
            for (int i = 0; i < Main.maxNPCs && bastroboyNPC is null; i++) {
                if (Main.npc[i] is not null && Main.npc[i].active && Main.npc[i].ModNPC is Bastroboy) {
                    bastroboyNPC = Main.npc[i];
                }
            }

            if (bastroboyNPC is null) {
                return;
            }

            float t = (Projectile.ai[0] == 0 ? 1f : -1f) * Main.GameUpdateCount * travelSpeed + Projectile.ai[0] * MathHelper.Pi;
            Projectile.Center = bastroboyNPC.Center + new Vector2(1.25f * MathF.Cos(t), MathF.Sin(t) * MathF.Cos(t)) * travelRadius;
            Projectile.timeLeft = 2;
            Projectile.scale = 1f + 0.2f * (MathF.Sin(Main.GameUpdateCount * 0.05f) + 1f) / 2f;

            if (Main.GameUpdateCount % 14 == 0) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.MagicMirror);
            }
            
        }

        public override bool ShouldUpdatePosition() {
            return false;
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI) {
            behindNPCs.Add(index);
        }

        public override bool PreDraw(ref Color lightColor) {
            Texture2D texture = Projectile.ai[0] == 0 ? TextureAssets.Projectile[Type].Value 
                : ModContent.Request<Texture2D>(Texture.Replace("Star", "Crescent"), AssetRequestMode.ImmediateLoad).Value;
            Texture2D glowTexture = Mod.Assets.Request<Texture2D>("Assets/Images/Glow2", AssetRequestMode.ImmediateLoad).Value;
            Color color = Projectile.ai[0] == 0 ? new Color(201, 137, 193) : new Color(216, 198, 247);

            /*effect ??= Mod.Assets.Request<Effect>("Assets/Effects/BastroboyProjectileShader", AssetRequestMode.ImmediateLoad).Value;
            effect.Parameters["sampleTexture"].SetValue(texture);
            effect.Parameters["time"].SetValue(Main.GameUpdateCount * 0.1f);
            effect.Parameters["color"].SetValue((Projectile.ai[0] == 0 ? new Color(201, 137, 193) : new Color(216, 198, 247)).ToVector3());
            */

            SpriteBatchSnapshot snapshot = Main.spriteBatch.Capture();
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(default, BlendState.Additive, default, default, default, effect);

            for (int i = 0; i < Projectile.oldPos.Length; i++) {
                Main.spriteBatch.Draw(
                    texture,
                    Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition,
                    null,
                    color * (1f - (float)i / Projectile.oldPos.Length) * 0.3f,
                    Projectile.rotation,
                    texture.Size() * 0.5f,
                    Projectile.scale,
                    SpriteEffects.None,
                    0f
                );
            }

            Main.spriteBatch.Draw(
                glowTexture,
                Projectile.Center - Main.screenPosition,
                null,
                color * 0.45f,
                Projectile.rotation,
                glowTexture.Size() * 0.5f,
                Projectile.scale * 0.5f,
                SpriteEffects.None,
                0f
            );

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(snapshot);

            Main.spriteBatch.Draw(
                texture,
                Projectile.Center - Main.screenPosition,
                null,
                color,
                Projectile.rotation,
                texture.Size() * 0.5f,
                Projectile.scale,
                SpriteEffects.None,
                0f
            );



            return false;
        }
    }
}

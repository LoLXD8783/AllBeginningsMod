using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Projectiles
{
    public abstract class WhipProjectileBase : ModProjectile
    {
        public abstract int HandleWidth { get; }
        public abstract int HandleHeight { get; }

        protected ref float Timer => ref Projectile.ai[0];

        protected Player Owner => Main.player[Projectile.owner];

        public override void SetStaticDefaults() {
            ProjectileID.Sets.IsAWhip[Type] = true;
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;

            Projectile.width = 18;
            Projectile.height = 18;

            Projectile.penetrate = -1;
            Projectile.extraUpdates = 1;
            Projectile.localNPCHitCooldown = -1;
            Projectile.aiStyle = ProjAIStyleID.Whip;

            Projectile.WhipSettings.Segments = 16;
            Projectile.WhipSettings.RangeMultiplier = 1f;
        }

        public override bool PreDraw(ref Color lightColor) {
            List<Vector2> controlPoints = new List<Vector2>();
            Projectile.FillWhipControlPoints(Projectile, controlPoints);

            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 position = controlPoints[0];

            for (int i = 0; i < controlPoints.Count - 1; i++) {
                Rectangle frame = new(0, 0, HandleWidth, HandleHeight);
                Vector2 origin = frame.Size() / 2f;
                float scale = 1f;

                if (i == controlPoints.Count - 2) {
                    frame.Y = 74;
                    frame.Height = 48;

                    Projectile.GetWhipSettings(Projectile, out float timeToFlyOut, out int _, out float _);
                    float t = Timer / timeToFlyOut;
                    scale = MathHelper.Lerp(0.5f, 1.5f, Utils.GetLerpValue(0.1f, 0.7f, t, true) * Utils.GetLerpValue(0.9f, 0.7f, t, true));
                }
                else if (i > 10) {
                    frame.Y = 60;
                    frame.Height = 14;
                }
                else if (i > 5) {
                    frame.Y = 46;
                    frame.Height = 14;
                }
                else if (i > 0) {
                    frame.Y = 32;
                    frame.Height = 14;
                }

                Vector2 element = controlPoints[i];
                Vector2 diff = controlPoints[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2;
                Color color = Lighting.GetColor(element.ToTileCoordinates());

                Main.EntitySpriteDraw(texture, position - Main.screenPosition, frame, color, rotation, origin, scale, effects, 0);

                position += diff;
            }

            return false;
        }
    }
}
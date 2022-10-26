using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases;

public abstract class WhipProjectileBase : ModProjectile
{
    public ref float Timer => ref Projectile.ai[0];

    public abstract int HeadHeight { get; }

    public abstract int ChainHeight { get; }

    public abstract int HandleWidth { get; }
    public abstract int HandleHeight { get; }

    public abstract Color BackLineColor { get; }

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
    }

    public override bool PreDraw(ref Color lightColor) {
        List<Vector2> controlPoints = new();

        Projectile.FillWhipControlPoints(Projectile, controlPoints);

        DrawControlPointsBackLine(controlPoints);
        DrawControlPoints(controlPoints);

        return false;
    }

    protected void DrawControlPointsBackLine(List<Vector2> controlPoints) {
        Texture2D texture = TextureAssets.FishingLine.Value;

        Rectangle frame = texture.Frame();
        Vector2 origin = new(frame.Width / 2f, 2f);
        Vector2 position = controlPoints[0];

        for (int i = 0; i < controlPoints.Count - 1; i++) {
            Vector2 element = controlPoints[i];
            Vector2 diff = controlPoints[i + 1] - element;

            float rotation = diff.ToRotation() - MathHelper.PiOver2;
            Color color = Lighting.GetColor(element.ToTileCoordinates(), BackLineColor);
            Vector2 scale = new(1f, (diff.Length() + 2f) / frame.Height);

            Main.EntitySpriteDraw(texture, position - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0);

            position += diff;
        }
    }

    protected void DrawControlPoints(List<Vector2> controlPoints) {
        SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

        Texture2D texture = TextureAssets.Projectile[Type].Value;
        Vector2 position = controlPoints[0];

        for (int i = 0; i < controlPoints.Count - 1; i++) {
            Rectangle frame = new(0, 0, HandleWidth, HandleHeight);
            Vector2 origin = frame.Size() / 2f;
            float scale = 1f;

            if (i == controlPoints.Count - 2) {
                frame.Y = HandleHeight + ChainHeight * 3;
                frame.Height = HeadHeight;

                Projectile.GetWhipSettings(Projectile, out float timeToFlyOut, out int _, out float _);
                float t = Timer / timeToFlyOut;
                scale = MathHelper.Lerp(0.5f, 1.5f, Utils.GetLerpValue(0.1f, 0.7f, t, true) * Utils.GetLerpValue(0.9f, 0.7f, t, true));
            }
            else if (i > 10) {
                frame.Y = HandleHeight + ChainHeight * 2;
                frame.Height = ChainHeight;
            }
            else if (i > 5) {
                frame.Y = HandleHeight + ChainHeight;
                frame.Height = ChainHeight;
            }
            else if (i > 0) {
                frame.Y = HandleHeight;
                frame.Height = ChainHeight;
            }

            Vector2 element = controlPoints[i];
            Vector2 diff = controlPoints[i + 1] - element;

            float rotation = diff.ToRotation() - MathHelper.PiOver2;
            Color color = Lighting.GetColor(element.ToTileCoordinates());

            Main.EntitySpriteDraw(texture, position - Main.screenPosition, frame, color, rotation, origin, scale, effects, 0);

            position += diff;
        }
    }
}
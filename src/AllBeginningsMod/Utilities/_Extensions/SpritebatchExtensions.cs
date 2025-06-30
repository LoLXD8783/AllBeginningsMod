using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.GameContent;

namespace AllBeginningsMod.Utilities;

public static class SpritebatchExtensions {
        public static void Begin(this SpriteBatch spriteBatch, SpriteBatchData data) {
        spriteBatch.Begin(
            data.SortMode,
            data.BlendState,
            data.SamplerState,
            data.DepthStencilState,
            data.RasterizerState,
            data.Effect,
            data.TransformMatrix
        );
    }

    public static void EndBegin(this SpriteBatch spriteBatch, SpriteBatchData data) {
        spriteBatch.End();
        spriteBatch.Begin(data);
    }

    public static SpriteBatchData CaptureEndBegin(this SpriteBatch spriteBatch, SpriteBatchData data) {
        SpriteBatchData captureData = spriteBatch.Capture();
        spriteBatch.EndBegin(data);

        return captureData;
    }

    public static void InsertDraw(this SpriteBatch spriteBatch, SpriteBatchData data, Action<SpriteBatch> drawAction) {/*
        if ((bool)SpriteBatchCache.BeginCalled.GetValue(spriteBatch)) {
            SpriteBatchData rebeginData = spriteBatch.CaptureEndBegin(data);
            drawAction(spriteBatch);
            spriteBatch.EndBegin(rebeginData);
        }
        else {
            
        }*/

        SpriteBatchData initData = spriteBatch.CaptureEndBegin(data);
        drawAction(spriteBatch);
        spriteBatch.EndBegin(initData);
    }

    public static SpriteBatchData Capture(this SpriteBatch spriteBatch) {
        return SpriteBatchData.Capture(spriteBatch);
    }

    public static void DrawAdditive(this SpriteBatch spriteBatch,
        Texture2D texture,
        Vector2 position,
        Rectangle? source,
        Color color,
        float rotation,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects) {
        SpriteBatchData data = spriteBatch.Capture();
        spriteBatch.End();
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

        spriteBatch.Draw(
            texture,
            position,
            source,
            color,
            rotation,
            origin,
            scale,
            effects,
            0
        );

        spriteBatch.End();
        spriteBatch.Begin(data);
    }

    public static void End(this SpriteBatch spriteBatch, out SpriteBatchData snapshot) {
        snapshot = spriteBatch.Capture();
        spriteBatch.End();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void DrawLine(this SpriteBatch sb, Vector2 start, Vector2 end, Color? color = null, int width = 1, Texture2D? texture = null) {
        var offset = end - start;
        float angle = (float)Math.Atan2(offset.Y, offset.X);
        var rect = new Rectangle(
            (int)Math.Round(start.X), (int)Math.Round(start.Y),
            (int)offset.Length(), width
        );

        sb.Draw(texture ?? TextureAssets.BlackTile.Value, rect, null, color ?? Color.White, angle, Vector2.Zero, SpriteEffects.None, 0f);
    }
    
    public static void DrawRect(this SpriteBatch sb, Rectangle rect, Color? color = null, int thickness = 1, Texture2D? texture = null) {
        var finalColor = color ?? Color.White;

        sb.Draw(
            texture,
            new Rectangle(rect.X, rect.Y, rect.Width, thickness),
            finalColor
        );

        sb.Draw(
            texture,
            new Rectangle(
                rect.X,
                rect.Y + rect.Height - thickness,
                rect.Width,
                thickness
            ),
            finalColor
        );

        sb.Draw(
            texture,
            new Rectangle(
                rect.X,
                rect.Y + thickness,
                thickness,
                rect.Height - (thickness * 2)
            ),
            finalColor
        );

        sb.Draw(
            texture,
            new Rectangle(
                rect.X + rect.Width - thickness,
                rect.Y + thickness,
                thickness,
                rect.Height - (thickness * 2)
            ),
            finalColor
        );
    }
    
    public static void DrawStringOutlined(this SpriteBatch sb, DynamicSpriteFont font, string text, Vector2 position, Color color, Vector2 origin = default, Vector2? scale = null, Color? outlineColor = null) {
        Color newColor = outlineColor ?? Color.Black * 0.5f;

        scale ??= Vector2.One;

        for (int i = 0; i < 5; i++) {
            if (i == 4)
                newColor = color;

            var offset = i switch {
                0 => new Vector2(-2f, +0f),
                1 => new Vector2(+2f, +0f),
                2 => new Vector2(+0f, -2f),
                3 => new Vector2(+0f, +2f),
                _ => default,
            };

            sb.DrawString(font, text, position + offset, newColor, 0f, origin, scale.Value, SpriteEffects.None, 0f);
        }
    }
}
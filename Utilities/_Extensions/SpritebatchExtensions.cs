using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;

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
}
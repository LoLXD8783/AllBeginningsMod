using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace AllBeginningsMod.Utilities;

public readonly struct SpriteBatchData
{
    public SpriteSortMode SortMode { get; init; }
    public BlendState BlendState { get; init; }
    public SamplerState SamplerState { get; init; }
    public DepthStencilState DepthStencilState { get; init; }
    public RasterizerState RasterizerState { get; init; }
    public Effect Effect { get; init; }
    public Matrix TransformMatrix { get; init; }

    public SpriteBatchData(
        SpriteSortMode sortMode,
        BlendState blendState,
        SamplerState samplerState,
        DepthStencilState depthStencilState,
        RasterizerState rasterizerState,
        Effect effect,
        Matrix transformMatrix
    ) {
        SortMode = sortMode;
        BlendState = blendState;
        SamplerState = samplerState;
        DepthStencilState = depthStencilState;
        RasterizerState = rasterizerState;
        Effect = effect;
        TransformMatrix = transformMatrix;
    }

    public static SpriteBatchData Capture(SpriteBatch spriteBatch) {
        SpriteSortMode sortMode = (SpriteSortMode)SpriteBatchCache.SortMode.GetValue(spriteBatch);
        BlendState blendState = (BlendState)SpriteBatchCache.BlendState.GetValue(spriteBatch);
        SamplerState samplerState = (SamplerState)SpriteBatchCache.SamplerState.GetValue(spriteBatch);
        DepthStencilState depthStencilState = (DepthStencilState)SpriteBatchCache.DepthStencilState.GetValue(spriteBatch);
        RasterizerState rasterizerState = (RasterizerState)SpriteBatchCache.RasterizerState.GetValue(spriteBatch);
        Effect effect = (Effect)SpriteBatchCache.Effect.GetValue(spriteBatch);
        Matrix transformMatrix = (Matrix)SpriteBatchCache.TransformMatrix.GetValue(spriteBatch);

        return new SpriteBatchData(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
    }

    public static SpriteBatchData Default() {
        return new SpriteBatchData {
            SortMode = default,
            BlendState = default,
            SamplerState = Main.DefaultSamplerState,
            DepthStencilState = default,
            RasterizerState = Main.Rasterizer,
            Effect = null,
            TransformMatrix = Main.GameViewMatrix.TransformationMatrix
        };
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Common.Systems.Drawing.Snapshots;

public readonly struct SpriteBatchSnapshot
{
    public readonly SpriteSortMode SortMode;
    public readonly BlendState BlendState;
    public readonly SamplerState SamplerState;
    public readonly DepthStencilState DepthStencilState;
    public readonly RasterizerState RasterizerState;
    public readonly Effect Effect;
    public readonly Matrix TransformMatrix;

    public SpriteBatchSnapshot(
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

    public static SpriteBatchSnapshot Capture(SpriteBatch spriteBatch) {
        ReflectionCache.EnsureInitialized();

        SpriteSortMode sortMode = (SpriteSortMode)ReflectionCache.SortMode.GetValue(spriteBatch);
        BlendState blendState = (BlendState)ReflectionCache.BlendState.GetValue(spriteBatch);
        SamplerState samplerState = (SamplerState)ReflectionCache.SamplerState.GetValue(spriteBatch);
        DepthStencilState depthStencilState = (DepthStencilState)ReflectionCache.DepthStencilState.GetValue(spriteBatch);
        RasterizerState rasterizerState = (RasterizerState)ReflectionCache.RasterizerState.GetValue(spriteBatch);
        Effect effect = (Effect)ReflectionCache.Effect.GetValue(spriteBatch);
        Matrix transformMatrix = (Matrix)ReflectionCache.TransformMatrix.GetValue(spriteBatch);

        return new SpriteBatchSnapshot(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Common.Graphics.Snapshots;

public readonly struct SpriteBatchSnapshot
{
    /// <summary>
    /// Represents the <see cref="SpriteSortMode"/> of a captured <see cref="SpriteBatch"/>.
    /// </summary>
    public readonly SpriteSortMode SortMode;
    
    /// <summary>
    /// Represents the <see cref="BlendState"/> of a captured <see cref="SpriteBatch"/>.
    /// </summary>
    public readonly BlendState BlendState;
    
    /// <summary>
    /// Represents the <see cref="SamplerState"/> of a captured <see cref="SpriteBatch"/>.
    /// </summary>
    public readonly SamplerState SamplerState;
    
    /// <summary>
    /// Represents the <see cref="DepthStencilState"/> of a captured <see cref="SpriteBatch"/>.
    /// </summary>
    public readonly DepthStencilState DepthStencilState;
    
    /// <summary>
    /// Represents the <see cref="RasterizerState"/> of a captured <see cref="SpriteBatch"/>.
    /// </summary>
    public readonly RasterizerState RasterizerState;
    
    /// <summary>
    /// Represents the <see cref="Effect"/> of a captured <see cref="SpriteBatch"/>.
    /// </summary>
    public readonly Effect Effect;
    
    /// <summary>
    /// Represents the <see cref="TransformMatrix"/> of a captured <see cref="SpriteBatch"/>.
    /// </summary>
    public readonly Matrix TransformMatrix;
    
    /// <summary>
    /// Represents the states of a <see cref="SpriteBatch"/>'s captured drawing instance.
    /// </summary>
    /// <param name="sortMode">The sort mode.</param>
    /// <param name="blendState">The blend state.</param>
    /// <param name="samplerState">The sampler state.</param>
    /// <param name="depthStencilState">The depth stencil state.</param>
    /// <param name="rasterizerState">The rasterizer state.</param>
    /// <param name="effect">The effect.</param>
    /// <param name="transformMatrix">The transform matrix.</param>
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
    
    /// <summary>
    /// Captures the current drawing instance of a <see cref="SpriteBatch"/>.
    /// </summary>
    /// <param name="spriteBatch">The spritebatch.</param>
    /// <returns>A <see cref="SpriteBatchSnapshot"/> containing the drawing states of a <see cref="SpriteBatch"/> drawing instance</returns>
    public static SpriteBatchSnapshot Capture(SpriteBatch spriteBatch) {
        SpriteBatchCache.EnsureInitialized();

        SpriteSortMode sortMode = (SpriteSortMode) SpriteBatchCache.SortMode.GetValue(spriteBatch);
        BlendState blendState = (BlendState) SpriteBatchCache.BlendState.GetValue(spriteBatch);
        SamplerState samplerState = (SamplerState) SpriteBatchCache.SamplerState.GetValue(spriteBatch);
        DepthStencilState depthStencilState = (DepthStencilState) SpriteBatchCache.DepthStencilState.GetValue(spriteBatch);
        RasterizerState rasterizerState = (RasterizerState) SpriteBatchCache.RasterizerState.GetValue(spriteBatch);
        Effect effect = (Effect) SpriteBatchCache.Effect.GetValue(spriteBatch);
        Matrix transformMatrix = (Matrix) SpriteBatchCache.TransformMatrix.GetValue(spriteBatch);

        return new SpriteBatchSnapshot(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
    }
}
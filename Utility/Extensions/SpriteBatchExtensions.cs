using AllBeginningsMod.Common.Graphics.Snapshots;
using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Utility.Extensions;

public static class SpriteBatchExtensions
{
    /// <summary>
    /// Starts a <see cref="SpriteBatch"/> drawing instance from a captured <see cref="SpriteBatchSnapshot"/>.
    /// </summary>
    /// <param name="spriteBatch">The spritebatch to be started.</param>
    /// <param name="snapshot">The captured snapshot.</param>
    public static void Begin(this SpriteBatch spriteBatch, SpriteBatchSnapshot snapshot) {
        spriteBatch.Begin(
            snapshot.SortMode,
            snapshot.BlendState,
            snapshot.SamplerState,
            snapshot.DepthStencilState,
            snapshot.RasterizerState,
            snapshot.Effect,
            snapshot.TransformMatrix
        );
    }
}
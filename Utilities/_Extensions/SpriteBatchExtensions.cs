using AllBeginningsMod.Common.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Utilities;

public static class SpriteBatchExtensions
{
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

    public static SpriteBatchSnapshot Capture(this SpriteBatch spriteBatch) {
        return SpriteBatchSnapshot.Capture(spriteBatch);
    }
}
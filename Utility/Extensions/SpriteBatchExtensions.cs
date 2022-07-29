using AllBeginningsMod.Core.Drawing.Snapshots;
using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Utility.Extensions;

public static class SpriteBatchExtensions
{
    public static void Begin(this SpriteBatch spriteBatch, SpriteBatchSnapshot snapshot) => spriteBatch.Begin(snapshot.SortMode,
        snapshot.BlendState,
        snapshot.SamplerState,
        snapshot.DepthStencilState,
        snapshot.RasterizerState,
        snapshot.Effect,
        snapshot.TransformMatrix);
}
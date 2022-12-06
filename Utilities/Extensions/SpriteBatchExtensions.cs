using AllBeginningsMod.Common.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace AllBeginningsMod.Utilities.Extensions;

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

    public static void DrawAdditive(this SpriteBatch spriteBatch,
        Texture2D texture,
        Vector2 position,
        Rectangle? source,
        Color color,
        float rotation,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects) {
        SpriteBatchSnapshot snapshit = spriteBatch.Capture();
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
        spriteBatch.Begin(snapshit);
    }
}
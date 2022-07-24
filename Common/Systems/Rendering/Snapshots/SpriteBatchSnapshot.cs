using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Common.Systems.Rendering.Snapshots
{
    public readonly struct SpriteBatchSnapshot
    {
        public readonly SpriteSortMode SortMode;
        public readonly BlendState BlendState;
        public readonly SamplerState SamplerState;
        public readonly DepthStencilState DepthStencilState;
        public readonly RasterizerState RasterizerState;
        public readonly Effect Effect;
        public readonly Matrix TransformationMatrix;

        public SpriteBatchSnapshot(
            SpriteSortMode sortMode, 
            BlendState blendState, 
            SamplerState samplerState, 
            DepthStencilState depthStencilState, 
            RasterizerState rasterizerState, 
            Effect effect, 
            Matrix transformationMatrix
            ) {
            SortMode = sortMode;
            BlendState = blendState;
            SamplerState = samplerState;
            DepthStencilState = depthStencilState;
            RasterizerState = rasterizerState;
            Effect = effect;
            TransformationMatrix = transformationMatrix;
        }

        public static SpriteBatchSnapshot Capture(SpriteBatch spriteBatch) {
            ReflectionCache.EnsureInitialized();

            return new SpriteBatchSnapshot(
                (SpriteSortMode)ReflectionCache.SortMode.GetValue(spriteBatch),
                (BlendState)ReflectionCache.BlendState.GetValue(spriteBatch),
                (SamplerState)ReflectionCache.SamplerState.GetValue(spriteBatch),
                (DepthStencilState)ReflectionCache.DepthStencilState.GetValue(spriteBatch),
                (RasterizerState)ReflectionCache.RasterizerState.GetValue(spriteBatch),
                (Effect)ReflectionCache.Effect.GetValue(spriteBatch),
                (Matrix)ReflectionCache.TransformMatrix.GetValue(spriteBatch)
            );
        }
    }
}
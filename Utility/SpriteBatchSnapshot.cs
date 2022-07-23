using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Utility
{
    public struct SpriteBatchSnapshot
    {
        public SpriteSortMode SortMode { get; set; }
        public BlendState BlendState { get; set; }
        public SamplerState SamplerState { get; set; }
        public DepthStencilState DepthStencilState { get; set; }
        public RasterizerState RasterizerState { get; set; }
        public Effect Effect { get; set; }
        public Matrix TransformationMatrix { get; set; }

        public SpriteBatchSnapshot(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix transformationMatrix) {
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
                sortMode: (SpriteSortMode)ReflectionCache.sortMode.GetValue(spriteBatch),
                blendState: (BlendState)ReflectionCache.blendState.GetValue(spriteBatch),
                samplerState: (SamplerState)ReflectionCache.samplerState.GetValue(spriteBatch),
                depthStencilState: (DepthStencilState)ReflectionCache.depthStencilState.GetValue(spriteBatch),
                rasterizerState: (RasterizerState)ReflectionCache.rasterizerState.GetValue(spriteBatch),
                effect: (Effect)ReflectionCache.customEffect.GetValue(spriteBatch),
                transformationMatrix: (Matrix)ReflectionCache.transformMatrix.GetValue(spriteBatch)
            );
        }

        public void Begin(SpriteBatch spriteBatch) {
            spriteBatch.Begin(SortMode, BlendState, SamplerState, DepthStencilState, RasterizerState, Effect, TransformationMatrix);
        }

        class ReflectionCache : ModSystem
        {
            public static bool Initialized { get; private set; }
            public static FieldInfo sortMode;
            public static FieldInfo blendState;
            public static FieldInfo samplerState;
            public static FieldInfo depthStencilState;
            public static FieldInfo rasterizerState;
            public static FieldInfo customEffect;
            public static FieldInfo transformMatrix;

            public override void Load() {
                EnsureInitialized();
            }

            public static void EnsureInitialized() {
                if (Initialized) {
                    return;
                }

                const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
                sortMode = typeof(SpriteBatch).GetField("sortMode", flags);
                blendState = typeof(SpriteBatch).GetField("blendState", flags);
                samplerState = typeof(SpriteBatch).GetField("samplerState", flags);
                depthStencilState = typeof(SpriteBatch).GetField("depthStencilState", flags);
                rasterizerState = typeof(SpriteBatch).GetField("rasterizerState", flags);
                customEffect = typeof(SpriteBatch).GetField("customEffect", flags);
                transformMatrix = typeof(SpriteBatch).GetField("transformMatrix", flags);

                Initialized = true;
            }

            public override void Unload() {
                sortMode = null;
                blendState = null;
                samplerState = null;
                depthStencilState = null;
                rasterizerState = null;
                customEffect = null;
                transformMatrix = null;
            }
        }
    }
}

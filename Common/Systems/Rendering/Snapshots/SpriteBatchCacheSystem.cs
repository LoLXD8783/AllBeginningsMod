using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Rendering.Snapshots
{
    public sealed class ReflectionCache : ModSystem
    {
        public static bool Initialized { get; private set; }

        public static FieldInfo SortMode { get; private set; }
        public static FieldInfo BlendState { get; private set; }
        public static FieldInfo SamplerState { get; private set; }
        public static FieldInfo DepthStencilState { get; private set; }
        public static FieldInfo RasterizerState { get; private set; }
        public static FieldInfo Effect { get; private set; }
        public static FieldInfo TransformMatrix { get; private set; }

        public override void OnModLoad() {
            EnsureInitialized();
        }

        public override void OnModUnload() {
            Initialized = false;

            SortMode = null;
            BlendState = null;
            SamplerState = null;
            DepthStencilState = null;
            RasterizerState = null;
            Effect = null;
            TransformMatrix = null;
        }

        public static void EnsureInitialized() {
            if (Initialized) {
                return;
            }

            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

            SortMode = typeof(SpriteBatch).GetField("sortMode", flags);
            BlendState = typeof(SpriteBatch).GetField("blendState", flags);
            SamplerState = typeof(SpriteBatch).GetField("samplerState", flags);
            DepthStencilState = typeof(SpriteBatch).GetField("depthStencilState", flags);
            RasterizerState = typeof(SpriteBatch).GetField("rasterizerState", flags);
            Effect = typeof(SpriteBatch).GetField("customEffect", flags);
            TransformMatrix = typeof(SpriteBatch).GetField("transformMatrix", flags);

            Initialized = true;
        }
    }
}
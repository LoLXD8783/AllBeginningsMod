using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terraria.ModLoader;

namespace AllBeginningsMod.Utilities;

public sealed class SpriteBatchCache : ILoadable {
    public static FieldInfo SortMode { get; private set; }
    public static FieldInfo BlendState { get; private set; }
    public static FieldInfo SamplerState { get; private set; }
    public static FieldInfo DepthStencilState { get; private set; }
    public static FieldInfo RasterizerState { get; private set; }
    public static FieldInfo Effect { get; private set; }
    public static FieldInfo TransformMatrix { get; private set; }
    public static FieldInfo BeginCalled { get; private set; }
    public static MethodInfo FlushBatch { get; private set; }

    void ILoadable.Load(Mod mod) {
        SortMode = typeof(SpriteBatch).GetField("sortMode", ReflectionUtilities.FlagsPrivateInstance);
        BlendState = typeof(SpriteBatch).GetField("blendState", ReflectionUtilities.FlagsPrivateInstance);
        SamplerState = typeof(SpriteBatch).GetField("samplerState", ReflectionUtilities.FlagsPrivateInstance);
        DepthStencilState = typeof(SpriteBatch).GetField("depthStencilState", ReflectionUtilities.FlagsPrivateInstance);
        RasterizerState = typeof(SpriteBatch).GetField("rasterizerState", ReflectionUtilities.FlagsPrivateInstance);
        Effect = typeof(SpriteBatch).GetField("customEffect", ReflectionUtilities.FlagsPrivateInstance);
        TransformMatrix = typeof(SpriteBatch).GetField("transformMatrix", ReflectionUtilities.FlagsPrivateInstance);
        BeginCalled = typeof(SpriteBatch).GetField("beginCalled", ReflectionUtilities.FlagsPrivateInstance);
        FlushBatch = typeof(SpriteBatch).GetMethod("FlushBatch", ReflectionUtilities.FlagsPrivateInstance);
    }

    void ILoadable.Unload() {
        SortMode = null;
        BlendState = null;
        SamplerState = null;
        DepthStencilState = null;
        RasterizerState = null;
        Effect = null;
        TransformMatrix = null;
    }
}
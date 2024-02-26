using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terraria.ModLoader;

namespace AllBeginningsMod.Utilities;

public sealed class SpriteBatchCache : ILoadable
{
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
        SortMode = typeof(SpriteBatch).GetField("sortMode", Helper.FlagsPrivateInstance);
        BlendState = typeof(SpriteBatch).GetField("blendState", Helper.FlagsPrivateInstance);
        SamplerState = typeof(SpriteBatch).GetField("samplerState", Helper.FlagsPrivateInstance);
        DepthStencilState = typeof(SpriteBatch).GetField("depthStencilState", Helper.FlagsPrivateInstance);
        RasterizerState = typeof(SpriteBatch).GetField("rasterizerState", Helper.FlagsPrivateInstance);
        Effect = typeof(SpriteBatch).GetField("customEffect", Helper.FlagsPrivateInstance);
        TransformMatrix = typeof(SpriteBatch).GetField("transformMatrix", Helper.FlagsPrivateInstance);
        BeginCalled = typeof(SpriteBatch).GetField("beginCalled", Helper.FlagsPrivateInstance);
        FlushBatch = typeof(SpriteBatch).GetMethod("FlushBatch", Helper.FlagsPrivateInstance);
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
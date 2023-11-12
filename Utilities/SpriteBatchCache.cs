using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
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

    void ILoadable.Load(Mod mod) {
        SortMode = typeof(SpriteBatch).GetField("sortMode", ReflectionUtils.FlagsPrivateInstance);
        BlendState = typeof(SpriteBatch).GetField("blendState", ReflectionUtils.FlagsPrivateInstance);
        SamplerState = typeof(SpriteBatch).GetField("samplerState", ReflectionUtils.FlagsPrivateInstance);
        DepthStencilState = typeof(SpriteBatch).GetField("depthStencilState", ReflectionUtils.FlagsPrivateInstance);
        RasterizerState = typeof(SpriteBatch).GetField("rasterizerState", ReflectionUtils.FlagsPrivateInstance);
        Effect = typeof(SpriteBatch).GetField("customEffect", ReflectionUtils.FlagsPrivateInstance);
        TransformMatrix = typeof(SpriteBatch).GetField("transformMatrix", ReflectionUtils.FlagsPrivateInstance);
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
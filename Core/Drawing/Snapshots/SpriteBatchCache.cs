using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AllBeginningsMod.Core.Drawing.Snapshots;

public sealed class ReflectionCache : ILoadable
{
    public static bool Initialized { get; private set; }

    public static FieldInfo SortMode { get; private set; }
    public static FieldInfo BlendState { get; private set; }
    public static FieldInfo SamplerState { get; private set; }
    public static FieldInfo DepthStencilState { get; private set; }
    public static FieldInfo RasterizerState { get; private set; }
    public static FieldInfo Effect { get; private set; }
    public static FieldInfo TransformMatrix { get; private set; }

    void ILoadable.Load(Mod mod) => EnsureInitialized();

    void ILoadable.Unload() {
        Initialized = false;

        SortMode = null;
        BlendState = null;
        SamplerState = null;
        DepthStencilState = null;
        RasterizerState = null;
        Effect = null;
        TransformMatrix = null;
    }

    internal static void EnsureInitialized() {
        if (Initialized)
            return;

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
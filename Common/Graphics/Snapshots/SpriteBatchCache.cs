using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Graphics.Snapshots;

public sealed class SpriteBatchCache : ILoadable
{
    /// <summary>
    /// Represents whether the cache has been initialized or not.
    /// </summary>
    public static bool Initialized { get; private set; }

    /// <summary>
    /// Represents the <see cref="SpriteBatch"/>'s <see cref="SpriteSortMode"/> field info.
    /// </summary>
    public static FieldInfo SortMode { get; private set; }
    
    /// <summary>
    /// Represents the <see cref="SpriteBatch"/>'s <see cref="BlendState"/> field info.
    /// </summary>
    public static FieldInfo BlendState { get; private set; }
    
    /// <summary>
    /// Represents the <see cref="SpriteBatch"/>'s <see cref="SamplerState"/> field info.
    /// </summary>
    public static FieldInfo SamplerState { get; private set; }
    
    /// <summary>
    /// Represents the <see cref="SpriteBatch"/>'s <see cref="DepthStencilState"/> field info.
    /// </summary>
    public static FieldInfo DepthStencilState { get; private set; }
    
    /// <summary>
    /// Represents the <see cref="SpriteBatch"/>'s <see cref="RasterizerState"/> field info.
    /// </summary>
    public static FieldInfo RasterizerState { get; private set; }
    
    /// <summary>
    /// Represents the <see cref="SpriteBatch"/>'s <see cref="Effect"/> field info.
    /// </summary>
    public static FieldInfo Effect { get; private set; }
    
    /// <summary>
    /// Represents the <see cref="SpriteBatch"/>'s <see cref="Matrix"/> field info.
    /// </summary>
    public static FieldInfo TransformMatrix { get; private set; }

    void ILoadable.Load(Mod mod) {
        EnsureInitialized();
    }

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
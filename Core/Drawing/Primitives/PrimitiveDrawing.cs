using System.Collections.Generic;
using AllBeginningsMod.Utility;
using AllBeginningsMod.Utility.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Core.Drawing.Primitives;

[Autoload(Side = ModSide.Client)]
public sealed class PrimitiveDrawing : ILoadable
{
    public static DynamicIndexBuffer IndexBuffer { get; private set; }
    public static DynamicVertexBuffer VertexBuffer { get; private set; }

    private static GraphicsDevice Device => Main.graphics.GraphicsDevice;

    void ILoadable.Load(Mod mod) { }

    void ILoadable.Unload() => ThreadUtils.RunOnMainThread(() => {
        IndexBuffer?.Dispose();
        IndexBuffer = null;

        VertexBuffer?.Dispose();
        VertexBuffer = null;
    });

    public static void DrawPrimitive(VertexPositionColorTexture[] vertices, short[] indices, Effect effect, PrimitiveType type) {
        if (vertices.Length <= 0 || indices.Length <= 0 || effect == null)
            return;

        if (VertexBuffer == null || VertexBuffer.VertexCount < vertices.Length) {
            VertexBuffer?.Dispose();
            VertexBuffer = new DynamicVertexBuffer(Device, VertexPositionColorTexture.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
        }

        if (IndexBuffer == null || IndexBuffer.IndexCount < indices.Length) {
            IndexBuffer?.Dispose();
            IndexBuffer = new DynamicIndexBuffer(Device, IndexElementSize.SixteenBits, indices.Length, BufferUsage.WriteOnly);
        }

        VertexBuffer.SetData(vertices, SetDataOptions.Discard);
        IndexBuffer.SetData(indices, 0, indices.Length, SetDataOptions.Discard);

        Device.SetVertexBuffer(VertexBuffer);
        Device.Indices = IndexBuffer;

        Device.RasterizerState = RasterizerState.CullNone;

        foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
            pass.Apply();
            Device.DrawIndexedPrimitives(type, 0, 0, VertexBuffer.VertexCount, 0, VertexBuffer.GetPrimitiveCount(type));
        }
    }
}
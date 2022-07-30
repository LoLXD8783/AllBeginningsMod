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
    private static GraphicsDevice Device => Main.graphics.GraphicsDevice;

    private static DynamicIndexBuffer indexBuffer;
    private static DynamicVertexBuffer vertexBuffer;

    void ILoadable.Load(Mod mod) { }

    void ILoadable.Unload() {
        ThreadUtils.RunOnMainThread(
            () => {
                indexBuffer?.Dispose();
                indexBuffer = null;

                vertexBuffer?.Dispose();
                vertexBuffer = null;
            }
        );
    }

    public static void DrawPrimitive(PrimitiveType type, VertexPositionColorTexture[] vertices, short[] indices, Effect effect) {
        if (vertices.Length <= 0 || indices.Length <= 0 || effect == null)
            return;

        if (vertexBuffer == null || vertexBuffer.VertexCount < vertices.Length) {
            vertexBuffer?.Dispose();
            vertexBuffer = new DynamicVertexBuffer(Device, VertexPositionColorTexture.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
        }

        if (indexBuffer == null || indexBuffer.IndexCount < indices.Length) {
            indexBuffer?.Dispose();
            indexBuffer = new DynamicIndexBuffer(Device, IndexElementSize.SixteenBits, indices.Length, BufferUsage.WriteOnly);
        }

        vertexBuffer.SetData(vertices, SetDataOptions.Discard);
        indexBuffer.SetData(indices, 0, indices.Length, SetDataOptions.Discard);

        Device.SetVertexBuffer(vertexBuffer);
        Device.Indices = indexBuffer;

        Device.RasterizerState = RasterizerState.CullNone;

        int primitiveCount = vertexBuffer.GetPrimitiveCount(type);

        foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
            pass.Apply();
            Device.DrawIndexedPrimitives(type, 0, 0, vertexBuffer.VertexCount, 0, primitiveCount);
        }
    }
}
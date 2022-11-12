using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace AllBeginningsMod.Common.Graphics;

public sealed class PrimitiveBuffer
{
    private static GraphicsDevice Device => Main.graphics.GraphicsDevice;

    public DynamicIndexBuffer IndexBuffer { get; private set; }
    public DynamicVertexBuffer VertexBuffer { get; private set; }

    public void PrepareForDrawing() {
        Device.SetVertexBuffer(VertexBuffer);
        Device.Indices = IndexBuffer;
        Device.RasterizerState = RasterizerState.CullNone;
    }
    
    public void SetData(VertexPositionColorTexture[] vertices, int[] indices) {
        EnsureIndexCapacity(indices.Length);
        IndexBuffer.SetData(indices, 0, indices.Length, SetDataOptions.Discard);
        
        EnsureVertexCapacity(vertices.Length);
        VertexBuffer.SetData(vertices, SetDataOptions.Discard);
    }

    private void EnsureIndexCapacity(int indexLength) {
        if (IndexBuffer == null || IndexBuffer.IndexCount < indexLength) {
            IndexBuffer?.Dispose();
            IndexBuffer = new DynamicIndexBuffer(Device, IndexElementSize.SixteenBits, indexLength, BufferUsage.WriteOnly);
        }
    }

    private void EnsureVertexCapacity(int vertexLength) {
        if (VertexBuffer == null || VertexBuffer.VertexCount < vertexLength) {
            VertexBuffer?.Dispose();
            VertexBuffer = new DynamicVertexBuffer(Device, VertexPositionColorTexture.VertexDeclaration, vertexLength, BufferUsage.WriteOnly);
        }
    }
}
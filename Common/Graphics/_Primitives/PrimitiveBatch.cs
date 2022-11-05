using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace AllBeginningsMod.Common.Graphics;

public static class PrimitiveBatch
{
    private static GraphicsDevice Device => Main.graphics.GraphicsDevice;

    private static DynamicIndexBuffer indexBuffer;
    private static DynamicVertexBuffer vertexBuffer;
    
    public static void DrawPrimitiveShape(IPrimitiveShape shape, PrimitiveType type, Effect effect) {
        EnsureCapacity(shape);
        
        vertexBuffer.SetData(shape.Vertices, SetDataOptions.Discard);
        indexBuffer.SetData(shape.Indices, 0, shape.Indices.Length, SetDataOptions.Discard);
        
        PrepareForDrawing();
        
        int primitiveCount = vertexBuffer.GetPrimitiveCount(type);

        foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
            pass.Apply();
            Device.DrawIndexedPrimitives(type, 0, 0, vertexBuffer.VertexCount, 0, primitiveCount);
        }
    }

    private static void PrepareForDrawing() {
        Device.SetVertexBuffer(vertexBuffer);
        Device.Indices = indexBuffer;
        
        Device.RasterizerState = RasterizerState.CullNone;
    }
    
    private static void EnsureCapacity<T>(T shape) where T : IPrimitiveShape {
        int vertexLength = shape.Vertices.Length;
        int indexLength = shape.Indices.Length;
        
        if (vertexBuffer == null || vertexBuffer.VertexCount < vertexLength) {
            vertexBuffer?.Dispose();
            vertexBuffer = new DynamicVertexBuffer(Device, VertexPositionColorTexture.VertexDeclaration, vertexLength, BufferUsage.WriteOnly);
        }

        if (indexBuffer == null || indexBuffer.IndexCount < indexLength) {
            indexBuffer?.Dispose();
            indexBuffer = new DynamicIndexBuffer(Device, IndexElementSize.SixteenBits, indexLength, BufferUsage.WriteOnly);
        }
    }
}
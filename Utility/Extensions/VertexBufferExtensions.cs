using Microsoft.Xna.Framework.Graphics;
using System;

namespace AllBeginningsMod.Utility.Extensions;

public static class VertexBufferExtensions
{
    public static int GetPrimitiveCount(this VertexBuffer buffer, PrimitiveType type) => type switch {
        PrimitiveType.TriangleList => buffer.VertexCount / 2,
        PrimitiveType.TriangleStrip => buffer.VertexCount - 2,
        PrimitiveType.LineList => buffer.VertexCount / 2,
        PrimitiveType.LineStrip => buffer.VertexCount - 1,
        PrimitiveType.PointListEXT => buffer.VertexCount / 3
    };
}
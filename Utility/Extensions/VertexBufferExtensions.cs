using Microsoft.Xna.Framework.Graphics;
using System;

namespace AllBeginningsMod.Utility.Extensions;

public static class VertexBufferExtensions
{
    /// <summary>
    /// Gets the proper amount of primitives to be drawn in a buffer.
    /// </summary>
    /// <param name="buffer">The vertex buffer.</param>
    /// <param name="type">The primitive type.</param>
    /// <returns></returns>
    public static int GetPrimitiveCount(this VertexBuffer buffer, PrimitiveType type) {
        return type switch {
            PrimitiveType.TriangleList => buffer.VertexCount / 2,
            PrimitiveType.TriangleStrip => buffer.VertexCount - 2,
            PrimitiveType.LineList => buffer.VertexCount / 2,
            PrimitiveType.LineStrip => buffer.VertexCount - 1,
            PrimitiveType.PointListEXT => buffer.VertexCount / 3,
            _ => 0 // throw new ArgumentException($"Unsupported primitive type: {type}", nameof(type))
        };
    }
}
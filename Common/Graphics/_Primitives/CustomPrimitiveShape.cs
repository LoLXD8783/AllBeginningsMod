using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Common.Graphics;

public readonly struct CustomPrimitiveShape : IPrimitiveShape
{
    public VertexPositionColorTexture[] Vertices { get; init; }

    public int[] Indices { get; init; }

    public CustomPrimitiveShape(VertexPositionColorTexture[] vertices, int[] indices) {
        Vertices = vertices;
        Indices = indices;
    }
}
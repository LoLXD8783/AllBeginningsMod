using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Common.Graphics.Primitives;

// TODO: Add rotation support with the tip as its rotation origin.
public sealed class TriangleShape : PrimitiveShape
{
    public override PrimitiveType Type => PrimitiveType.TriangleStrip;

    public readonly Vector2 Position;

    public readonly Color Color;

    public readonly float Width;
    public readonly float Height;

    public TriangleShape(Vector2 position, Color color, float width, float height) {
        Position = position;
        Color = color;
        Width = width;
        Height = height;
    }

    public override void SetShape(out VertexPositionColorTexture[] vertices, out short[] indices) {
        List<VertexPositionColorTexture> vertexList = new();
        List<short> indexList = new();

        vertexList.Add(new VertexPositionColorTexture(new Vector3(Position - new Vector2(Width, 0f), 0f), Color, new Vector2(0f, 1f)));
        vertexList.Add(new VertexPositionColorTexture(new Vector3(Position - new Vector2(0f, Height), 0f), Color, new Vector2(0.5f, 0f)));
        vertexList.Add(new VertexPositionColorTexture(new Vector3(Position + new Vector2(Width, 0f), 0f), Color, Vector2.One));

        indexList.Add((short) vertexList.Count);
        indexList.Add((short) (vertexList.Count + 1));
        indexList.Add((short) (vertexList.Count + 2));

        vertices = vertexList.ToArray();
        indices = indexList.ToArray();
    }
}
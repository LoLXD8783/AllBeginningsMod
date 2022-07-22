using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AllBeginningsMod.Common.Systems.Rendering.Primitives
{
    public abstract class PrimitiveBase 
    {
        public readonly int VertexCount;

        public readonly Effect Effect;

        public readonly PrimitiveType Type;

        public readonly List<Vector2> Positions;
        public readonly VertexPositionColorTexture[] Vertices;

        public PrimitiveBase(PrimitiveType type, Effect effect, int vertexCount) {
            Type = type;
            Effect = effect;
            VertexCount = vertexCount;
            Positions = new List<Vector2>(vertexCount);
            Vertices = new VertexPositionColorTexture[vertexCount];
        }

        public abstract void SetPositions();

        public abstract void SetVertices();
    }
}
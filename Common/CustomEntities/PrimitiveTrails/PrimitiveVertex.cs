using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace AllBeginningsMod.Common.CustomEntities.PrimitiveTrails
{
    public struct PrimitiveVertex
    {
        public VertexPositionColorTexture FirstVertex;
        public VertexPositionColorTexture SecondVertex;

        public Color Color;
        
        public float Alpha; 
        public float Scale;

        public PrimitiveVertex(Vector2 position, Vector2 oldPosition, Color color, float alpha, float scale)
        {
            Color = color;
            Alpha = alpha;
            Scale = scale;

            Vector2 offset = new Vector2(Scale).RotatedBy((oldPosition - position).ToRotation());

            FirstVertex = new VertexPositionColorTexture(new Vector3(position + offset, 0f), Color * Alpha, Vector2.Zero);
            SecondVertex = new VertexPositionColorTexture(new Vector3(position - offset, 0f), Color * Alpha, Vector2.One);
        }
    }
}
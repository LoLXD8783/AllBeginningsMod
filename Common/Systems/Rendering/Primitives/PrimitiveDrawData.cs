using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Common.Systems.Rendering.Primitives
{
    public record struct PrimitiveDrawData(VertexPositionColorTexture[] Vertices, Effect Effect, PrimitiveType Type);
}
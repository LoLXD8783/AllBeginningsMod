using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Core.Drawing.Primitives;

public record struct PrimitiveDrawData(VertexPositionColorTexture[] Vertices, ushort[] Indices, Effect Effect, PrimitiveType Type);
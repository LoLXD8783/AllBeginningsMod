using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Common.Systems.Drawing.Primitives;

public record struct PrimitiveDrawData(VertexPositionColorTexture[] Vertices, ushort[] Indices, Effect Effect, PrimitiveType Type);
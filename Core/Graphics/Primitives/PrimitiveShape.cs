using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Core.Drawing.Primitives;

public abstract class PrimitiveShape
{
    public abstract PrimitiveType Type { get; }

    public abstract void SetShape(out VertexPositionColorTexture[] vertices, out short[] indices);
}
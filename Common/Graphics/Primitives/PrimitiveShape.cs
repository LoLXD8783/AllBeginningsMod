using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Common.Graphics.Primitives;

public abstract class PrimitiveShape
{
    public virtual List<VertexPositionColorTexture> Vertices { get; protected set; } = new();
    public virtual List<short> Indices { get; protected set; } = new();

    public abstract PrimitiveType Type { get; }

    public virtual void SetShape() { }

    public virtual void ResetShape() {
        Vertices?.Clear();
        Vertices ??= new List<VertexPositionColorTexture>();
        
        Indices?.Clear();
        Indices ??= new List<short>();
    }
}
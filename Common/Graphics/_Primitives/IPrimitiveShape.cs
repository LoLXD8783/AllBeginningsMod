using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Common.Graphics;

public interface IPrimitiveShape
{
    public VertexPositionColorTexture[] Vertices { get; }
    
    public int[] Indices { get; }
}
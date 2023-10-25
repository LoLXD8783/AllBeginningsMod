using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBeginningsMod.Common.PrimitiveDrawing
{
    internal interface ITrailStyle
    {
        int VertexCount(int trailPositionCount);
        int IndexCount(int trailPositionCount);
        /// <summary>
        /// Use this function to set the vertices and indices.
        /// </summary>
        /// <param name="trailPositions"></param>
        /// <param name="trailWidth"></param>
        /// <param name="vertexBuffer"></param>
        /// <param name="indexBuffer"></param>
        /// <returns>Whether or not to proceed with rendering.</returns>
        void SetBuffers(Vector2[] trailPositions, Func<float, float> trailWidth, Func<Vector2, Color> trailColor, ref VertexPositionColorTexture[] vertices, ref ushort[] indices);
    }
}

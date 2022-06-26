using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;

namespace AllBeginningsMod.Common.CustomEntities.PrimitiveTrails
{
    public abstract class PrimitiveTrail : CustomEntity
    {
        public List<VertexPositionColorTexture> Vertices { get; protected set; }

        public Entity Entity { get; protected set; }
        
        public int Length { get; protected set; }
        public int Width { get; protected set; }

        public PrimitiveTrail(Entity entity, int length, int width)
        {
            Entity = entity;
            Length = length;
            Width = width;
        }

        public abstract Effect Effect { get; }

        protected static GraphicsDevice Device => Main.graphics.GraphicsDevice;
    }
}
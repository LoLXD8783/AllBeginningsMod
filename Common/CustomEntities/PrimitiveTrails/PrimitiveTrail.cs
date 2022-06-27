using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;

namespace AllBeginningsMod.Common.CustomEntities.PrimitiveTrails
{
    public abstract class PrimitiveTrail
    {
        public Entity Entity;

        public int WhoAmI;
        public int Length;
        
        public List<PrimitiveVertex> PrimitiveVertices;
        public List<VertexPositionColorTexture> Vertices;

        public PrimitiveTrail(Entity entity, int length)
        {
            Entity = entity;
            Length = length;
            PrimitiveVertices = new List<PrimitiveVertex>(Length);
            Vertices = new List<VertexPositionColorTexture>(Length);
        }

        public virtual void OnUpdate() { }

        public virtual void OnDraw() { }
    }
}
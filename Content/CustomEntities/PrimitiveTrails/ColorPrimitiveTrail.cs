using AllBeginningsMod.Common.CustomEntities.PrimitiveTrails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Runtime.InteropServices;

namespace AllBeginningsMod.Content.CustomEntities.PrimitiveTrails
{
    public sealed class ColorPrimitiveTrail : PrimitiveTrail, IDisposable
    {
        public Color Color;

        public float Alpha;
        public float Width;

        private BasicEffect effect = new(Main.graphics.GraphicsDevice)
        {
            VertexColorEnabled = true
        };

        public ColorPrimitiveTrail(Entity entity, int length, Color color, float alpha, float width) : base(entity, length)
        {
            Color = color;
            Alpha = alpha;
            Width = width; 
        }
        
        public override void OnUpdate() 
        {
            while (PrimitiveVertices.Count > Length)
            {
                PrimitiveVertices.RemoveAt(0);
            }

            Vector2 position = Entity.Center;
            Vector2 oldPosition = Entity.oldPosition + Entity.Size / 2f;

            PrimitiveVertex primitiveVertex = new(position, oldPosition, Color, Alpha, Width);
            PrimitiveVertices.Add(primitiveVertex);

            var span = CollectionsMarshal.AsSpan(PrimitiveVertices).Slice(0, PrimitiveVertices.Count);
            for (int i = 0; i < span.Length; i++)
            {
                ref PrimitiveVertex vertex = ref span[i];

                vertex.Scale = (float)i / PrimitiveVertices.Count;
                vertex.Alpha = (float)i / PrimitiveVertices.Count;
            }

            while (Vertices.Count > Length * 2)
            {
                Vertices.RemoveAt(0);
            }

            Vertices.Add(primitiveVertex.FirstVertex);
            Vertices.Add(primitiveVertex.SecondVertex);
        }
            
        public override void OnDraw()
        {
            if (Vertices.Count < 3)
            {
                return;
            }
            
            GraphicsDevice device = Main.graphics.GraphicsDevice;
            device.RasterizerState = RasterizerState.CullNone; 

            effect.Projection = Matrix.CreateOrthographicOffCenter(0f, device.Viewport.Width * 2f, device.Viewport.Height * 2f, 0f, 0f, -1f);
            effect.View = Main.GameViewMatrix.TransformationMatrix;
            effect.World = Matrix.CreateTranslation(new Vector3(-Main.screenPosition, 0f));

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawUserPrimitives(PrimitiveType.TriangleStrip, Vertices.ToArray(), 0, Vertices.Count - 2);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                effect?.Dispose();
            }

            effect = null;
        }
        
        ~ColorPrimitiveTrail() => Dispose(false);
    }
}
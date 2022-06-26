using AllBeginningsMod.Common.CustomEntities.PrimitiveTrails;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;

namespace AllBeginningsMod.Content.CustomEntities.PrimitiveTrails
{
    public sealed class ColorPrimitiveTrail : PrimitiveTrail
    {
        public Color Color { get; set; }

        public ColorPrimitiveTrail(Entity entity, int length, int width, Color color) : base(entity, length, width)
        {
            Color = color;
            Vertices = new List<VertexPositionColorTexture>(Length * 2); 
        }
       
        public override BasicEffect Effect { get; } = new BasicEffect(Device)
        {
            VertexColorEnabled = true
        };

        public override void OnUpdate()
        {
            SetupVertices();
            SetupEffectMatrices();
        }

        public override void OnDraw()
        {
            if (Vertices.Count < 3)
            {
                return;
            }

            Device.RasterizerState = RasterizerState.CullNone;

            foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Device.DrawUserPrimitives(PrimitiveType.TriangleStrip, Vertices.ToArray(), 0, Vertices.Count - 2);
            }
        }

        private void SetupVertices()
        {
            if (Vertices.Count > Length * 2)
            {
                Vertices.RemoveAt(0);
                return;
            }

            Vector2 offset = new Vector2(Width).RotatedBy((Entity.oldPosition - Entity.position).ToRotation());
            
            Vertices.Add(new VertexPositionColorTexture(new Vector3(Entity.Center + offset, 0f), Color, Vector2.One));
            Vertices.Add(new VertexPositionColorTexture(new Vector3(Entity.Center - offset, 0f), Color, Vector2.Zero));
        }

        private void SetupEffectMatrices()
        {
            float width = Device.Viewport.Width > 0f ? 2f / Device.Viewport.Width : 0f;
            float height = Device.Viewport.Height > 0f ? -2f / Device.Viewport.Height : 0f;

            Effect.Projection = new Matrix
            {
                M11 = width,
                M22 = height,
                M33 = 1f,
                M44 = 1f,
                M41 = -1f - width / 2f,
                M42 = 1f - height / 2f
            };
            Effect.View = Main.GameViewMatrix.TransformationMatrix;
            Effect.World = Matrix.CreateTranslation(new Vector3(-Main.screenPosition, 0f));
        }
    }
}
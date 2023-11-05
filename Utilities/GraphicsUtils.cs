using AllBeginningsMod.Utilities.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Utilities
{
    internal class GraphicsUtils : ILoadable
    {
        private static VertexBuffer vertexBuffer;
        private static IndexBuffer indexBuffer;
        public void Load(Mod mod) {
            Main.QueueMainThreadAction(
                () => {
                    vertexBuffer = new DynamicVertexBuffer(Main.graphics.GraphicsDevice, typeof(VertexPositionColorTexture), 4, BufferUsage.None);
                    indexBuffer = new DynamicIndexBuffer(Main.graphics.GraphicsDevice, IndexElementSize.SixteenBits, 6, BufferUsage.None);
                }
            );
        }

        public void Unload() {
            Main.QueueMainThreadAction(
                () => {
                    vertexBuffer.Dispose();
                    indexBuffer.Dispose();
                }
            );
        }

        
        public static void DrawQuad(Vector2 position, float width, float height, Color color, Effect effect) {
            GraphicsDevice device = Main.graphics.GraphicsDevice;
            if (vertexBuffer is null) {
                return;
            }

            if (indexBuffer.IndexCount == 0) {
                indexBuffer.SetData(
                    new ushort[] {
                        0, 3, 2,
                        2, 1, 0
                    }
                );
            }

            Vector3 positionVec3 = position.ToVector3();
            vertexBuffer.SetData(
                new VertexPositionColorTexture[] {
                    new VertexPositionColorTexture(positionVec3, color, new(0, 0)),
                    new VertexPositionColorTexture(positionVec3 + Vector3.UnitX * width, color, new(1, 0)),
                    new VertexPositionColorTexture(positionVec3 + new Vector3(width, height, 0f), color, new(1, 1)),
                    new VertexPositionColorTexture(positionVec3 + Vector3.UnitY * height, color, new(0, 1))
                }
            );

            device.SetVertexBuffer(vertexBuffer);
            device.Indices = indexBuffer;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertexBuffer.VertexCount, 0, indexBuffer.IndexCount / 3);
            }
        }

        
    }
}

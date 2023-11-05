using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.PrimitiveDrawing
{
    internal class PrimitiveTrail
    {
        public DynamicVertexBuffer VertexBuffer { get; private set; }
        public DynamicIndexBuffer IndexBuffer { get; private set; }
        public ITrailStyle TrailStyle { get; }
        public Func<float, float> TrailWidth { get; set; }
        public Func<Vector2, Color> TrailColor { get; set; }
        public static Effect DefaultTrailEffect => ModContent.Request<Effect>("AllBeginningsMod/Assets/Effects/DefaultTrailShader", AssetRequestMode.ImmediateLoad).Value;
        public int MaxTrailPositions { get; }

        private static GraphicsDevice Device => Main.graphics.GraphicsDevice;

        public PrimitiveTrail(int maxTrailPositionsCount, Func<float, float> trailWidth, Func<Vector2, Color> trailColor = null, ITrailStyle trailStyle = null) {
            TrailStyle = trailStyle ?? new DefaultTrailStyle();
            TrailWidth = trailWidth;
            TrailColor = trailColor ?? (_ => Color.White);
            MaxTrailPositions = maxTrailPositionsCount;

            Main.QueueMainThreadAction(
                () => {
                    VertexBuffer = new DynamicVertexBuffer(Device, typeof(VertexPositionColorTexture), TrailStyle.VertexCount(MaxTrailPositions), BufferUsage.None);
                    IndexBuffer = new DynamicIndexBuffer(Device, IndexElementSize.SixteenBits, TrailStyle.IndexCount(MaxTrailPositions), BufferUsage.None);
                }
            );
        }

        public void Update(Vector2[] trailPositions) {
            if (VertexBuffer is null || IndexBuffer is null) {
                return;
            }

            if (trailPositions.Length > MaxTrailPositions) {
                trailPositions = trailPositions[..MaxTrailPositions];
            }
            else if (trailPositions.Length < 2) {
                return;
            }

            VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[TrailStyle.VertexCount(trailPositions.Length)];
            ushort[] indices = new ushort[TrailStyle.IndexCount(trailPositions.Length)];
            TrailStyle.SetBuffers(trailPositions, TrailWidth, factor => TrailColor(factor), ref vertices, ref indices);

            VertexBuffer.SetData(vertices);
            IndexBuffer.SetData(indices);
        }

        public void Draw(Effect effect = null) {
            if (VertexBuffer is null || IndexBuffer is null) {
                return;
            }

            Device.SetVertexBuffer(VertexBuffer);
            Device.Indices = IndexBuffer;

            if (effect is null) {
                if (DefaultTrailEffect is null) {
                    return;
                }

                effect = DefaultTrailEffect;
            }
            
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, VertexBuffer.VertexCount, 0, IndexBuffer.IndexCount / 3);
            }
        }
    }
}

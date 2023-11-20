using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
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
        public Func<float, Color> TrailColor { get; set; }
        private static Effect defaultTrailEffect;
        public int MaxTrailPositions { get; }
        public Vector2[] Positions { get; set; }

        private static GraphicsDevice Device => Main.graphics.GraphicsDevice;

        public PrimitiveTrail(int maxTrailPositionsCount, Func<float, float> trailWidth, Func<float, Color> trailColor = null, ITrailStyle trailStyle = null) {
            TrailStyle = trailStyle ?? new DefaultTrailStyle();
            TrailWidth = trailWidth;
            TrailColor = trailColor ?? (_ => Color.White);
            MaxTrailPositions = maxTrailPositionsCount;
            Positions = new Vector2[maxTrailPositionsCount];

            Main.QueueMainThreadAction(
                () => {
                    VertexBuffer = new DynamicVertexBuffer(Device, typeof(VertexPositionColorTexture), TrailStyle.VertexCount(MaxTrailPositions), BufferUsage.None);
                    IndexBuffer = new DynamicIndexBuffer(Device, IndexElementSize.SixteenBits, TrailStyle.IndexCount(MaxTrailPositions), BufferUsage.None);
                }
            );
        }

        private void UpdateBuffers() {
            if (Positions.Length > MaxTrailPositions) {
                Positions = Positions[(Positions.Length - MaxTrailPositions)..];
            }

            VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[TrailStyle.VertexCount(Positions.Length)];
            ushort[] indices = new ushort[TrailStyle.IndexCount(Positions.Length)];
            TrailStyle.SetBuffers(Positions.ToImmutableArray(), TrailWidth, TrailColor, ref vertices, ref indices);

            VertexBuffer.SetData(vertices);
            IndexBuffer.SetData(indices);
        }

        public void Draw(Effect effect) {
            if (VertexBuffer is null || IndexBuffer is null) {
                return;
            }

            UpdateBuffers();
            Device.SetVertexBuffer(VertexBuffer);
            Device.Indices = IndexBuffer;
            
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, VertexBuffer.VertexCount, 0, IndexBuffer.IndexCount / 3);
            }
        }

        public void Draw(Texture2D texture, Color color, Matrix transformationMatrix = default, bool blackAsAlpha = false) {
            defaultTrailEffect ??= ModContent.Request<Effect>("AllBeginningsMod/Assets/Effects/DefaultTrailShader", AssetRequestMode.ImmediateLoad).Value;
            defaultTrailEffect.Parameters["sampleTexture"].SetValue(texture);
            defaultTrailEffect.Parameters["color"].SetValue(color.ToVector4());
            defaultTrailEffect.Parameters["transformationMatrix"].SetValue(transformationMatrix);
            defaultTrailEffect.Parameters["blackAsAlpha"].SetValue(blackAsAlpha);

            Draw(defaultTrailEffect);
        }
    }
}

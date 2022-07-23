using AllBeginningsMod.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Rendering.Primitives
{
    [Autoload(Side = ModSide.Client)]
    public sealed class PrimitiveRenderingSystem : ModSystem
    {
        public static DynamicIndexBuffer IndexBuffer { get; private set; }
        public static DynamicVertexBuffer VertexBuffer { get; private set; }

        public static RenderTarget2D PrimitiveTarget { get; private set; }

        private static GraphicsDevice Device => Main.graphics.GraphicsDevice;

        private static List<PrimitiveDrawData> queuedDrawData;

        public override void OnModLoad() {
            Main.QueueMainThreadAction(() => {
                PrimitiveTarget = new RenderTarget2D(Device, Main.screenWidth / 2, Main.screenHeight / 2);
            });

            queuedDrawData = new List<PrimitiveDrawData>();

            Main.OnPreDraw += CachePrimitiveDraw;
            Main.OnResolutionChanged += ResizeTarget;

            On.Terraria.Main.DrawDust += DrawTarget;
        }

        public override void OnModUnload() {
            Main.QueueMainThreadAction(() => {
                PrimitiveTarget?.Dispose();
                PrimitiveTarget = null;
            });

            IndexBuffer?.Dispose();
            IndexBuffer = null;

            VertexBuffer?.Dispose();
            VertexBuffer = null;

            queuedDrawData?.Clear();
            queuedDrawData = null;

            Main.OnPreDraw -= CachePrimitiveDraw;
            Main.OnResolutionChanged -= ResizeTarget;

            On.Terraria.Main.DrawDust -= DrawTarget;
        }

        public static void QueuePrimitive(VertexPositionColorTexture[] vertices, ushort[] indices, Effect effect, PrimitiveType type) {
            if (IndexBuffer?.IndexCount != indices.Length) {
                IndexBuffer = new DynamicIndexBuffer(Device, IndexElementSize.SixteenBits, indices.Length, BufferUsage.WriteOnly);
            }

            if (VertexBuffer?.VertexCount != vertices.Length) {
                VertexBuffer = new DynamicVertexBuffer(Device, VertexPositionColorTexture.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            }

            queuedDrawData?.Add(new PrimitiveDrawData(vertices, indices, effect, type));
        }

        private static void ClearQueuedPrimitives() {
            queuedDrawData?.Clear();
        }

        private static void DrawQueuedPrimitives() {
            queuedDrawData?.ForEach(x => {
                IndexBuffer?.SetData(x.Indices, 0, x.Indices.Length, SetDataOptions.Discard);
                VertexBuffer?.SetData(x.Vertices, SetDataOptions.Discard);

                int primitiveCount = x.Type switch {
                    PrimitiveType.TriangleList => VertexBuffer.VertexCount / 3,
                    PrimitiveType.TriangleStrip => VertexBuffer.VertexCount - 2,
                    PrimitiveType.LineList => VertexBuffer.VertexCount / 2,
                    PrimitiveType.LineStrip => VertexBuffer.VertexCount - 1,
                    PrimitiveType.PointListEXT => VertexBuffer.VertexCount / 3,
                    _ => 0
                };

                foreach (EffectPass pass in x.Effect.CurrentTechnique.Passes) {
                    pass.Apply();
                    Device.DrawIndexedPrimitives(x.Type, 0, 0, VertexBuffer.VertexCount, 0, primitiveCount);
                }
            });
        }

        private static void CachePrimitiveDraw(GameTime gameTime) {
            SpriteBatch spriteBatch = Main.spriteBatch;
            RenderTargetBinding[] oldTargets = Device.GetRenderTargets();

            Device.SetRenderTarget(PrimitiveTarget);
            Device.Clear(Color.Transparent);

            Device.SetVertexBuffer(VertexBuffer);
            Device.Indices = IndexBuffer;

            Device.RasterizerState = RasterizerState.CullNone;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, default, default, default, Main.GameViewMatrix.TransformationMatrix);

            DrawQueuedPrimitives();
            ClearQueuedPrimitives();

            spriteBatch.End();

            Device.SetRenderTargets(oldTargets);
        }

        private static void DrawTarget(On.Terraria.Main.orig_DrawDust orig, Main self) {
            orig(self);

            SpriteBatch spriteBatch = Main.spriteBatch;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, default, default, default, Main.GameViewMatrix.TransformationMatrix);
            spriteBatch.Draw(PrimitiveTarget, DrawUtils.ScreenRectangle, Color.White);
            spriteBatch.End();
        }

        private static void ResizeTarget(Vector2 resolution) {
            Main.QueueMainThreadAction(() => {
                PrimitiveTarget = new RenderTarget2D(Main.graphics.GraphicsDevice, (int)(resolution.X / 2f), (int)(resolution.Y / 2f));
            });
        }
    }
}
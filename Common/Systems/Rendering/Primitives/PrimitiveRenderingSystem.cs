using AllBeginningsMod.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Rendering.Primitives
{
    [Autoload(Side = ModSide.Client)]
    public sealed class PrimitiveRenderingSystem : ModSystem
    {
        public const int MaxPrimitives = 8000;
        public const int MaxVertices = MaxPrimitives * 3;

        public static VertexBuffer VertexBuffer { get; private set; }
        public static RenderTarget2D PrimitiveTarget { get; private set; }

        private static GraphicsDevice Device => Main.graphics.GraphicsDevice;

        private static PrimitiveDrawData[] queuedDrawData = Array.Empty<PrimitiveDrawData>();
        private static int queuedDrawDataCount;

        public override void OnModLoad() {
            Main.QueueMainThreadAction(() => {
                VertexBuffer = new VertexBuffer(Device, VertexPositionColorTexture.VertexDeclaration, MaxVertices, BufferUsage.WriteOnly);
                PrimitiveTarget = new RenderTarget2D(Device, Main.screenWidth / 2, Main.screenHeight / 2);
            });

            queuedDrawData = new PrimitiveDrawData[MaxPrimitives];

            Main.OnPreDraw += CachePrimitiveDraw;
            Main.OnResolutionChanged += ResizeTarget;

            On.Terraria.Main.DrawDust += DrawTarget;
        }

        public override void OnModUnload() {
            Main.QueueMainThreadAction(() => {
                VertexBuffer?.Dispose();
                VertexBuffer = null;

                PrimitiveTarget?.Dispose();
                PrimitiveTarget = null;
            });

            queuedDrawData = null;

            Main.OnPreDraw -= CachePrimitiveDraw;
            Main.OnResolutionChanged -= ResizeTarget;

            On.Terraria.Main.DrawDust -= DrawTarget;
        }

        public static void QueuePrimitive(VertexPositionColorTexture[] vertices, Effect effect, PrimitiveType type) {
            queuedDrawData[queuedDrawDataCount] = new PrimitiveDrawData(vertices, effect, type);
            queuedDrawDataCount++;
        }

        private static void ClearQueuedPrimitives() {
            for (int i = 0; i < MaxPrimitives; i++) {
                queuedDrawData[i] = default;
            }

            queuedDrawDataCount = 0;
        }

        private static void DrawQueuedPrimitives() {
            for (int i = 0; i < queuedDrawDataCount; i++) {
                PrimitiveDrawData primitive = queuedDrawData[i];

                VertexBuffer.SetData(primitive.Vertices);

                int primitiveCount = primitive.Type switch {
                    PrimitiveType.TriangleList => VertexBuffer.VertexCount / 3,
                    PrimitiveType.TriangleStrip => VertexBuffer.VertexCount - 2,
                    PrimitiveType.LineList => VertexBuffer.VertexCount / 2,
                    PrimitiveType.LineStrip => VertexBuffer.VertexCount - 1,
                    PrimitiveType.PointListEXT => VertexBuffer.VertexCount / 3,
                    _ => 0
                };

                foreach (EffectPass pass in primitive.Effect.CurrentTechnique.Passes) {
                    pass.Apply();
                    Device.DrawPrimitives(primitive.Type, 0, primitiveCount);
                }
            }
        }

        private static void CachePrimitiveDraw(GameTime gameTime) {
            SpriteBatch spriteBatch = Main.spriteBatch;
            RenderTargetBinding[] oldTargets = Device.GetRenderTargets();

            Device.SetRenderTarget(PrimitiveTarget);
            Device.SetVertexBuffer(VertexBuffer);
            Device.Clear(Color.Transparent);

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
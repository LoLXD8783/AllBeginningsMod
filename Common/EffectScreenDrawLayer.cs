using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;
using Terraria;

namespace AllBeginningsMod.Common
{
    internal class EffectScreenDrawLayer
    {
        private static readonly PropertyInfo RenderTargetUsageProperty;
        static EffectScreenDrawLayer() {
            RenderTargetUsageProperty = typeof(RenderTarget2D).GetProperty("RenderTargetUsage", Helper.FlagsPublicInstance);
        }

        private static SpriteBatch SpriteBatch => Main.spriteBatch;
        private static GraphicsDevice Device => SpriteBatch.GraphicsDevice;

        private RenderTarget2D target;
        private RenderTarget2D oldScreenTarget;
        public EffectScreenDrawLayer() {
            target = new RenderTarget2D(Device, Main.screenWidth, Main.screenHeight);
            oldScreenTarget = new RenderTarget2D(Device, Main.screenWidth, Main.screenHeight);
        }

        public void Draw(Effect effect, Action<SpriteBatch> drawAction) {
            if (target.Width != Main.screenWidth || target.Height != Main.screenHeight) {
                target = new RenderTarget2D(
                    Device,
                    Main.screenWidth,
                    Main.screenHeight,
                    false,
                    SurfaceFormat.Color,
                    DepthFormat.None,
                    0,
                    RenderTargetUsage.PreserveContents
                );
                oldScreenTarget = new RenderTarget2D(
                    Device,
                    Main.screenWidth,
                    Main.screenHeight,
                    false,
                    SurfaceFormat.Color,
                    DepthFormat.None,
                    0,
                    RenderTargetUsage.PreserveContents
                );
            }

            SpriteBatchData data = SpriteBatch.Capture();
            SpriteBatch.End();

            RenderTargetBinding[] bindings = Device.GetRenderTargets();

            Device.SetRenderTarget(target);
            Device.Clear(Color.Transparent);

            SpriteBatch.Begin(data);
            drawAction(Main.spriteBatch);
            SpriteBatch.End();

            Device.SetRenderTarget(oldScreenTarget);
            Device.Clear(Color.Transparent);

            SpriteBatch.Begin(SpriteBatchData.Default());
            SpriteBatch.Draw((Texture2D)bindings[0].RenderTarget, Vector2.Zero, Color.White);
            SpriteBatch.EndBegin(SpriteBatchData.Default() with { Effect = effect });
            SpriteBatch.Draw(target, Vector2.Zero, Color.White);
            SpriteBatch.End();

            Device.SetRenderTargets(bindings);

            SpriteBatch.Begin(SpriteBatchData.Default());
            SpriteBatch.Draw(oldScreenTarget, Vector2.Zero, Color.White);
            SpriteBatch.EndBegin(data);
        }
    }
}

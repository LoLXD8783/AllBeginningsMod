using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Rendering
{
    [Autoload(Side = ModSide.Client)]
    public sealed class PrimitiveRenderingSystem : ModSystem
    {
        public static RenderTarget2D PrimitiveTarget { get; private set; }

        public override void OnModLoad() {
            Main.QueueMainThreadAction(() => {
                PrimitiveTarget = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth / 2, Main.screenHeight / 2);
            });

            Main.OnResolutionChanged += ResizeTarget;
        }

        public override void OnModUnload() {
            Main.OnResolutionChanged -= ResizeTarget;
        }

        private static void ResizeTarget(Vector2 resolution) {
            Main.QueueMainThreadAction(() => {
                PrimitiveTarget = new RenderTarget2D(Main.graphics.GraphicsDevice, (int)resolution.X / 2, (int)resolution.Y / 2);
            });
        }
    }
}
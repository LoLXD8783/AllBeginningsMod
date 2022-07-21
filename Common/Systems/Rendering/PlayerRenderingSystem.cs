using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Rendering
{
    [Autoload(Side = ModSide.Client)]
    public sealed class PlayerRenderingSystem : ModSystem
    {
        public static RenderTarget2D PlayerTarget { get; private set; }

        public override void OnModLoad() {
            Main.QueueMainThreadAction(() => {
                PlayerTarget = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
            });

            Main.OnResolutionChanged += ResizeTarget;
            On.Terraria.Main.CheckMonoliths += CachePlayerDraw;
        }

        public override void OnModUnload() {
            Main.OnResolutionChanged -= ResizeTarget;
            On.Terraria.Main.CheckMonoliths -= CachePlayerDraw;
        }

        private static void CachePlayerDraw(On.Terraria.Main.orig_CheckMonoliths orig) {
            orig();

            if (Main.gameMenu) {
                return;
            }

            SpriteBatch spriteBatch = Main.spriteBatch;
            GraphicsDevice device = spriteBatch.GraphicsDevice;
            RenderTargetBinding[] oldTargets = device.GetRenderTargets();

            device.SetRenderTarget(PlayerTarget);
            device.Clear(Color.Transparent);

            spriteBatch.Begin(default, default, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);

            Player player = Main.LocalPlayer;

            if (player.active && !player.dead) {
                Main.PlayerRenderer?.DrawPlayer(Main.Camera, player, player.position, player.fullRotation, player.fullRotationOrigin);
            }

            spriteBatch.End();

            device.SetRenderTargets(oldTargets);
        }

        private static void ResizeTarget(Vector2 resolution) {
            Main.QueueMainThreadAction(() => {
                PlayerTarget = new RenderTarget2D(Main.graphics.GraphicsDevice, (int)resolution.X, (int)resolution.Y);
            });
        }
    }
}
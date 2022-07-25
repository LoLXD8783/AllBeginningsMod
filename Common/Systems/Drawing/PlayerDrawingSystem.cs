using AllBeginningsMod.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Drawing;

[Autoload(Side = ModSide.Client)]
public sealed class PlayerDrawingSystem : ModSystem
{
    public static RenderTarget2D PlayerTarget { get; private set; }

    private static GraphicsDevice Device => Main.graphics.GraphicsDevice;

    public override void OnModLoad() {
        ThreadUtils.RunOnMainThread(() => { PlayerTarget = new RenderTarget2D(Device, Main.screenWidth, Main.screenHeight); });

        Main.OnResolutionChanged += ResizeTarget;

        On.Terraria.Main.CheckMonoliths += CachePlayerDraw;
    }

    public override void OnModUnload() {
        ThreadUtils.RunOnMainThread(() => {
            PlayerTarget.Dispose();
            PlayerTarget = null;
        });

        Main.OnResolutionChanged -= ResizeTarget;

        On.Terraria.Main.CheckMonoliths -= CachePlayerDraw;
    }

    private static void CachePlayerDraw(On.Terraria.Main.orig_CheckMonoliths orig) {
        orig();

        if (Main.gameMenu)
            return;

        SpriteBatch spriteBatch = Main.spriteBatch;
        RenderTargetBinding[] oldTargets = Device.GetRenderTargets();

        Device.SetRenderTarget(PlayerTarget);
        Device.Clear(Color.Transparent);

        spriteBatch.Begin(default, default, default, default, Main.Rasterizer, default, Main.GameViewMatrix.TransformationMatrix);

        Player player = Main.LocalPlayer;

        if (player.active && !player.dead)
            Main.PlayerRenderer?.DrawPlayer(Main.Camera, player, player.position, player.fullRotation, player.fullRotationOrigin);

        spriteBatch.End();

        Device.SetRenderTargets(oldTargets);
    }

    private static void ResizeTarget(Vector2 resolution) => ThreadUtils.RunOnMainThread(() => {
        PlayerTarget = new RenderTarget2D(Device, (int) resolution.X, (int) resolution.Y);
    });
}
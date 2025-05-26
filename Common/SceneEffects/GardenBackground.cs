using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.SceneEffects; 
internal class GardenBackground : ILoadable {
    public void Load(Mod mod) {
        On_Main.DoDraw_WallsAndBlacks += DrawBackground; ;
    }

    public void Unload() {
        On_Main.DoDraw_WallsAndBlacks -= DrawBackground;
    }

    private static Texture2D[] layers;
    public static void DrawBackground(On_Main.orig_DoDraw_WallsAndBlacks orig, Main self) {
        if(layers is null) {
            layers = new Texture2D[5];
            for(int i = 0; i < layers.Length; i++) {
                layers[i] = ModContent.Request<Texture2D>(
                    $"AllBeginningsMod/Common/SceneEffects/GardenBackground_{i}",
                    AssetRequestMode.ImmediateLoad
                ).Value;
            }
        }

        Vector2 center = AllBeginningsWorld.GardenBounds.Center() * 16f;
        Vector2 dir = center - Main.LocalPlayer.Center;
        for(int i = layers.Length - 1; i > 0; i--) {
            Main.spriteBatch.Draw(
                layers[i], center - Main.screenPosition + dir * MathF.Pow((float)i / layers.Length, 2) * 0.3f,
                null,
                Color.White,
                0f,
                layers[i].Size() / 2f,
                2f,
                SpriteEffects.None,
                0f
            );
        }

        orig(self);
    }
}

/// <summary>
/// Temporary class for testing background
/// </summary>
internal class GardenSpawn : ModPlayer {
    public static Vector2 GardenCenter;
    public override void OnEnterWorld() {
        GardenCenter = Player.Center + Vector2.UnitY * 5000;
    }
}
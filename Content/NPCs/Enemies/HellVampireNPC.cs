using System;
using AllBeginningsMod.Common.Bases.NPCs;
using AllBeginningsMod.Common.Loaders;
using AllBeginningsMod.Content.Dusts;
using AllBeginningsMod.Utilities;
using AllBeginningsMod.Utilities.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.NPCs.Enemies;

public sealed class HellVampireNPC : VampireNPC
{
    public override float SpawnChance(NPCSpawnInfo spawnInfo) {
        if (spawnInfo.SpawnTileY < Main.UnderworldLayer) {
            return 0f;
        }

        return 0.05f;
    }

    protected override void ExplosionEffects() {
        IEntitySource source = NPC.GetSource_Death();
        Vector2[] gorePositions = NPC.Center.PositionsAround(
            3,
            _ => Main.rand.NextFloat(10f, 25f),
            out Vector2[] goreDirections,
            MathHelper.TwoPi * 0.75f
        );
        for (int i = 0; i < gorePositions.Length; i++) {
            Gore gore = Gore.NewGoreDirect(
                source,
                gorePositions[i],
                goreDirections[i] * Main.rand.NextFloat(3f, 5f),
                Mod.Find<ModGore>("HellVampireGore" + i).Type
            );
             
            gore.position -= new Vector2(gore.Width, gore.Height) * 0.5f;
        }

        Vector2[] dustPositions = NPC.Center.PositionsAround(12, _ => Main.rand.NextFloat(5f, 30f), out Vector2[] dustDirections);
        for (int i = 0; i < dustPositions.Length; i++) {
            Dust.NewDustPerfect(
                dustPositions[i],
                Main.rand.NextFromList(DustID.Smoke, DustID.TreasureSparkle, DustID.YellowTorch),
                dustDirections[i] * Main.rand.NextFloat(3f, 6f)
            );

            if (i % 2 == 0) {
                Dust.NewDustPerfect(
                    dustPositions[i],
                    ModContent.DustType<SmokeDust>(),
                    dustDirections[i] * Main.rand.NextFloat(0.1f, 7f),
                    Main.rand.Next(30, 50),
                    Color.Lerp(Main.rand.NextFromList(Color.Red, Color.Orange), Color.Black, Main.rand.NextFloat(0.35f)),
                    Main.rand.NextFloat(0.7f, 1.4f)
                );
            }
        }

        Lighting.AddLight(NPC.Center, new Vector3(1.86f, 1.22f, 0.69f) * 3.5f);
        SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, NPC.Center);
    }

    [Effect("FishEye")]
    private static Effect fishEyeEffect;
    protected override void Draw(SpriteBatch spriteBatch, Color drawColor, float explodingProgress) {
        Texture2D texture = TextureAssets.Npc[Type].Value;
        explodingProgress *= explodingProgress;

        Vector2 position = NPC.Center - Main.screenPosition;
        if (explodingProgress != 0f && Main.GameUpdateCount % (int)((1f - explodingProgress) * 10f + 1f) == 0) {
            position += Main.rand.NextVector2Unit() * explodingProgress * 2f;
        }
        Vector2 scale = Vector2.One * (1f + 0.5f * explodingProgress);
        float rotation = NPC.rotation + Main.rand.NextFloatDirection() * 0.07f * explodingProgress;

        fishEyeEffect.Parameters["strength"].SetValue(explodingProgress * 2f);
        fishEyeEffect.Parameters["uImageSize0"].SetValue(texture.Size());
        fishEyeEffect.Parameters["uSourceRect"].SetValue(new Vector4(0f, 0f, texture.Width, texture.Height));
        fishEyeEffect.Parameters["center"].SetValue(Vector2.One * 0.5f);

        SpriteBatchSnapshot snapshot = spriteBatch.Capture();
        spriteBatch.End();
        spriteBatch.Begin(snapshot with { Effect = fishEyeEffect });

        spriteBatch.Draw(
            texture,
            position,
            null,
            drawColor,
            rotation,
            texture.Size() * 0.5f,
            scale,
            SpriteEffects.None,
            0
        );

        spriteBatch.End();
        spriteBatch.Begin(
            SpriteSortMode.Deferred, 
            BlendState.Additive, 
            Main.DefaultSamplerState, 
            DepthStencilState.None, 
            Main.Rasterizer, 
            fishEyeEffect, 
            Main.GameViewMatrix.TransformationMatrix
        );

        float bloomScaling = 0.2f;
        Texture2D texBloom0 = ModContent.Request<Texture2D>(Texture + "_Glow", AssetRequestMode.ImmediateLoad).Value;
        spriteBatch.Draw(
            texBloom0,
            position,
            null,
            Color.White * explodingProgress * 1.1f,
            rotation,
            texBloom0.Size() * 0.5f,
            scale,
            SpriteEffects.None,
            0
        );

        Texture2D texBloom1 = ModContent.Request<Texture2D>(Texture + "_Bloom", AssetRequestMode.ImmediateLoad).Value;
        spriteBatch.Draw(
            texBloom1,
            position,
            null,
            Color.White * (explodingProgress * 2f - 1f) * 1.1f,
            rotation,
            texBloom1.Size() * 0.5f,
            scale * bloomScaling,
            SpriteEffects.None,
            0
        );

        spriteBatch.End();
        spriteBatch.Begin(snapshot);
    }
}
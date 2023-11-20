using System;
using AllBeginningsMod.Common.Bases.NPCs;
using AllBeginningsMod.Common.Loaders;
using AllBeginningsMod.Content.CameraModifiers;
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
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.NPCs.Enemies.Hell;

public sealed class HellVampireNPC : VampireNPC
{
    protected override float ExplosionRange => 125;

    protected override void PostSetDefaults() {
        NPC.lavaImmune = true;
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) {
        return spawnInfo.Player.ZoneUnderworldHeight ? 0.15f : 0f;
    }

    protected override void ExplosionEffects() {
        IEntitySource source = NPC.GetSource_Death();
        Gore gore1 = Gore.NewGoreDirect(
            source,
            NPC.Center - Vector2.One * 20f,
            -Vector2.One * Main.rand.NextFloat(3f, 5f),
            Mod.Find<ModGore>("HellVampireGore0").Type
        );

        gore1.position -= new Vector2(gore1.Width, gore1.Height) * 0.5f;

        Gore gore2 = Gore.NewGoreDirect(
            source,
            NPC.Center + Vector2.One * 20f,
            Vector2.One * Main.rand.NextFloat(3f, 5f),
            Mod.Find<ModGore>("HellVampireGore1").Type
        );

        gore2.position -= new Vector2(gore2.Width, gore2.Height) * 0.5f;

        Vector2[] dustPositions = NPC.Center.PositionsAround(13, _ => Main.rand.NextFloat(20f, 30f), out Vector2[] dustDirections, Main.rand.NextFloat());
        for (int i = 0; i < dustPositions.Length; i++) {
            Dust.NewDustPerfect(
                dustPositions[i],
                Main.rand.NextFromList(DustID.Smoke, DustID.TreasureSparkle, DustID.YellowTorch),
                dustDirections[i] * Main.rand.NextFloat(3f, 6f)
            );

            if (i % 3 == 0) {
                Dust.NewDustPerfect(
                    dustPositions[i],
                    ModContent.DustType<HellVampireExplosionDust>(),
                    dustDirections[i] * Main.rand.NextFloat(0.6f, 7f),
                    Main.rand.Next(30, 50),
                    Color.White,
                    Main.rand.NextFloat(0.4f, 0.9f)
                );
            }
        }

        Lighting.AddLight(NPC.Center, new Vector3(1.86f, 1.22f, 0.69f) * 3.5f);
        SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, NPC.Center);

        Main.instance.CameraModifiers.Add(
            new ExplosionShakeCameraModifier(8f, 0.87f, NPC.Center, 5000, FullName)
        );
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
        Vector2 scale = Vector2.One * (1f + 0.3f * explodingProgress);
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
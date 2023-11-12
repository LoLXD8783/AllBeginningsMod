using AllBeginningsMod.Common.Bases.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent;
using Terraria;
using AllBeginningsMod.Utilities.Extensions;
using Mono.Cecil;
using Terraria.Audio;
using Terraria.ID;
using Terraria.DataStructures;
using AllBeginningsMod.Content.Dusts;
using Terraria.ModLoader;
using AllBeginningsMod.Utilities;
using AllBeginningsMod.Common.Loaders;

namespace AllBeginningsMod.Content.NPCs.Enemies;

internal class VampireDevilNPC : VampireNPC
{
    public override void SetStaticDefaults() {
        Main.npcFrameCount[Type] = 2;
    }

    protected override float ExplosionRange => 128;
    protected override int MaxExplodingTime => 180;
    protected override void PostSetDefaults() {
        NPC.width = NPC.height = 75;
    }

    protected override void FollowBehaviour(Player target) {
        if (NPC.velocity.LengthSquared() > 1.2f) {
            return;
        }

        NPC.velocity += 0.05f * NPC.Center.DirectionTo(target.Center);
        NPC.direction = MathF.Sign(target.Center.X - NPC.Center.X);
    }

    public override void FindFrame(int frameHeight) {
        if (IsExploding) {
            NPC.frame = new(0, 0, 84, frameHeight);
            return;
        }

        int frameTime = 20;
        if (NPC.frameCounter++ > frameTime * 2) {
            NPC.frameCounter = 0d;
        }

        NPC.frame = new(0, ((int)NPC.frameCounter / frameTime % 2) * frameHeight, 84, frameHeight);
    }

    protected override void ExplosionEffects() {
        IEntitySource source = NPC.GetSource_Death();
        Vector2[] dustPositions = NPC.Center.PositionsAround(13, _ => Main.rand.NextFloat(5f, 30f), out Vector2[] dustDirections);
        for (int i = 0; i < dustPositions.Length; i++) {
            Dust.NewDustPerfect(
                dustPositions[i],
                Main.rand.NextFromList(DustID.Smoke, DustID.TreasureSparkle, DustID.YellowTorch),
                dustDirections[i] * Main.rand.NextFloat(3f, 7f)
            );

            Dust.NewDustPerfect(
                dustPositions[i],
                ModContent.DustType<SmokeDust>(),
                dustDirections[i] * Main.rand.NextFloat(0.1f, 7f),
                Main.rand.Next(30, 50),
                Color.Lerp(Main.rand.NextFromList(Color.OrangeRed, Color.SlateGray), Color.Black, Main.rand.NextFloat(0.45f)),
                Main.rand.NextFloat(0.7f, 1.4f)
            );
        }

        Lighting.AddLight(NPC.Center, new Vector3(1.86f, 1.22f, 0.69f) * 5f);
        SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, NPC.Center);
    }

    [Effect("FishEye")]
    private static Effect fishEyeEffect;
    protected override void Draw(SpriteBatch spriteBatch, Color drawColor, float explodingProgress) {
        Texture2D texture = TextureAssets.Npc[Type].Value;

        explodingProgress *= explodingProgress;

        Vector2 position = NPC.Center - Main.screenPosition;
        if (explodingProgress != 0f && Main.GameUpdateCount % (int)((1f - explodingProgress) * 10f + 1f) == 0) {
            position += Main.rand.NextVector2Unit() * explodingProgress * 2.5f;
        }

        Vector2 scale = Vector2.One * (1f + 0.5f * explodingProgress);
        float rotation = NPC.rotation + Main.rand.NextFloatDirection() * 0.05f * explodingProgress;

        for (int i = 0; i < 4; i++) {
            spriteBatch.Draw(
                texture,
                position + Main.rand.NextVector2Unit() * 4f,
                NPC.frame,
                Color.Black * 0.1f,
                rotation,
                NPC.frame.Size() * 0.5f,
                scale,
                NPC.direction > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0
            );
        }

        fishEyeEffect.Parameters["strength"].SetValue(explodingProgress * 2f);
        fishEyeEffect.Parameters["uImageSize0"].SetValue(texture.Size());
        fishEyeEffect.Parameters["uSourceRect"].SetValue(new Vector4(NPC.frame.X, NPC.frame.Y, NPC.frame.Width, NPC.frame.Height));
        fishEyeEffect.Parameters["center"].SetValue(Vector2.One * 0.5f);

        SpriteBatchSnapshot snapshot = spriteBatch.Capture();
        spriteBatch.End();
        spriteBatch.Begin(snapshot with { Effect = fishEyeEffect });

        spriteBatch.Draw(
            texture,
            position,
            NPC.frame,
            drawColor,
            rotation,
            NPC.frame.Size() * 0.5f,
            scale,
            NPC.direction > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
            0
        );

        spriteBatch.End();
        spriteBatch.Begin(snapshot);
    }
}

using System;
using AllBeginningsMod.Common.Bases.Tiles;
using AllBeginningsMod.Common.Graphics.Particles;
using AllBeginningsMod.Content.Items.Materials;
using AllBeginningsMod.Content.Particles;
using AllBeginningsMod.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AllBeginningsMod.Content.Tiles.Plants;

public sealed class DeathBlossomTile : ModTileBase
{
    public override void SetStaticDefaults() {
        Main.tileCut[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileLighted[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;

        TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Plant"]);

        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);

        TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
        TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.EmptyTile, 1, 0);
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 0);

        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.newTile.UsesCustomCanPlace = true;

        TileObjectData.newTile.DrawYOffset = 2;

        TileObjectData.newTile.AnchorValidTiles = new int[] { TileID.Grass };

        TileObjectData.newTile.AnchorAlternateTiles = new int[] { TileID.ClayPot, TileID.PlanterBox };

        TileObjectData.addTile(Type);

        AddMapEntry(new Color(92, 71, 232));

        DustType = DustID.Enchanted_Pink;
        HitSound = SoundID.Grass;
        MineResist = 1.5f;
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
        r = 0.1f;
        b = 0.4f;
    }

    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) {
        Tile tile = Framing.GetTileSafely(i, j);
        TileObjectData data = TileObjectData.GetTileData(tile);

        if (tile.TileFrameY != 0)
            return false;

        SpriteEffects effects = i % 2 == 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        Texture2D texture = TextureAssets.Tile[Type].Value;

        Vector2 origin = new(texture.Width / 2f, texture.Height);
        Vector2 offset = new(data.DrawXOffset, data.DrawYOffset);
        Vector2 offScreen = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
        Vector2 position = new Vector2(i, j) * 16f - Main.screenPosition + offScreen + origin + offset;

        Color color = Lighting.GetColor(i, j);

        float rotation = MathF.Cos(i + Main.GameUpdateCount * 0.05f) * 0.2f;

        spriteBatch.Draw(texture, position, null, color, rotation, origin, 1f, effects, 0);

        return false;
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch) {
        Tile tile = Framing.GetTileSafely(i, j);
        TileObjectData data = TileObjectData.GetTileData(tile);

        if (tile.TileFrameY != 0)
            return;

        if (Main.rand.NextBool(100))
            Particle.Spawn<DeathBlossomParticle>(new Vector2(i, j) * 16f);

        SpriteEffects effects = i % 2 == 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        Texture2D texture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;

        Vector2 origin = new(texture.Width / 2f, texture.Height);
        Vector2 offset = new(data.DrawXOffset, data.DrawYOffset);
        Vector2 offScreen = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
        Vector2 position = new Vector2(i, j) * 16f - Main.screenPosition + offScreen + origin + offset;

        float rotation = MathF.Cos(i + Main.GameUpdateCount * 0.05f) * 0.2f;

        spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1f, effects, 0);
    }

    public override void NumDust(int i, int j, bool fail, ref int num) {
        num = 2;
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY) {
        Item.NewItem(new EntitySource_TileBreak(i, j), new Rectangle(i * 16, j * 16, 16, 32), ModContent.ItemType<DeathBlossomItem>());
    }
}
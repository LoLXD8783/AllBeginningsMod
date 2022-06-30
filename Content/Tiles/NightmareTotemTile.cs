using AllBeginningsMod.Content.Items.Placeables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AllBeginningsMod.Content.Tiles
{
    public sealed class NightmareTotemTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileNoFail[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);

            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new int[]
            {
                16,
                16,
                16
            };

            TileObjectData.addTile(Type);

            DustType = DustID.Shadowflame;
            HitSound = SoundID.Dig;
            MineResist = 2f;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Texture2D glowmaskTexture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;

            Tile tile = Framing.GetTileSafely(i, j);

            Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
            Vector2 drawPosition = new Vector2(i, j) * 16f - Main.screenPosition + zero + new Vector2(0f, TileObjectData.GetTileData(tile).DrawYOffset);

            Rectangle frame = new(tile.TileFrameX, tile.TileFrameY, 18, 18);

            spriteBatch.Draw(glowmaskTexture, drawPosition, frame, Color.White, 0f, default, 1f, SpriteEffects.None, 0f);
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

        public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 32, ModContent.ItemType<NightmareTotemItem>());
    }
}
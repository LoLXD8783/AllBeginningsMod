using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AllBeginningsMod.Content.Items.Placeable
{
    internal class StoneStatueTile : ModTile
    {
        public override void SetStaticDefaults() {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileNoAttach[Type] = true;

            DustType = DustID.Stone;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.DrawXOffset = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(110, 108, 105));
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j) {
            yield return new Item(ModContent.ItemType<StoneStatueItem>());
        }
    }
}

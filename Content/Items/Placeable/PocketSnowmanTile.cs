using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AllBeginningsMod.Content.Items.Placeable
{
    internal class PocketSnowmanTile : ModTile
    {
        public override void SetStaticDefaults() {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileNoAttach[Type] = true;

            DustType = DustID.Snow;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.DrawXOffset = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(220, 220, 240));
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j) {
            yield return new Item(ModContent.ItemType<PocketSnowmanItem>());
        }
    }
}

using AllBeginningsMod.Common.Bases.Tiles;
using AllBeginningsMod.Content.Items.Placeables.Relics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Tiles.Relics;

public sealed class NightgauntRelicTile : RelicTileBase
{
    public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(new EntitySource_TileBreak(i, j),
        i * 16,
        j * 16,
        32,
        32,
        ModContent.ItemType<NightgauntRelicItem>());
}
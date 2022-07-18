using AllBeginningsMod.Content.Items.Accessories;
using AllBeginningsMod.Content.Items.Consumables;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems
{
    public sealed class ABChestLootSystem : ModSystem
    {
        public override void PostWorldGen() {
            int[] woodChestLoot = new int[] {
                ModContent.ItemType<MidasPouchItem>(),
                ModContent.ItemType<PegasusBootsItem>()
            };
            int woodChestLootIndex = 0;

            for (int i = 0; i < Main.maxChests; i++) {
                Chest chest = Main.chest[i];

                if (chest == null) {
                    continue;
                }

                Tile tile = Framing.GetTileSafely(chest.x, chest.y);

                if (!tile.HasTile || tile.TileType != TileID.Containers) {
                    continue;
                }

                if (tile.TileFrameX == 0 || tile.TileFrameX == 12 * 36) {
                    for (int j = 0; j < Chest.maxItems; j++) {
                        Item item = chest.item[j];

                        if (item.type == ItemID.None) {
                            item.SetDefaults(woodChestLoot[woodChestLootIndex]);
                            item.stack = woodChestLootIndex == 0 ? WorldGen.genRand.Next(1, 3) : 1;
                            woodChestLootIndex = (woodChestLootIndex + 1) % woodChestLoot.Length;
                            break;
                        }
                    }
                }
            }
        } 
    }
}
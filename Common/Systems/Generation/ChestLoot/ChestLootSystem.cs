using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Generation.ChestLoot
{
    public abstract class ChestLootSystem : ModSystem
    {
        protected abstract int ChestFrameX { get; }

        protected List<ItemChestLootEntry> LootEntries { get; set; }

        public sealed override void PostWorldGen() {
            LootEntries = new List<ItemChestLootEntry>();

            AddLootEntries();

            for (int i = 0; i < Main.maxChests; i++) {
                Chest chest = Main.chest[i];

                if (chest == null) {
                    continue;
                }

                Tile tile = Framing.GetTileSafely(chest.x, chest.y);

                if (!tile.HasTile || tile.TileType != TileID.Containers || tile.TileFrameX != ChestFrameX) {
                    continue;
                }

                LootEntries.ForEach(x => AddChestLoot(chest, x));
            }

            LootEntries.Clear();
        }

        protected virtual void AddLootEntries() { }

        private static void AddChestLoot(Chest chest, ItemChestLootEntry loot) {
            if (chest.item.Any(x => x.type == loot.Type) || (loot.MaxAmountPerWorld >= loot.CurrentWorldAmount && !WorldGen.genRand.NextBool(loot.ExtraSpawnChance))) {
                return;
            }

            for (int i = 0; i < Chest.maxItems; i++) {
                Item item = chest.item[i];

                if (item.IsAir) {
                    loot.CurrentWorldAmount++;

                    item.SetDefaults(loot.Type);
                    item.stack = WorldGen.genRand.Next(loot.MinStack, loot.MaxStack);
                    break;
                }
            }
        }
    }
}
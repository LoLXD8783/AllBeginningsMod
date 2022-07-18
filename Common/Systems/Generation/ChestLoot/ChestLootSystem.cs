using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Generation.ChestLoot
{
    public abstract class ChestLootSystem : ModSystem
    {
        protected List<ItemChestLootEntry> LootEntries { get; set; }

        protected abstract int ChestFrameX { get; }

        public sealed override void PostWorldGen() {
            LootEntries = new List<ItemChestLootEntry>();

            AddLootEntries();

            for (var i = 0; i < Main.maxChests; i++) {
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
            if (chest == null || !WorldGen.genRand.NextBool(loot.SpawnChance)) {
                return;
            }

            for (var i = 0; i < Chest.maxItems; i++) {
                Item item = chest.item[i];

                if (item.IsAir) {
                    item.SetDefaults(loot.Type);
                    item.stack = WorldGen.genRand.Next(loot.MinStack, loot.MaxStack);
                    break;
                }
            }
        }
    }
}
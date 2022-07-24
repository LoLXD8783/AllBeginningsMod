using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.WorldGeneration.ChestLoot
{
    public abstract class ChestLootSystem : ModSystem
    {
        public static List<ItemChestLootEntry> LootEntries { get; private set; }

        public abstract int ChestFrameX { get; }

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

                LootEntries.ForEach(lootEntry => AddLootToChest(chest, lootEntry));
            }

            LootEntries?.Clear();
            LootEntries = null;
        }

        public virtual void AddLootEntries() { }

        private static void AddLootToChest(Chest chest, ItemChestLootEntry loot) {
            if (!WorldGen.genRand.NextBool(loot.SpawnChance)) {
                return;
            }

            for (int i = 0; i < Chest.maxItems; i++) {
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
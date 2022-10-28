using System;
using System.Collections.Generic;
using AllBeginningsMod.Content.Items.Accessories;
using AllBeginningsMod.Content.Items.Consumables;
using AllBeginningsMod.Content.Items.Weapons.Summon;
using AllBeginningsMod.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.WorldGeneration;

public sealed class ChestLootSystem : ModSystem
{
    private static readonly List<ChestLootEntry> registeredLoot = new();
    private static readonly Dictionary<int, int> itemAmountByType = new();

    public override void SetStaticDefaults() {
        registeredLoot.Add(new ChestLootEntry(ModContent.ItemType<PlumeWhipItem>(), 1, 1, 1, 12 * 36));
        registeredLoot.Add(new ChestLootEntry(ModContent.ItemType<PegasusBootsItem>(), 1, 1, 1, 0, 12 * 36));
        registeredLoot.Add(new ChestLootEntry(ModContent.ItemType<MidasPouchItem>(), 4, 1, 10, 0, 12 * 36));
    }

    public override void PostWorldGen() {
        for (int i = 0; i < registeredLoot.Count; i++) {
            for (int j = 0; j < Main.maxChests; j++) {
                ApplyLootEntry(Main.chest[j], registeredLoot[i]);
            }
        }
    }

    private static void ApplyLootEntry(Chest chest, ChestLootEntry entry) {
        if (chest == null) {
            return;
        }

        Tile tile = Framing.GetTileSafely(chest.x, chest.y);

        if (!MatchesChestFrame(tile, entry) || !MatchesTileConditions(tile)) {
            return;
        }

        bool alreadyExists = itemAmountByType.TryGetValue(entry.Type, out int amount) && amount > 0;
        bool shouldAdd = WorldGen.genRand.NextBool(entry.Chance);

        if (alreadyExists && !shouldAdd) {
            return;
        }

        int stack = entry.MinStack == 1 && entry.MaxStack == 1 ? 1 : WorldGen.genRand.Next(entry.MinStack, entry.MaxStack);

        if (chest.TryAddItem(entry.Type, stack)) {
            if (alreadyExists) {
                itemAmountByType[entry.Type] += stack;
            }
            else {
                itemAmountByType[entry.Type] = stack;
            }
        }
        
        Console.WriteLine($"{chest.x}X{chest.y} @ {entry.Type}/{entry.MinStack}/{entry.MaxStack}");
    }

    private static bool MatchesChestFrame(Tile tile, ChestLootEntry entry) {
        for (int i = 0; i < entry.ChestFrames.Length; i++) {
            if (tile.TileFrameX == entry.ChestFrames[i]) {
                return true;
            }
        }

        return false;
    }

    private static bool MatchesTileConditions(Tile tile) {
        return tile.HasTile && tile.TileType == TileID.Containers;
    }
}
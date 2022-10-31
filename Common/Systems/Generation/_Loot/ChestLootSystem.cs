using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Generation;

public sealed class ChestLootSystem : ModSystem
{
    public static readonly Dictionary<int, int> ItemWorldAmountByType = new();

    public override void PostWorldGen() {
        for (int i = 0; i < Main.maxChests; i++) {
            Chest chest = Main.chest[i];

            if (chest == null) {
                continue;
            }

            Tile tile = Framing.GetTileSafely(chest.x, chest.y);

            if (!tile.HasTile || tile.TileType != TileID.Containers) {
                continue;
            }

            SetAdequateLoot(tile, chest);
        }
    }

    public static void RegisterLootItem(int type, int stack) {
        if (ExistsInWorld(type)) {
            ItemWorldAmountByType[type] += stack;
        }
        else {
            ItemWorldAmountByType[type] = stack;
        }
    }

    public static bool ExistsInWorld(int type) {
        return ItemWorldAmountByType.TryGetValue(type, out int amount);
    }

    private static void SetAdequateLoot(Tile tile, Chest chest) {
        switch (tile.TileFrameX) {
            case 0:
            case 12 * 36:
                new WoodChestLoot().SetLoot(chest);

                break;
        }
    }
}
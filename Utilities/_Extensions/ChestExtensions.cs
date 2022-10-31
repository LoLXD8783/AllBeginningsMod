using AllBeginningsMod.Common.Systems.Generation;
using Terraria;
using Terraria.ID;

namespace AllBeginningsMod.Utilities;

public static class ChestExtensions
{
    public static bool TryAddItem(this Chest chest, int type, int stack = 1) {
        if (!chest.TryGetEmptySlot(out int index) || type == ItemID.None) {
            return false;
        }

        chest.item[index].SetDefaults(type);
        chest.item[index].stack = stack;

        return true;
    }

    public static bool TryAddShopItem(this Chest chest, int type, ref int nextSlot) {
        if (!chest.TryGetEmptySlot(out _) || type == ItemID.None || nextSlot == -1) {
            return false;
        }

        chest.item[nextSlot].SetDefaults(type);
        nextSlot++;

        return true;
    }

    public static bool TryAddLootItem(this Chest chest, int type, int stack = 1, int chance = 1) {
        if (ChestLootSystem.ExistsInWorld(type) && !WorldGen.genRand.NextBool(chance)) {
            return false;
        }

        bool success = chest.TryAddItem(type, stack);

        if (success) {
            ChestLootSystem.RegisterLootItem(type, stack);
        }

        return success;
    }

    public static bool TryGetEmptySlot(this Chest chest, out int index) {
        for (int i = 0; i < Chest.maxItems; i++) {
            Item item = chest.item[i];

            if (item != null && item.IsAir) {
                index = i;

                return true;
            }
        }

        index = -1;

        return false;
    }
}
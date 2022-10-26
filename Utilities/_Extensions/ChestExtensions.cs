using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace AllBeginningsMod.Utilities;

public static class ChestExtensions
{
    private static readonly Dictionary<int, int> itemAmountByType;

    private static readonly Action<Item[], int[]> sortAction;

    static ChestExtensions() {
        itemAmountByType = new Dictionary<int, int>();

        MethodInfo sortMethodInfo = typeof(ItemSorting).GetMethod("Sort", BindingFlags.Static | BindingFlags.NonPublic);
        sortAction = sortMethodInfo.CreateDelegate<Action<Item[], int[]>>();
    }

    public static bool TrySort(this Chest chest, params int[] ignoreSlots) {
        if (!chest.HasAnyItem()) {
            return false;
        }

        sortAction(chest.item, ignoreSlots);

        return true;
    }

    public static bool TryAddItem(this Chest chest, int type, int stack = 1) {
        int index = chest.FindEmptySlotIndex();

        if (index == -1 || type == ItemID.None) {
            return false;
        }

        chest.item[index].SetDefaults(type);
        chest.item[index].stack = stack;

        return true;
    }

    public static bool TryAddShopItem(this Chest chest, int type, ref int nextSlot) {
        if (nextSlot == -1 || type == ItemID.None) {
            return false;
        }

        chest.item[nextSlot].SetDefaults(type);
        nextSlot++;

        return true;
    }

    public static bool TryAddLootItem(this Chest chest, int type, int stack = 1, int chance = 1) {
        bool alreadyExists = itemAmountByType.TryGetValue(type, out int amount) && amount > 0;
        bool shouldAdd = WorldGen.genRand.NextBool(chance);

        if (alreadyExists && !shouldAdd) {
            return false;
        }

        bool success = chest.TryAddItem(type, stack);

        if (success) {
            if (alreadyExists) {
                itemAmountByType[type] += stack;
            }
            else {
                itemAmountByType[type] = stack;
            }
        }

        return success;
    }

    public static bool HasAnyItem(this Chest chest) {
        for (int i = 0; i < Chest.maxItems; i++) {
            Item item = chest.item[i];

            if (!item.IsAir) {
                return true;
            }
        }

        return false;
    }

    public static int FindEmptySlotIndex(this Chest chest) {
        for (int i = 0; i < Chest.maxItems; i++) {
            Item item = chest.item[i];

            if (item.IsAir) {
                return i;
            }
        }

        return -1;
    }
}
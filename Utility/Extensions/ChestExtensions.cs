using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace AllBeginningsMod.Utility.Extensions;

public static class ChestExtensions
{
    private static readonly MethodInfo sortMethodInfo = typeof(ItemSorting).GetMethod("Sort", BindingFlags.Static | BindingFlags.NonPublic);
    
    public static bool TryAddItem(this Chest chest, int type, int stack) {
        if (type == ItemID.None)
            return false;
    
        foreach (Item item in chest.item.Where(item => item.IsAir)) {
            item.SetDefaults(type);
            item.stack = stack;
            return true;
        }
        
        return false;
    }

    public static bool TryRemoveItem(this Chest chest, Predicate<Item> predicate) {
        bool success = false;

        if (!chest.HasAnyItem())
            return success;
        
        foreach (Item item in chest.item.Where(item => !item.IsAir && predicate.Invoke(item))) {
            item.TurnToAir();
            success = true;
        }
        
        return success;
    }

    public static bool TrySort(this Chest chest, params int[] ignoreSlots) {
        if (!chest.HasAnyItem())
            return false;
        
        sortMethodInfo?.Invoke(
            null,
            new object[] {
                chest.item,
                ignoreSlots
            }
        );
        
        return true;
    }

    public static bool HasAnyItem(this Chest chest) {
        return chest.item.Any(item => item != null && !item.IsAir && item.type != ItemID.None);
    }
}
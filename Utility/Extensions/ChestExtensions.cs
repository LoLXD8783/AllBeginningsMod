using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace AllBeginningsMod.Utility.Extensions;

public static class ChestExtensions
{
    public static bool TryAddItem(this Chest chest, int type, int stack) {
        foreach (Item item in chest.item.Where(item => item.IsAir)) {
            item.SetDefaults(type);
            item.stack = stack;
            return true;
        }
        
        return false;
    }

    public static bool TryRemoveItem(this Chest chest, Predicate<Item> predicate) {
        bool success = false;
        
        foreach (Item item in chest.item.Where(item => !item.IsAir && predicate.Invoke(item))) {
            item.TurnToAir();
            success = true;
        }

        return success;
    }
}
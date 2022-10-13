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
    
    /// <summary>
    /// Attempts to add an item to a chest's inventory.
    /// </summary>
    /// <param name="chest">The chest.</param>
    /// <param name="type">The item type.</param>
    /// <param name="stack">The item stack.</param>
    /// <returns>Whether the item has been successfully added to the chest's inventory or not</returns>
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

    /// <summary>
    /// Attempts to remove an item from a chest's inventory.
    /// </summary>
    /// <param name="chest">The chest.</param>
    /// <param name="predicate">The predicate.</param>
    /// <returns>Whether the item has been successfully removed from the chest's inventory or not</returns>
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
    
    /// <summary>
    /// Attempts to sort a chest's inventory.
    /// </summary>
    /// <param name="chest">The chest.</param>
    /// <param name="ignoreSlots">The item slots that should be ignored by sorting.</param>
    /// <returns>Whether the chest's inventory has been successfully sorted or not.</returns>
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

    /// <summary>
    /// Represents whether the chest contains any item in its inventory or not.
    /// </summary>
    /// <param name="chest">The chest.</param>
    /// <returns></returns>
    public static bool HasAnyItem(this Chest chest) {
        return chest.item.Any(item => item != null && !item.IsAir && item.type != ItemID.None);
    }
}
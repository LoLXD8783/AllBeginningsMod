using System.Collections.Generic;

namespace AllBeginningsMod.Utility.Extensions;

public static class ListExtensions
{
    /// <summary>
    /// Attempts to insert an item into a list.
    /// </summary>
    /// <param name="list">The list.</param>
    /// <param name="index">The insertion index.</param>
    /// <param name="item">The item to be inserted.</param>
    /// <typeparam name="T">The generic type.</typeparam>
    /// <returns>Whether the item has been successfully inserted into the list or not.</returns>
    public static bool TryInsert<T>(this List<T> list, int index, T item) {
        if (index != -1) {
            list.Insert(index, item);
            return true;
        }

        return false;
    }
}
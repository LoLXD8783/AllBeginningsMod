using System.Collections.Generic;

namespace AllBeginningsMod.Utility.Extensions;

public static class ListExtensions
{
    public static bool TryInsert<T>(this List<T> list, int index, T item) {
        if (index != -1) {
            list.Insert(index, item);
            return true;
        }

        return false;
    }
}
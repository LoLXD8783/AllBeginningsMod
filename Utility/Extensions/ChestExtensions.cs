using Terraria;

namespace AllBeginningsMod.Utility.Extensions;

public static class ChestExtensions
{
    public static bool TryAddItem(this Chest chest, int type, int stack) {
        for (int i = 0; i < Chest.maxItems; i++) {
            Item item = chest.item[i];

            if (item.IsAir) {
                item.SetDefaults(type);
                item.stack = stack;
                return true;
            }
        }
        
        return false;
    }
}
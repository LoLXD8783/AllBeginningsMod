using Terraria;

namespace AllBeginningsMod.Utilities;

public static class PlayerExtensions
{
    public static bool HasEquip(this Player player, int type) {
        int extra = player.GetAmountOfExtraAccessorySlotsToShow();

        for (int i = 0; i <= 10 + extra; i++) {
            if (player.armor[i].type == type) {
                return true;
            }
        }

        return false;
    }

    public static bool HasVanityEquip(this Player player, int type) {
        int extra = player.GetAmountOfExtraAccessorySlotsToShow();

        for (int i = 10; i <= 17 + extra; i++) {
            if (player.armor[i].type == type) {
                return true;
            }
        }

        return false;
    }
}
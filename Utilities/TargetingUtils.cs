using System;
using AllBeginningsMod.Utilities.Extensions;
using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Utilities;

public static class TargetingUtils
{
    public static void ForEachPlayerInRange(Vector2 center, float range, Action<Player> action) {
        foreach (Player player in Main.player) {
            if (player.InRange(center, range)) {
                action.Invoke(player);
            }
        }
    }
}
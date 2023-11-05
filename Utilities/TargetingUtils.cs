using System;
using AllBeginningsMod.Utilities.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace AllBeginningsMod.Utilities;

public static class TargetingUtils
{
    public static void ForEachPlayerInRange(Vector2 position, float range, Action<Player> action) {
        for (int i = 0; i < Main.maxPlayers; i++) {
            Player player = Main.player[i];
            if (player is null || !player.active || !player.Hitbox.Intersects(position, range)) {
                break;
            }

            action(player);
        }
    }
}
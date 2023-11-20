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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="center"></param>
    /// <param name="player"></param>
    /// <param name="predicate"></param>
    /// <returns>Distance to the closest player.</returns>
    public static float ClosestPlayer(Vector2 center, out Player player, Func<Player, bool> predicate = null) {
        (Player player, float distance)? closest = null;
        for (int i = 0; i <= Main.maxPlayers; i++) {
            Player checkPlayer = Main.player[i];
            if (checkPlayer is null) {
                continue;
            }

            float distance = center.DistanceSQ(checkPlayer.Center);
            if ((closest is null || closest.Value.distance < distance) && (predicate is null || predicate.Invoke(checkPlayer))) {
                closest = (checkPlayer, distance);
            }
        }

        
        if (closest is null) {
            player = null;
            return -1f;
        }

        player = closest.Value.player;
        return closest.Value.distance;
    }
}
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Extensions;
using AllBeginningsMod.Utilities.Extensions;

namespace AllBeginningsMod.Utilities
{
    public static class TargetingUtils
    {
        public static void ForEachPlayerInRange(Vector2 center, float range, Action<Player> action) {
            foreach (Player player in Main.player) {
                if (player.InRange(center, range))
                    action.Invoke(player);
            }
        }
    }
}

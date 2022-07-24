using AllBeginningsMod.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Globals.NPCs
{
    public sealed class SpawnRatesGlobalNPC : GlobalNPC
    {
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns) {
            if (player.TryGetModPlayer(out BuffPlayer buffPlayer) && buffPlayer.DevilGift) {
                spawnRate *= 3;
            }
        }
    }
}
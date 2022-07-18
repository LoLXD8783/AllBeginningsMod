using AllBeginningsMod.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Globals.NPCs
{
    public sealed class SpawnRatesGlobalNPC : GlobalNPC
    {
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (player.GetModPlayer<BuffPlayer>().DevilGift)
            {
                spawnRate *= (int)3f;
            }
        }
    }
}
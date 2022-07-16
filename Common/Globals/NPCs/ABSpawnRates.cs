using AllBeginningsMod.Common.Globals.Players;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Globals.NPCs
{
    public sealed class ABSpawnRates : GlobalNPC
    {
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (player.GetModPlayer<ABAccessoryPlayer>().DevilGift)
            {
                spawnRate *= (int)3f;
            }
        }
    }
}
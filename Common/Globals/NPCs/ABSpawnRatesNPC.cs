using AllBeginningsMod.Common.Globals.Players;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Globals.NPCs
{
    public sealed class ABSpawnRatesNPC : GlobalNPC
    {
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (player.GetModPlayer<ABBuffPlayer>().DevilGift)
            {
                spawnRate *= (int)3f;
            }
        }
    }
}
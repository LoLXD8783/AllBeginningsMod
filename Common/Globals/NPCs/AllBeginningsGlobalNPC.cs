using Terraria;
using Terraria.ModLoader;
using AllBeginningsMod.Common.Globals.Players;

namespace AllBeginningsMod.Common.Globals.NPCs
{
    public sealed class AllBeginningsGlobalNPC : GlobalNPC
    {
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (player.GetModPlayer<AllBeginningsGlobalPlayer>().DevilGift == true)
            {
                spawnRate *= (int)3f;
            }
        }
    }
}

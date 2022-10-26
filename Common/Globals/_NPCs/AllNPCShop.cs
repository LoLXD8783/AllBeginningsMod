using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Globals;

public sealed class AllNPCShop : GlobalNPC
{
    public override void SetupShop(int type, Chest shop, ref int nextSlot) {
        switch (type) {
            case NPCID.BestiaryGirl:
                break;
        }
    }
}
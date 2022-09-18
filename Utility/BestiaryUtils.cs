using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;

namespace AllBeginningsMod.Utility;

public static class BestiaryUtils
{
    public static BestiaryEntryUnlockState GetUnlockState(int type) {
        return Main.BestiaryDB.FindEntryByNPCID(NPCID.FromNetId(type)).UIInfoProvider.GetEntryUICollectionInfo().UnlockState;
    }
}
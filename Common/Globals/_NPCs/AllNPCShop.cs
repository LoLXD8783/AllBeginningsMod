using AllBeginningsMod.Content.Items.Accessories;
using AllBeginningsMod.Utilities;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Globals;

public sealed class AllNPCShop : GlobalNPC
{
    public override void SetupShop(int type, Chest shop, ref int nextSlot) {
        switch (type) {
            case NPCID.BestiaryGirl:
                AddCritterItem<FeatherCharmItem>(shop, NPCID.Bird, ref nextSlot);
                AddCritterItem<RabbitsFootItem>(shop, NPCID.Bunny, ref nextSlot);
                AddCritterItem<SnailsShellItem>(shop, NPCID.Snail, ref nextSlot);
                AddCritterItem<MagmaShellItem>(shop, NPCID.MagmaSnail, ref nextSlot);
                AddCritterItem<FinoftheDolphinItem>(shop, NPCID.Dolphin, ref nextSlot);
                AddCritterItem<BrownFeatherWingsItem>(shop, NPCID.Duck, ref nextSlot);
                AddCritterItem<ClearFeatherWingsItem>(shop, NPCID.DuckWhite, ref nextSlot);
                AddCritterItem<DarkFeatherWingsItem>(shop, NPCID.Grebe, ref nextSlot);

                break;
        }
    }

    private static void AddCritterItem<T>(Chest shop, int critterType, ref int nextSlot) where T : ModItem {
        BestiaryEntry entry = Main.BestiaryDB.FindEntryByNPCID(NPCID.FromNetId(critterType));

        if (entry.UIInfoProvider.GetEntryUICollectionInfo().UnlockState != BestiaryEntryUnlockState.CanShowDropsWithDropRates_4) {
            return;
        }

        shop.TryAddShopItem(ModContent.ItemType<T>(), ref nextSlot);
    }
}
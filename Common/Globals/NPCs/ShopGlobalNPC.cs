using AllBeginningsMod.Content.Items.Accessories;
using AllBeginningsMod.Content.Items.Accessories.Wings;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Globals.NPCs;

public sealed class ShopGlobalNPC : GlobalNPC
{
    public override void SetupShop(int type, Chest shop, ref int nextSlot) {
        if (type == NPCID.BestiaryGirl) {
            void AddZoologistCritterItem(int critterType, int itemType, ref int nextSlot) {
                BestiaryEntry critterEntry = Main.BestiaryDB.FindEntryByNPCID(NPCID.FromNetId(critterType));

                if (critterEntry.UIInfoProvider.GetEntryUICollectionInfo().UnlockState ==
                    BestiaryEntryUnlockState.CanShowDropsWithDropRates_4) {
                    shop.item[nextSlot].SetDefaults(itemType);
                    nextSlot++;
                }
            }

            AddZoologistCritterItem(NPCID.Bird, ModContent.ItemType<FeatherCharmItem>(), ref nextSlot);
            AddZoologistCritterItem(NPCID.Bunny, ModContent.ItemType<RabbitsFootItem>(), ref nextSlot);
            AddZoologistCritterItem(NPCID.Snail, ModContent.ItemType<SnailsShellItem>(), ref nextSlot);
            AddZoologistCritterItem(NPCID.MagmaSnail, ModContent.ItemType<MagmaShellItem>(), ref nextSlot);
            AddZoologistCritterItem(NPCID.Duck, ModContent.ItemType<BrownFeatherWingsItem>(), ref nextSlot);
            AddZoologistCritterItem(NPCID.Grebe, ModContent.ItemType<DarkFeatherWingsItem>(), ref nextSlot);
            AddZoologistCritterItem(NPCID.Dolphin, ModContent.ItemType<FinoftheDolphinItem>(), ref nextSlot);
            AddZoologistCritterItem(NPCID.DuckWhite, ModContent.ItemType<ClearFeatherWingsItem>(), ref nextSlot);
        }
    }
}
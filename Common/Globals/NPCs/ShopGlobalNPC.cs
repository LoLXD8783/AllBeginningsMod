using AllBeginningsMod.Content.Items.Accessories;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Globals.NPCs
{
    public sealed class ShopGlobalNPC : GlobalNPC
    {
        public override void SetupShop(int type, Chest shop, ref int nextSlot) {
            if (type == NPCID.BestiaryGirl) {
                AddZoologistCritterItem(NPCID.Bunny, ModContent.ItemType<RabbitsFootItem>(), shop, ref nextSlot);
                AddZoologistCritterItem(NPCID.Snail, ModContent.ItemType<SnailsShellItem>(), shop, ref nextSlot);
                AddZoologistCritterItem(NPCID.MagmaSnail, ModContent.ItemType<MagmaShellItem>(), shop, ref nextSlot);
                AddZoologistCritterItem(NPCID.DuckWhite, ModContent.ItemType<ClearFeatherWingsItem>(), shop, ref nextSlot);
                AddZoologistCritterItem(NPCID.Duck, ModContent.ItemType<BrownFeatherWingsItem>(), shop, ref nextSlot);
                AddZoologistCritterItem(NPCID.Grebe, ModContent.ItemType<DarkFeatherWingsItem>(), shop, ref nextSlot);
            }
        }

        private static void AddZoologistCritterItem(int critterType, int itemType, Chest shop, ref int nextSlot) {
            BestiaryEntry critterEntry = Main.BestiaryDB.FindEntryByNPCID(NPCID.FromNetId(critterType));

            if (critterEntry.UIInfoProvider.GetEntryUICollectionInfo().UnlockState == BestiaryEntryUnlockState.CanShowDropsWithDropRates_4) {
                shop.item[nextSlot].SetDefaults(itemType);
                nextSlot++;
            }
        }
    }
}
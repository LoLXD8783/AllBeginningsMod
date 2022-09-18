using AllBeginningsMod.Content.Items.Accessories;
using AllBeginningsMod.Utility;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Globals.NPCs;

public sealed class AllNPCShop : GlobalNPC
{
    public override void SetupShop(int type, Chest shop, ref int nextSlot) {
        if (type == NPCID.BestiaryGirl) {
            AddCritterItemToShop(NPCID.Bird, ModContent.ItemType<FeatherCharmItem>(), ref nextSlot);
            AddCritterItemToShop(NPCID.Bunny, ModContent.ItemType<RabbitsFootItem>(), ref nextSlot);
            AddCritterItemToShop(NPCID.Snail, ModContent.ItemType<SnailsShellItem>(), ref nextSlot);
            AddCritterItemToShop(NPCID.MagmaSnail, ModContent.ItemType<MagmaShellItem>(), ref nextSlot);
            AddCritterItemToShop(NPCID.Dolphin, ModContent.ItemType<FinoftheDolphinItem>(), ref nextSlot);

            void AddCritterItemToShop(int critterType, int itemType, ref int nextSlot) {
                if (BestiaryUtils.GetUnlockState(critterType) == BestiaryEntryUnlockState.CanShowDropsWithDropRates_4) {
                    AddItemToShop(itemType, ref nextSlot);
                }
            }
        }

        void AddItemToShop(int type, ref int nextSlot) {
            shop.item[nextSlot].SetDefaults(type);
            nextSlot++;
        }
    }
}
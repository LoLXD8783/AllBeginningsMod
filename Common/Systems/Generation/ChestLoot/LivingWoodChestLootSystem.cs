using AllBeginningsMod.Content.Items.Accessories;
using AllBeginningsMod.Content.Items.Consumables;
using AllBeginningsMod.Content.Items.Weapons.Summon;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Generation.ChestLoot
{
    public sealed class LivingWoodChestLootSystem : ChestLootSystem
    {
        protected override int ChestFrameX => 12 * 36;

        protected override void AddLootEntries() {
            LootEntries.Add(new ItemChestLootEntry(ModContent.ItemType<PlumeWhipItem>(), 1, 1, 3, 4));
            LootEntries.Add(new ItemChestLootEntry(ModContent.ItemType<MidasPouchItem>(), 3, 6, 10, 4));
            LootEntries.Add(new ItemChestLootEntry(ModContent.ItemType<PegasusBootsItem>(), 1, 1, 5, 4));
        }
    }
}
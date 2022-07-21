using AllBeginningsMod.Content.Items.Accessories;
using AllBeginningsMod.Content.Items.Consumables;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Generation.ChestLoot
{
    public sealed class LivingWoodChestLootSystem : ChestLootSystem
    {
        public override int ChestFrameX => 12 * 36;

        public override void AddLootEntries() {
            LootEntries.Add(new ItemChestLootEntry(ModContent.ItemType<PegasusBootsItem>()));
            LootEntries.Add(new ItemChestLootEntry(ModContent.ItemType<MidasPouchItem>(), 2, 6));
        }
    }
}
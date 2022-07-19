using AllBeginningsMod.Content.Items.Accessories;
using AllBeginningsMod.Content.Items.Consumables;
using AllBeginningsMod.Content.Items.Weapons.Summon;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Generation.ChestLoot
{
    public sealed class WoodChestLootSystem : ChestLootSystem
    {
        protected override int ChestFrameX => 0;

        protected override void AddLootEntries() {
            LootEntries.Add(new ItemChestLootEntry(ModContent.ItemType<PlumeWhipItem>(), 1, 1, 4));
            LootEntries.Add(new ItemChestLootEntry(ModContent.ItemType<MidasPouchItem>(), 2, 6, 4));
            LootEntries.Add(new ItemChestLootEntry(ModContent.ItemType<PegasusBootsItem>(), 1, 1, 4));
        }
    }
}
using AllBeginningsMod.Content.Items.Accessories;
using AllBeginningsMod.Content.Items.Consumables;
using AllBeginningsMod.Content.Items.Weapons.Summon;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.WorldGeneration.ChestLoot;

public sealed class LivingWoodChestLootSystem : ChestLootSystem
{
    public override int ChestFrameX => 12 * 36;

    public override void AddLootEntries() {
        LootEntries.Add(new ItemChestLootEntry(ModContent.ItemType<MidasPouchItem>(), 2, 6, 2));
        LootEntries.Add(new ItemChestLootEntry(ModContent.ItemType<PlumeWhipItem>(), 1, 1, 2));
        LootEntries.Add(new ItemChestLootEntry(ModContent.ItemType<PegasusBootsItem>(), 1, 1, 4));
    }
}
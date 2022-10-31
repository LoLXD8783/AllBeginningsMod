using AllBeginningsMod.Content.Items.Accessories;
using AllBeginningsMod.Content.Items.Consumables;
using AllBeginningsMod.Content.Items.Weapons.Summon;
using AllBeginningsMod.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Generation;

public readonly struct WoodChestLoot : IChestLoot
{
    public void SetLoot(Chest chest) {
        chest.TryAddLootItem(ModContent.ItemType<MidasPouchItem>(), WorldGen.genRand.Next(3, 6), 2);
        chest.TryAddLootItem(ModContent.ItemType<PegasusBootsItem>(), 1, 4);
        chest.TryAddLootItem(ModContent.ItemType<PlumeWhipItem>(), 1, 4);
    }
}
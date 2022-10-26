using AllBeginningsMod.Content.Items.Accessories;
using AllBeginningsMod.Content.Items.Consumables;
using AllBeginningsMod.Content.Items.Weapons.Summon;
using AllBeginningsMod.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.WorldGeneration;

public sealed class LivingWoodChestLoot : ChestLoot
{
    public override int ChestFrame => 12 * 36;

    public override void SetLoot(Chest chest) {
        TryAddItem(chest, ModContent.ItemType<MidasPouchItem>(), WorldGen.genRand.Next(3, 6), 4);
        TryAddItem(chest, ModContent.ItemType<PegasusBootsItem>(), 1, 4);
        TryAddItem(chest, ModContent.ItemType<PlumeWhipItem>(), 1, 4);

        chest.TrySort();
    }
}
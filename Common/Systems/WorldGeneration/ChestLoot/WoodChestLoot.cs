using AllBeginningsMod.Content.Items.Accessories;
using AllBeginningsMod.Content.Items.Consumables;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.WorldGeneration.ChestLoot;

public sealed class WoodChestLoot : ChestLoot
{
    public override int ChestFrame => 0;

    public override void SetLoot(Chest chest) {
        TryAddItem(chest, ModContent.ItemType<MidasPouchItem>(), WorldGen.genRand.Next(3, 6), 4);
        TryAddItem(chest, ModContent.ItemType<PegasusBootsItem>(), 1, 4);
    }
}
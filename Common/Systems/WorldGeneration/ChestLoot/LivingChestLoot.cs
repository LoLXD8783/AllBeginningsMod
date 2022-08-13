using AllBeginningsMod.Content.Items.Accessories;
using AllBeginningsMod.Content.Items.Consumables;
using AllBeginningsMod.Content.Items.Weapons.Summon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.WorldGeneration.ChestLoot;

public sealed class LivingWoodChestLoot : ChestLoot
{
    public override int ChestFrame => 12 * 36;

    public override void SetLoot(Chest chest) {
        AddItemToChest(chest, ModContent.ItemType<MidasPouchItem>(), WorldGen.genRand.Next(3, 6), 4);
        AddItemToChest(chest, ModContent.ItemType<PegasusBootsItem>(), 1, 4);
        AddItemToChest(chest, ModContent.ItemType<PlumeWhipItem>(), 1, 4);
    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Accessories;

public sealed class PegasusBootsItem : ModItem
{
    public override void SetDefaults() {
        Item.canBePlacedInVanityRegardlessOfConditions = true;
        Item.accessory = true;

        Item.width = 32;
        Item.height = 28;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(silver: 80);
    }

    public override void UpdateEquip(Player player) {
        player.accRunSpeed = 4f;
        player.moveSpeed += 0.05f;
    }
}
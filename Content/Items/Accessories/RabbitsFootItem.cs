using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Accessories;

public sealed class RabbitsFootItem : ModItem
{
    public override void SetStaticDefaults() {
        SacrificeTotal = 1;
    }

    public override void SetDefaults() {
        Item.canBePlacedInVanityRegardlessOfConditions = true;
        Item.accessory = true;

        Item.width = 34;
        Item.height = 32;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(silver: 80);
    }

    public override void UpdateEquip(Player player) {
        player.luck += 0.05f;
        player.moveSpeed += 0.05f;
        player.jumpSpeedBoost += 0.05f;
    }
}
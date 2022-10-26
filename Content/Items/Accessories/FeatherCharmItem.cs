using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Accessories;

public sealed class FeatherCharmItem : ModItem
{
    public override void SetStaticDefaults() {
        SacrificeTotal = 1;
    }

    public override void SetDefaults() {
        Item.canBePlacedInVanityRegardlessOfConditions = true;
        Item.accessory = true;

        Item.width = 18;
        Item.height = 24;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(silver: 60);
    }

    public override void UpdateEquip(Player player) {
        player.moveSpeed += 0.1f;
    }
}
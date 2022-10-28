using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Accessories;

public sealed class MagmaShellItem : ModItem
{
    public override void SetDefaults() {
        Item.canBePlacedInVanityRegardlessOfConditions = true;
        Item.accessory = true;

        Item.defense = 8;

        Item.width = 62;
        Item.height = 32;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(gold: 1);
    }

    public override void UpdateEquip(Player player) {
        player.lavaMax += 210;
        player.moveSpeed -= 0.2f;
    }
}
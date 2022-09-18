using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Accessories;

public sealed class SnailsShellItem : ModItem
{
    public override void SetStaticDefaults() {
        SacrificeTotal = 1;
    }

    public override void SetDefaults() {
        Item.accessory = true;

        Item.defense = 8;

        Item.width = 26;
        Item.height = 24;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(gold: 1);
    }

    public override void UpdateEquip(Player player) {
        player.endurance += 0.1f;
        player.moveSpeed -= 0.2f;
    }
}
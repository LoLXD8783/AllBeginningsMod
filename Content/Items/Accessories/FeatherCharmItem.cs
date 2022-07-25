using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Accessories;

public sealed class FeatherCharmItem : ModItem
{
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("Feather Charm");
        Tooltip.SetDefault("Greatly increases movement speed");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
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
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Accessories;

public sealed class RabbitsFootItem : ModItem
{
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("Rabbit's Foot");
        Tooltip.SetDefault("Increases movement speed, jump speed and luck");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.accessory = true;

        Item.width = 34;
        Item.height = 32;

        Item.value = Item.sellPrice(silver: 80);
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateEquip(Player player) {
        player.luck += 0.05f;
        player.moveSpeed += 0.05f;
        player.jumpSpeedBoost += 0.05f;
    }
}
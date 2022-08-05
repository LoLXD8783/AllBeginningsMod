using AllBeginningsMod.Common.Bases.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace AllBeginningsMod.Content.Items.Accessories;

public sealed class PegasusBootsItem : ModItemBase
{
    public override void SetStaticDefaults() {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.accessory = true;

        Item.width = 32;
        Item.height = 28;

        Item.value = Item.sellPrice(silver: 80);
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateEquip(Player player) {
        player.accRunSpeed = 4f;
        player.moveSpeed += 0.05f;
    }
}
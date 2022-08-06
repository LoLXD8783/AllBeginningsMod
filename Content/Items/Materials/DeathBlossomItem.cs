using AllBeginningsMod.Common.Bases.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace AllBeginningsMod.Content.Items.Materials;

public sealed class DeathBlossomItem : ModItemBase
{
    public override void SetStaticDefaults() {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;
    }

    public override void SetDefaults() {
        Item.maxStack = 999;

        Item.width = 28;
        Item.height = 42;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(silver: 8);
    }
}
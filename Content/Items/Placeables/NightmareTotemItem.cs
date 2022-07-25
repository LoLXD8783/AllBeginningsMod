using AllBeginningsMod.Content.Tiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Placeables;

public sealed class NightmareTotemItem : ModItem
{
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("Nightmare Totem");
        Tooltip.SetDefault("The harbinger of terror");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
    }

    public override void SetDefaults() {
        Item.consumable = true;
        Item.useTurn = true;

        Item.maxStack = 999;

        Item.width = 26;
        Item.height = 64;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(silver: 10);

        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.createTile = ModContent.TileType<NightmareTotemTile>();
    }
}
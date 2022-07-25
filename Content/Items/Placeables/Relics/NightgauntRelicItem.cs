using AllBeginningsMod.Content.Tiles.Relics;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Placeables.Relics;

public sealed class NightgauntRelicItem : ModItem
{
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("Nightgaunt Relic");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
    }

    public override void SetDefaults() {
        Item.consumable = true;
        Item.useTurn = true;

        Item.maxStack = 99;

        Item.width = 46;
        Item.height = 42;

        Item.rare = ItemRarityID.Master;
        Item.value = Item.sellPrice(gold: 1);

        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.createTile = ModContent.TileType<NightgauntRelicTile>();
    }
}
using AllBeginningsMod.Content.Tiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Placeables
{
    public sealed class MysteriousTotemItem : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Mysterious Totem");
            Tooltip.SetDefault("Ancient relic of unknown origins");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
        }

        public override void SetDefaults() {
            Item.maxStack = 999;

            Item.width = 24;
            Item.height = 50;

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 6);

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.createTile = ModContent.TileType<MysteriousTotemTile>();
        }
    }
}
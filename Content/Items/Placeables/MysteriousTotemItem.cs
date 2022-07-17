using AllBeginningsMod.Content.Tiles;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Creative;
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
            Item.SetShopValues(ItemRarityColor.Blue1, Item.sellPrice(silver: 6));
            Item.DefaultToPlaceableTile(ModContent.TileType<MysteriousTotemTile>());
        }
    }
}
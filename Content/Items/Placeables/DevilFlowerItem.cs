using AllBeginningsMod.Content.Tiles;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Placeables
{
    public sealed class DevilFlowerItem : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Devil Flower");
            Tooltip.SetDefault("Flower gifted of an aggressive aroma" + "\nIncreases damage and enemy spawns when nearby");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
        }

        public override void SetDefaults() {
            Item.SetShopValues(ItemRarityColor.Blue1, Item.sellPrice(gold: 1, silver: 80));
            Item.DefaultToPlaceableTile(ModContent.TileType<DevilFlowerTile>());
        }
    }
}
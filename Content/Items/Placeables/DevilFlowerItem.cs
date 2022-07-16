using AllBeginningsMod.Content.Tiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
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
            Item.maxStack = 999;

            Item.width = 26;
            Item.height = 30;

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 30);

            Item.useTime = Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.createTile = ModContent.TileType<DevilFlowerTile>();
        }
    }
}
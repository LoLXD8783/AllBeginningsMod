using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Materials
{
    public sealed class DeathBlossomItem : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Death Blossom");
            Tooltip.SetDefault("Contains the essence of the ones who survived death");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;
        }

        public override void SetDefaults() {
            Item.maxStack = 999;

            Item.width = 28;
            Item.height = 42;

            Item.SetShopValues(ItemRarityColor.Blue1, Item.sellPrice(silver: 8));
        }
    }
}
using AllBeginningsMod.Common.Systems;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Accessories
{
    public sealed class PegasusBootsItem : ModItem
    {
        public override void SetStaticDefaults() {  
            DisplayName.SetDefault("Pegasus Boots");
            Tooltip.SetDefault("The wearer can run fast");

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
            player.moveSpeed += 0.05f;
            player.accRunSpeed = 4f;
        }

        public override void AddRecipes() {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Silk, 15);
            recipe.AddIngredient(ItemID.Feather, 12);
            recipe.AddRecipeGroup(RecipeGroupSystem.PlatinumBarGroup.ID, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
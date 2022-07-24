using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Armor.Scavenger
{
    public sealed class ScavengerChestplateItem : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Scavenger Chestplate");
        }

        public override void SetDefaults() {
            Item.defense = 5;

            Item.width = 36;
            Item.height = 40;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 50);
        }

        public override void AddRecipes() {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Silk, 8);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
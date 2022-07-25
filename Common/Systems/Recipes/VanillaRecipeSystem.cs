using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Recipes;

public sealed class VanillaRecipeSystem : ModSystem
{
    public override void AddRecipes() {
        Recipe recipe = Recipe.Create(ItemID.HermesBoots);
        recipe.AddIngredient(ItemID.Silk, 15);
        recipe.AddIngredient(ItemID.Feather, 12);
        recipe.AddRecipeGroup(RecipeGroupSystem.PlatinumBarGroup.ID, 5);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}
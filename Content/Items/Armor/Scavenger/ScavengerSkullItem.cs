using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Armor.Scavenger;

public sealed class ScavengerSkullItem : ModItem
{
    public override void SetStaticDefaults() => DisplayName.SetDefault("Scavenger Skull");

    public override void SetDefaults() {
        Item.defense = 3;

        Item.width = 22;
        Item.height = 20;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(silver: 45);
    }

    public override void AddRecipes() {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Silk, 5);
        recipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
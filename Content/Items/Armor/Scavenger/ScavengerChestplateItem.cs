using AllBeginningsMod.Common.Bases.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace AllBeginningsMod.Content.Items.Armor.Scavenger;

public sealed class ScavengerChestplateItem : ModItemBase
{
    public override void SetStaticDefaults() {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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
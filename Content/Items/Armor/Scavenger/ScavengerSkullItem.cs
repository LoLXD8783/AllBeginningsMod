using AllBeginningsMod.Common.Bases.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Armor.Scavenger;

[AutoloadEquip(EquipType.Head)]
public sealed class ScavengerSkullItem : ModItemBase
{
    public override void SetStaticDefaults() {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

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
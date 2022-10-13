using AllBeginningsMod.Common.Bases.Items;
using AllBeginningsMod.Content.Items.Materials;
using AllBeginningsMod.Content.Projectiles.Ranged;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Ranged;

public sealed class IcicleClimbingAxeItem : ModItemBase
{
    public override void SetStaticDefaults() {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.noMelee = true;
        Item.noUseGraphic = true;

        Item.maxStack = 1;

        Item.DamageType = DamageClass.Ranged;
        Item.damage = 16;
        Item.knockBack = 2f;

        Item.width = 46;
        Item.height = 38;

        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shoot = ModContent.ProjectileType<IcicleClimbingAxeProjectile>();
        Item.shootSpeed = 14f;

        Item.value = Item.sellPrice(gold: 1, silver: 12);
        Item.rare = ItemRarityID.Blue;

        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes() {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<SnowflakeItem>(), 18);
        recipe.AddRecipeGroup(RecipeGroupID.Wood, 12);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}
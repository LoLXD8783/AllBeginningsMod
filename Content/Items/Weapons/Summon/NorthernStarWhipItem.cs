using AllBeginningsMod.Common.Bases.Items;
using AllBeginningsMod.Content.Items.Materials;
using AllBeginningsMod.Content.Projectiles.Summon.Whips;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Summon;

public sealed class NorthernStarWhipItem : ModItemBase
{
    public override void SetStaticDefaults() {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.noMelee = true;
        Item.autoReuse = false;
        Item.noUseGraphic = true;

        Item.width = 48;
        Item.height = 44;

        Item.DamageType = DamageClass.SummonMeleeSpeed;
        Item.damage = 16;
        Item.knockBack = 0.5f;

        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shootSpeed = 3f;
        Item.shoot = ModContent.ProjectileType<NorthernStarWhipProjectile>();

        Item.UseSound = SoundID.Item152;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(silver: 20);
    }
    public override void AddRecipes() {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<SnowflakeItem>(), 16);
        recipe.AddRecipeGroup(RecipeGroupID.Wood, 12);
        recipe.AddTile(TileID.Bookcases);
        recipe.Register();
    }
}
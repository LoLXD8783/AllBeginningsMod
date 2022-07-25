using AllBeginningsMod.Content.Projectiles.Summon.Whips;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Summon;

public sealed class TimberWhipItem : ModItem
{
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("Timber Whip");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.noMelee = true;
        Item.autoReuse = false;
        Item.noUseGraphic = true;

        Item.width = 36;
        Item.height = 46;

        Item.DamageType = DamageClass.SummonMeleeSpeed;
        Item.damage = 8;
        Item.knockBack = 0.5f;

        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shootSpeed = 2.5f;
        Item.shoot = ModContent.ProjectileType<TimberWhipProjectile>();

        Item.UseSound = SoundID.Item152;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(silver: 20);
    }

    public override void AddRecipes() {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Wood, 12);
        recipe.AddIngredient(ItemID.Rope, 10);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}
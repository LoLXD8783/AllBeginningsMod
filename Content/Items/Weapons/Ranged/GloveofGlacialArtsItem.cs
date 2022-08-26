using AllBeginningsMod.Common.Bases.Items;
using AllBeginningsMod.Content.Items.Materials;
using AllBeginningsMod.Content.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Ranged;

public sealed class GloveofGlacialArtsItem : ModItemBase
{
    public override void SetStaticDefaults() {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.noMelee = true;
        Item.noUseGraphic = true;

        Item.DamageType = DamageClass.Ranged;
        Item.damage = 12;
        Item.knockBack = 3f;

        Item.width = 32;
        Item.height = 32;

        Item.useTime = 12;
        Item.useAnimation = 12;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shoot = ModContent.ProjectileType<GloveofGlacialArtsShurikenProjectile>();
        Item.shootSpeed = 10f;

        Item.rare = ItemRarityID.Blue;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
        type = Main.rand.Next(new int[] { type, ModContent.ProjectileType<GloveofGlacialArtsScytheProjectile>(), ModContent.ProjectileType<GloveofGlacialArtsDartProjectile>() });
        Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        return false;
    }
    public override void AddRecipes() {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<SnowflakeItem>(), 14);
        recipe.AddIngredient(ItemID.Leather, 6);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}
using AllBeginningsMod.Common.Bases.Items;
using AllBeginningsMod.Content.Items.Materials;
using AllBeginningsMod.Content.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Magic
{
    public sealed class TomeofCryomancyItem : ModItemBase
    {
        public override void SetStaticDefaults() {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults() {
            Item.damage = 10;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 3;

            Item.autoReuse = true;
            Item.knockBack = 2f;

            Item.width = 32;
            Item.height = 40;

            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.shoot = ModContent.ProjectileType<TomeofCryomancyProjectile>();
            Item.shootSpeed = 10f;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            Vector2 positionNew = new Vector2(Main.rand.NextFloat(-16 * 3, 16 * 3), Main.rand.NextFloat(-16 * 3, 16 * 3));
            Projectile.NewProjectile(source, position + positionNew, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes() {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SnowflakeItem>(), 15);
            recipe.AddIngredient(ItemID.Leather, 4);
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }
    }
}

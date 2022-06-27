using AllBeginningsMod.Content.Items.Materials;
using AllBeginningsMod.Content.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace AllBeginningsMod.Content.Items.Weapons.Ranged
{
    public sealed class DarkBombshellItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Bombshell");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.noUseGraphic = true;
            Item.noMelee = true;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 30;
            Item.knockBack = 8f;

            Item.width = 32;
            Item.height = 46;

            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.shoot = ModContent.ProjectileType<DarkBombshellProjectile>();
            Item.shootSpeed = 5f;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(gold: 1, silver: 80);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DeathBlossomItem>(3);
            recipe.AddIngredient(ItemID.Grenade, 5);
            recipe.AddTile(TileID.Tombstones);
            recipe.Register();
        }
    }
}
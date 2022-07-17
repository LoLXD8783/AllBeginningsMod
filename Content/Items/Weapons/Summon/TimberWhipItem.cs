using AllBeginningsMod.Content.Projectiles.Summon;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Summon
{
    public sealed class TimberWhipItem : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Timber Whip");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults() {
            Item.DefaultToWhip(ModContent.ProjectileType<TimberWhipProjectile>(), 8, 2, 3, 25);
            Item.SetWeaponValues(8, 2f);
            Item.SetShopValues(ItemRarityColor.Blue1, Item.sellPrice(silver: 20));

            Item.width = 36;
            Item.height = 46;

            Item.DamageType = DamageClass.SummonMeleeSpeed;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override void AddRecipes() {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 12);
            recipe.AddIngredient(ItemID.Rope, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
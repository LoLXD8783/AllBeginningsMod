using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace AllBeginningsMod.Content.Items.Weapons.Magic
{
    public class SpiritButterflyWand : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Magic;
            Item.damage = 13;
            Item.mana = 2;
            Item.knockBack = 1f;

            Item.width = Item.height = 32;

            Item.useTime = Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.shoot = ModContent.ProjectileType<Projectiles.Magic.SpiritButterflyWandProj>();
            Item.shootSpeed = 10f;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 50);
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Mod.CreateRecipe(ModContent.ItemType<SpiritButterflyWand>());
            recipe.AddIngredient(ItemID.WandofSparking);
            recipe.AddIngredient(ModContent.ItemType<Materials.DeathBlossom>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
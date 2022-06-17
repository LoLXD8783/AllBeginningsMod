using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Content.Items.Weapons.Melee
{
    public class NightPiercer : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.shootSpeed = 8f;
            Item.knockBack = 6.5f;
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 10);

            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;

            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.NightPiercerProj>();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useAnimation = 28;
                Item.useTime = 28;
                Item.damage = 10;
                Item.shoot = ModContent.ProjectileType<Projectiles.Melee.NightPiercerThrown>();
                Item.shootSpeed = 10f;
            }
            else 
            {
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.useAnimation = 20;
                Item.useTime = 20;
                Item.damage = 15;
                Item.shoot = ModContent.ProjectileType<Projectiles.Melee.NightPiercerProj>();
                Item.shootSpeed = 8f;
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = Mod.CreateRecipe(ModContent.ItemType<NightPiercer>());
            recipe.AddIngredient(ItemID.Spear);
            recipe.AddIngredient(ModContent.ItemType<Materials.DeathEssence>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}

using AllBeginningsMod.Content.Projectiles.Melee;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Melee
{
    public sealed class GraveShieldItem : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Grave Shield");
            Tooltip.SetDefault("Throwing this shield causes unstable energy to leak");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults() {
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.damage = 24;
            Item.DamageType = DamageClass.Melee;

            Item.knockBack = 3f;

            Item.width = 32;
            Item.height = 32;

            Item.useTime = 60;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<GraveShieldThrownProjectile>();

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(gold: 2, silver: 10);
        }
    }
}
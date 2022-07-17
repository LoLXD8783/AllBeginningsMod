using AllBeginningsMod.Content.Projectiles.Ranged;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Ranged
{
    public class BorealTurnipItem : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Boreal Turnip");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults() {
            Item.SetShopValues(ItemRarityColor.Blue1, Item.sellPrice(gold: 1, silver: 80));
            Item.SetWeaponValues(10, 3f);

            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.DamageType = DamageClass.Ranged;

            Item.width = 36;
            Item.height = 36;

            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.shoot = ModContent.ProjectileType<BorealTurnipProjectile>();
            Item.shootSpeed = 10f;
        }
    }
}
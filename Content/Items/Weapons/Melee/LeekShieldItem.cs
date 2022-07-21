using AllBeginningsMod.Content.Projectiles.Melee;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Melee
{
    public sealed class LeekShieldItem : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Leek Shield");
            Tooltip.SetDefault("I'm sure some bird would love to hold this" + "\n" + "Splits into returning leaves");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults() {
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.damage = 18;
            Item.DamageType = DamageClass.Melee;

            Item.knockBack = 3f;

            Item.width = 32;
            Item.height = 32;

            Item.useTime = 45;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<LeekShieldThrownProjectile>();

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(gold: 1, silver: 80);
        }
    }
}
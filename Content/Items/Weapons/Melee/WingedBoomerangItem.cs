using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace AllBeginningsMod.Content.Items.Weapons.Melee
{
    public sealed class WingedBoomerangItem : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Winged Boomerang");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults() {
            Item.noUseGraphic = true;
            Item.noMelee = true;

            Item.damage = 12;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.knockBack = 1f;

            Item.width = 18;
            Item.height = 34;

            Item.useTime = 40;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.WingedBoomerangProjectile>();
            Item.shootSpeed = 12f;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 60);
        }
    }
}

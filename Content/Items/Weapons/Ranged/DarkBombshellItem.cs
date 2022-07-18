using AllBeginningsMod.Content.Projectiles.Ranged;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Ranged
{
    public sealed class DarkBombshellItem : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Dark Bombshell");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults() {
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 40;
            Item.knockBack = 6f;

            Item.width = 34;
            Item.height = 46;

            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.shoot = ModContent.ProjectileType<DarkBombshellProjectile>();
            Item.shootSpeed = 6f;

            Item.rare = ItemRarityID.Blue;
        }
    }
}
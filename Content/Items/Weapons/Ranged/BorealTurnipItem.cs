using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace AllBeginningsMod.Content.Items.Weapons.Ranged
{
    public class BorealTurnipItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boreal Turnip");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 3f;
            Item.width = Item.height = 32;
            Item.useTime = Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.BorealTurnipProjectile>();
            Item.shootSpeed = 8f;
            Item.noMelee = Item.noUseGraphic = true;
        }
    }
}

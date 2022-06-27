using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace AllBeginningsMod.Content.Items.Weapons.Melee
{
    public class LeekSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("I'm sure some bird would love to hold this" +
                "\nDash through enemies, killing them in the process");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 1f;
            Item.width = Item.height = 32;
            Item.useTime = Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.LeekSwordProj>();
            Item.shootSpeed = 1f;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 90);
            Item.noUseGraphic = Item.noMelee = true;
        }
    }
}

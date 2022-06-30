using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace AllBeginningsMod.Content.Items.Weapons.Melee
{
    public class LeekShieldItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leek Shield");
            Tooltip.SetDefault("I'm sure some bird would love to hold this" + "\n" + "Can either be used as a weapon or an accessory");
            
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        
        public override void SetDefaults()
        {
            Item.noMelee = true;
            Item.accessory = true;
            Item.noUseGraphic = true;
            
            Item.damage = 18;
            Item.defense = 5;
            Item.DamageType = DamageClass.Melee;

            Item.knockBack = 3f;

            Item.width = 32;
            Item.height = 32;

            Item.useTime = 50;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.LeekShieldProjectile>();
            
            Item.value = Item.sellPrice(gold: 1, silver: 80);
        }
    }
}

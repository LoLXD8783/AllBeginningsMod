using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Melee
{
    public class LeekSwordItem : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Leek Sword");
            Tooltip.SetDefault("I'm sure some bird would love to hold this" + "\n" + "Giving your enemies a swift death");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults() {
            Item.SetWeaponValues(24, 1f);
            Item.SetShopValues(ItemRarityColor.Blue1, Item.sellPrice(gold: 1, silver: 20));

            Item.channel = true;
            Item.noUseGraphic = true;

            Item.DamageType = DamageClass.Melee;

            Item.width = 46;
            Item.height = 48;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
        }
    }
}
using Terraria;
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
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.damage = 24;
            Item.DamageType = DamageClass.Melee;

            Item.knockBack = 1f;

            Item.width = 32;
            Item.height = 32;

            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(gold: 1, silver: 20);
        }
    }
}
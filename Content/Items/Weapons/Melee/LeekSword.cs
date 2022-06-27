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
            Tooltip.SetDefault("I'm sure some bird would love to hold this");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 1f;
            Item.width = Item.height = 32;
            Item.useTime = Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(silver: 90);
        }
    }
}

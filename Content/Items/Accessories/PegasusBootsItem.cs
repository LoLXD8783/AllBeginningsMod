using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Accessories
{
    public sealed class PegasusBootsItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pegasus Boots");
            Tooltip.SetDefault("The wearer can run fast");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = Item.height = 32;
            Item.value = Item.sellPrice(silver: 80);
            Item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.05f;
            player.accRunSpeed = 4f;
        }
    }
}
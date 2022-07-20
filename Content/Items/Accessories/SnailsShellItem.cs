using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Accessories
{
    public sealed class SnailsShellItem : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Snail's Shell");
            Tooltip.SetDefault("Increases endurance by 10%, however decreases movement speed by 10%");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults() {
            Item.accessory = true;

            Item.defense = 8;

            Item.width = 26;
            Item.height = 24;

            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
        }

        public override void UpdateEquip(Player player) {
            player.endurance += 0.1f;
            player.moveSpeed -= 0.2f;
        }
    }
}
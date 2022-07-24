using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Accessories
{
    public sealed class MagmaShellItem : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Magma Shell");
            Tooltip.SetDefault("Allows for temporary lava immunity" + "\nThe wearer starts to feel heavier");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults() {
            Item.accessory = true;

            Item.defense = 8;

            Item.width = 62;
            Item.height = 32;

            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
        }

        public override void UpdateEquip(Player player) {
            player.lavaMax += 210;
            player.moveSpeed -= 0.2f;
        }
    }
}
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Accessories
{
    public sealed class FinoftheDolphinItem : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Fin of the Dolphin");
            Tooltip.SetDefault("Grants free water movement" + "\nIncreases damage while submerged in water");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults() {
            Item.accessory = true;

            Item.width = 18;
            Item.height = 24;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 80);
        }

        public override void UpdateEquip(Player player) {
            player.ignoreWater = true;

            if (player.wet) {
                Main.NewText("A");
                player.GetDamage(DamageClass.Generic) += 0.05f;
            }
        }
    }
}
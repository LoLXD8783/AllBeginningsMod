using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Consumables;

public sealed class MidasPouchItem : ModItem
{
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("Midas Pouch");
        Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
    }

    public override void SetDefaults() {
        Item.consumable = true;

        Item.maxStack = 999;

        Item.width = 24;
        Item.height = 28;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(silver: 1);
    }

    public override bool CanRightClick() {
        return true;
    }

    public override void RightClick(Player player) {
        int coinType = Main.rand.Next(100) switch {
            100 => ItemID.PlatinumCoin,
            > 80 => ItemID.GoldCoin,
            > 60 => ItemID.SilverCoin,
            _ => ItemID.CopperCoin
        };

        int coinAmount = coinType switch {
            ItemID.PlatinumCoin => 1,
            ItemID.GoldCoin => Main.rand.Next(5, 10),
            ItemID.SilverCoin => Main.rand.Next(10, 50),
            _ => Main.rand.Next(25, 100)
        };

        player.QuickSpawnItem(player.GetSource_OpenItem(Type), coinType, coinAmount);
    }
}
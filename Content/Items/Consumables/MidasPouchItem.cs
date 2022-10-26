using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Consumables;

public sealed class MidasPouchItem : ModItem
{
    public override void SetStaticDefaults() {
        SacrificeTotal = 20;
    }

    public override void SetDefaults() {
        Item.consumable = true;

        Item.maxStack = 999;

        Item.width = 24;
        Item.height = 28;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(silver: 1);
    }

    public override void RightClick(Player player) {
        int coinType = ItemID.CopperCoin;
        int coinAmount = Main.rand.Next(50, 100);

        if (Main.rand.NextBool(100)) {
            coinType = ItemID.PlatinumCoin;
            coinAmount = Main.rand.NextBool(10) ? 2 : 1;
        }
        else if (Main.rand.NextBool(75)) {
            coinType = ItemID.GoldCoin;
            coinAmount = Main.rand.Next(1, 15);
        }
        else if (Main.rand.NextBool(50)) {
            coinType = ItemID.SilverCoin;
            coinAmount = Main.rand.Next(25, 75);
        }

        player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Type), coinType, coinAmount);
    }

    public override bool CanRightClick() {
        return true;
    }
}
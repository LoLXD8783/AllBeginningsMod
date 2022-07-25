using AllBeginningsMod.Common.Bases.Items;
using AllBeginningsMod.Common.Players;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Accessories;

public sealed class FinoftheDolphinItem : PlayerCostumeItemBase
{
    public override void SetStaticDefaults() {
        base.SetStaticDefaults();

        DisplayName.SetDefault("Fin of the Dolphin");
        Tooltip.SetDefault("Lets you move swiftly in water" + "\nIncreases damage while submerged in water" + "\nTurns the user into a dolphin");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.accessory = true;

        Item.width = 16;
        Item.height = 20;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(silver: 80);
    }

    public override void UpdateEquip(Player player) {
        if (player.TryGetModPlayer(out AccessoryPlayer accessoryPlayer))
            accessoryPlayer.FinoftheDolphin = true;

        if (player.wet)
            player.GetDamage(DamageClass.Generic) += 0.1f;

        player.accFlipper = true;
        player.ignoreWater = true;
    }
}
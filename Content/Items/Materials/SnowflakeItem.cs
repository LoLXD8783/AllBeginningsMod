using AllBeginningsMod.Common.Bases.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria;

namespace AllBeginningsMod.Content.Items.Materials;

public sealed class SnowflakeItem : ModItemBase
{
    public override void SetDefaults() {
        Item.maxStack = 999;
        
        Item.width = 42;
        Item.height = 42;

        Item.rare = ItemRarityID.White;
        Item.value = Item.sellPrice(copper: 5);
    }
}
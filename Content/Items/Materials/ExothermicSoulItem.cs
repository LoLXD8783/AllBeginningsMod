using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Materials; 
internal class ExothermicSoulItem : ModItem {
    public override void SetDefaults() {
        Item.width = 26;
        Item.height = 28;
        Item.value = 10000;
        Item.maxStack = 999;
        Item.rare = ItemRarityID.Red;
    }
}
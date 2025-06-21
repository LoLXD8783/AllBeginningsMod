using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Placeable; 

internal class SandStoneStatueItem : ModItem {
    public override string Texture => Assets.Assets.Textures.Items.Placeable.KEY_SandStoneStatueItem;
    
    public override void SetDefaults() {
        Item.DefaultToPlaceableTile(ModContent.TileType<SandStoneStatueTile>());
        Item.placeStyle = Main.rand.Next(3);
        Item.width = 28;
        Item.height = 20;
        Item.value = 2000;
    }

    public override bool? UseItem(Player player) {
        Item.placeStyle = Main.rand.Next(3);
        return base.UseItem(player);
    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Materials
{
    public sealed class DeathBlossomItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death Blossom");
            Tooltip.SetDefault("Contains the essence of the ones who survived death");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;

            Item.width = 28;
            Item.height = 42;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 8);
        }
    }
}
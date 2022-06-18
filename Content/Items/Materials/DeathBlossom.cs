using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Materials
{
    public sealed class DeathBlossom : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death Essence");
            Tooltip.SetDefault("The essence of the ones who survived death");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;

            Item.width = 64;
            Item.height = 64;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(copper: 60);
        }
    }
}
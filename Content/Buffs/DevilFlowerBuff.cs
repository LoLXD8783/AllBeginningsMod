using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Buffs
{
    public sealed class DevilFlowerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Devil's Gift");
            Description.SetDefault("Increased damage and monster spawns. Enjoy the gift");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<Common.Globals.Players.AllBeginningsGlobalPlayer>().DevilGift = true;
        }
    }
}

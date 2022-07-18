using AllBeginningsMod.Common.Globals.Players;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Buffs
{
    public sealed class DevilFlowerBuff : ModBuff
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Devil's Gift");
            Description.SetDefault("Increased damage and monster spawns. Enjoy the gift");

            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            if (player.TryGetModPlayer(out ABBuffPlayer buffPlayer)) {
                buffPlayer.DevilGift = true;
            }
        }
    }
}
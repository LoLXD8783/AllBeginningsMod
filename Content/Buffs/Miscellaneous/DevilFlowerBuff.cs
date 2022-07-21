using AllBeginningsMod.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Buffs.Miscellaneous
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
            if (player.TryGetModPlayer(out BuffPlayer buffPlayer)) {
                buffPlayer.DevilGift = true;
            }

            player.GetDamage(DamageClass.Generic) += 0.1f;
        }
    }
}
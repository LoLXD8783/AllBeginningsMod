using AllBeginningsMod.Common.Bases.Buffs;
using AllBeginningsMod.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Buffs;

public sealed class DevilFlowerBuff : ModBuffBase
{
    public override void SetStaticDefaults() {
        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex) {
        if (player.TryGetModPlayer(out BuffPlayer buffPlayer))
            buffPlayer.DevilGift = true;

        player.GetDamage(DamageClass.Generic) += 0.1f;
    }
}
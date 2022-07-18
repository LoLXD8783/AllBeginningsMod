using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Players
{
    public sealed class BuffPlayer : ModPlayer
    {
        public bool DevilGift { get; set; }

        public override void ResetEffects() {
            DevilGift = false;
        }

        public override void PostUpdateEquips() {
            if (DevilGift) {
                Player.GetDamage(DamageClass.Generic) += 0.1f;
            }
        }
    }
}
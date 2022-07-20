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

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage) {
            if (DevilGift) {
                damage += 0.1f;
            }
        }
    }
}
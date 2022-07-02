using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Globals.Players
{
    public sealed class AllBeginningsGlobalPlayer : ModPlayer
    {
        public bool DevilGift;
        public override void ResetEffects()
        {
            DevilGift = false;
        }
        public override void PostUpdateEquips()
        {
            Player player = Main.LocalPlayer;
            if (DevilGift)
            {
                player.GetDamage(DamageClass.Generic) += 0.1f;
            }
        }
    }
}

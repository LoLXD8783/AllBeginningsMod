using AllBeginningsMod.Common.Loaders;
using AllBeginningsMod.Content.NPCs.Enemies.Bosses.Nightgaunt;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.SceneEffects
{
    [Autoload(Side = ModSide.Client)]
    internal class NightgauntSceneEffect : ModSceneEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

        [Effect("NightgauntFilter")]
        private static Effect effect;
        public override void SpecialVisuals(Player player, bool isActive) {
            effect.Parameters["smooth"].SetValue(0.4f);
            effect.Parameters["radius"].SetValue(0.85f);
            player.ManageSpecialBiomeVisuals("AllBeginningsMod:NightgauntFilter", isActive);
        }

        public override bool IsSceneEffectActive(Player player) {
            return Main.npc.Any(npc => npc.type == ModContent.NPCType<NightgauntNPC>() && npc.active && npc.life > 0);
        }
    }
}

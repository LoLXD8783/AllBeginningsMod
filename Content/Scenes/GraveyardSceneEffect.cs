using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Scenes;

public sealed class GraveyardSceneEffect : ModSceneEffect
{
    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

    public override void SpecialVisuals(Player player, bool isActive) {
        player.ManageSpecialBiomeVisuals(AllBeginningsMod.ModPrefix + "ScreenVignette", isActive);
    }

    public override bool IsSceneEffectActive(Player player) {
        return player.ZoneGraveyard;
    }
}
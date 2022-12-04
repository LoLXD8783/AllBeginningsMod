using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Graphics.Scenes;

[Autoload(Side = ModSide.Client)]
public sealed class GraveyardSceneEffect : ModSceneEffect
{
    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

    public override void SpecialVisuals(Player player, bool isActive) {
        if (isActive) {
            ScreenShaderSystem.Vignette.Parameters["uOpacity"].SetValue(1f);
            ScreenShaderSystem.Vignette.Parameters["strength"].SetValue(0.8f);
            ScreenShaderSystem.Vignette.Parameters["curvature"].SetValue(0.5f);
            ScreenShaderSystem.Vignette.Parameters["innerRadius"].SetValue(0.5f);
            ScreenShaderSystem.Vignette.Parameters["outerRadius"].SetValue(1.2f);
        }

        player.ManageSpecialBiomeVisuals(AllBeginningsMod.ModPrefix + "Vignette", isActive);
    }

    public override bool IsSceneEffectActive(Player player) {
        return player.ZoneGraveyard;
    }
}
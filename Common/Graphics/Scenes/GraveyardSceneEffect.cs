using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Graphics.Scenes;

[Autoload(Side = ModSide.Client)]
public sealed class GraveyardSceneEffect : ModSceneEffect
{
    public static float CurrentOpacity { get; private set; }

    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

    public override void SpecialVisuals(Player player, bool isActive) {
        CurrentOpacity = MathHelper.SmoothStep(CurrentOpacity, isActive ? 1f : 0f, 0.1f);

        if (isActive) {
            ScreenShaderSystem.Vignette.Parameters["uOpacity"].SetValue(CurrentOpacity);
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
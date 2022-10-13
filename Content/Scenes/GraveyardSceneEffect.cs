using AllBeginningsMod.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Scenes;

public sealed class GraveyardSceneEffect : ModSceneEffect
{
    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

    public override void SpecialVisuals(Player player, bool isActive) {
        if (isActive) {
            Filters.Scene[AllBeginningsMod.ModPrefix + "ScreenVignette"].GetShader().Shader.Parameters["uOpacity"].SetValue(1f);
            Filters.Scene[AllBeginningsMod.ModPrefix + "ScreenVignette"].GetShader().Shader.Parameters["strength"].SetValue(0.8f);
            Filters.Scene[AllBeginningsMod.ModPrefix + "ScreenVignette"].GetShader().Shader.Parameters["curvature"].SetValue(0.5f);
            Filters.Scene[AllBeginningsMod.ModPrefix + "ScreenVignette"].GetShader().Shader.Parameters["innerRadius"].SetValue(0.5f);
            Filters.Scene[AllBeginningsMod.ModPrefix + "ScreenVignette"].GetShader().Shader.Parameters["outerRadius"].SetValue(1.2f);
        }

        player.ManageSpecialBiomeVisuals(AllBeginningsMod.ModPrefix + "ScreenVignette", isActive);
    }
    
    public override bool IsSceneEffectActive(Player player) {
        return player.ZoneGraveyard;
    }
}
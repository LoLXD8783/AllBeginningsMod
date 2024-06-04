using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common
{
    [Autoload(Side = ModSide.Client)]
    internal class FilterSystem : ILoadable
    {
        public static void ApplyFilter(Filter filter, Action<Effect> effectAction = null) {
            filterApplyCache.Add(new ScreenShaderApplyData(filter, effectAction));
        }

        private readonly struct ScreenShaderApplyData
        {
            public readonly Filter Filter;
            public readonly Action<Effect> EffectAction;
            public ScreenShaderApplyData(Filter filter, Action<Effect> effectAction) {
                Filter = filter;
                EffectAction = effectAction;
            }
        }

        private static List<ScreenShaderApplyData> filterApplyCache;
        public void Load(Mod mod) {
            filterApplyCache = new();
            On_FilterManager.EndCapture += On_FilterManager_EndCapture;
        }

        public void Unload() {
            On_FilterManager.EndCapture -= On_FilterManager_EndCapture;
        }

        private void On_FilterManager_EndCapture(
            On_FilterManager.orig_EndCapture orig,
            Terraria.Graphics.Effects.FilterManager self,
            RenderTarget2D finalTexture,
            RenderTarget2D screenTarget1,
            RenderTarget2D screenTarget2,
            Color clearColor
        ) {
            RenderTarget2D drawToTarget = screenTarget2;
            RenderTarget2D textureTarget = screenTarget1;
            foreach (ScreenShaderApplyData shaderApplyData in filterApplyCache) {
                Filter filter = shaderApplyData.Filter ?? throw new ArgumentException("FilterManager: Filter was null.");
                shaderApplyData.EffectAction?.Invoke(filter.GetShader().Shader);

                Main.graphics.GraphicsDevice.SetRenderTarget(drawToTarget);
                Main.graphics.GraphicsDevice.Clear(clearColor);
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                filter.Apply();
                Main.spriteBatch.Draw(textureTarget, Vector2.Zero, Main.ColorOfTheSkies);
                Main.spriteBatch.End();

                (textureTarget, drawToTarget) = (drawToTarget, textureTarget);
            }

            filterApplyCache.Clear();

            orig(self, finalTexture, textureTarget, drawToTarget, clearColor);
        }
    }
}

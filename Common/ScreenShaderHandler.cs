using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common
{
    [Autoload(Side = ModSide.Client)]
    internal class ScreenShaderHandler : ILoadable
    {
        public static void ApplyShader(string filterName, Action<Effect> effectAction = null) {
            shaderApplyCache.Enqueue(new ScreenShaderApplyData(filterName, effectAction));
        }

        private readonly struct ScreenShaderApplyData
        {
            public readonly string FilterName;
            public readonly Action<Effect> EffectAction;
            public ScreenShaderApplyData(string screenShaderDataName, Action<Effect> effectAction) {
                FilterName = screenShaderDataName;
                EffectAction = effectAction;
            }
        }

        private static Queue<ScreenShaderApplyData> shaderApplyCache;
        public void Load(Mod mod) {
            shaderApplyCache = new();
            On_FilterManager.EndCapture += On_FilterManager_EndCapture;
        }

        public void Unload() {
            On_FilterManager.EndCapture -= On_FilterManager_EndCapture;
        }

        private void On_FilterManager_EndCapture(
            On_FilterManager.orig_EndCapture orig,
            FilterManager self,
            RenderTarget2D finalTexture, 
            RenderTarget2D screenTarget1,
            RenderTarget2D screenTarget2,
            Color clearColor
        ) {
            RenderTarget2D drawToTarget = screenTarget2;
            RenderTarget2D textureTarget = screenTarget1;
            while (shaderApplyCache.Count > 0) {
                ScreenShaderApplyData shaderApplyData = shaderApplyCache.Dequeue();
                Filter filter = Filters.Scene[shaderApplyData.FilterName];
                if (filter is null) {
                    Logging.PublicLogger.Error($"Filter not found: \"{shaderApplyData.FilterName}\"");
                    continue;
                }

                shaderApplyData.EffectAction?.Invoke(filter.GetShader().Shader);

                Main.graphics.GraphicsDevice.SetRenderTarget(drawToTarget);
                Main.graphics.GraphicsDevice.Clear(clearColor);
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                filter.Apply();
                Main.spriteBatch.Draw(textureTarget, Vector2.Zero, Main.ColorOfTheSkies);
                Main.spriteBatch.End();

                (textureTarget, drawToTarget) = (drawToTarget, textureTarget);
            }

            orig(self, finalTexture, textureTarget, drawToTarget, clearColor);
        }
    }
}

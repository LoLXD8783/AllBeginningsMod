using AllBeginningsMod.Common.Config;
using AllBeginningsMod.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.CustomEntities.PrimitiveTrails
{
    [Autoload(Side = ModSide.Client)]
    public sealed class PrimitiveTrailSystem : ModSystem
    {
        public static PrimitiveTrail[] PrimitiveTrails { get; private set; }
        
        public static RenderTarget2D RenderTarget { get; private set; }

        public static Queue<int> FreeIndices { get; private set; }

        private static SpriteBatch SpriteBatch => Main.spriteBatch;
        
        private static GraphicsDevice Device => SpriteBatch.GraphicsDevice;

        public override void OnModLoad()
        {
            int maxPrimitives = AllBeginningsClientConfig.Instance.MaxPrimitiveTrails;

            PrimitiveTrails = new PrimitiveTrail[maxPrimitives];
            FreeIndices = new Queue<int>(maxPrimitives);

            for (int i = 0; i < maxPrimitives; i++)
            {
                FreeIndices.Enqueue(i);
            }

            Main.QueueMainThreadAction(() =>
            {
                RenderTarget = new RenderTarget2D(Device, Main.screenWidth / 2, Main.screenHeight / 2);
            });

            Main.OnPreDraw += Main_OnPreDraw;
            On.Terraria.Main.DrawDust += Main_DrawDust;
        }

        public override void Unload()
        {
            Main.OnPreDraw -= Main_OnPreDraw;
            On.Terraria.Main.DrawDust -= Main_DrawDust;

            Main.QueueMainThreadAction(() =>
            {
                RenderTarget?.Dispose();
                RenderTarget = null;
            });

            PrimitiveTrails = null;
            FreeIndices = null;
        }

        public override void PostUpdateEverything()
        {
            for (int i = 0; i < PrimitiveTrails.Length; i++)
            {
                PrimitiveTrails[i]?.OnUpdate();
            }
        }

        public static PrimitiveTrail Spawn(PrimitiveTrail particle)
        {
            if (FreeIndices.TryDequeue(out int index))
            {
                PrimitiveTrails[index] = particle;
                PrimitiveTrails[index].WhoAmI = index;
            }

            return particle;
        }

        public static void Kill(PrimitiveTrail particle)
        {
            PrimitiveTrails[particle.WhoAmI] = null;

            FreeIndices.Enqueue(particle.WhoAmI);
        }

        private static void Main_OnPreDraw(GameTime gameTime)
        {
            RenderTargetBinding[] oldTargets = Main.graphics.GraphicsDevice.GetRenderTargets();

            Device.SetRenderTarget(RenderTarget);
            Device.Clear(Color.Transparent);

            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, default, default, default, Main.GameViewMatrix.TransformationMatrix); 

            for (int i = 0; i < PrimitiveTrails.Length; i++)
            {
                PrimitiveTrails[i]?.OnDraw();
            }

            SpriteBatch.End();

            Device.SetRenderTargets(oldTargets);
        }
        
        private static void Main_DrawDust(On.Terraria.Main.orig_DrawDust orig, Main self)
        {
            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, default, default, default, Main.GameViewMatrix.TransformationMatrix);
            SpriteBatch.Draw(RenderTarget, ScreenUtils.ScreenRectangle, Color.White);
            SpriteBatch.End();

            orig(self);
        }
    }
}
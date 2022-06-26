using AllBeginningsMod.Common.Config;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace AllBeginningsMod.Common.CustomEntities.Particles
{
    public sealed class ParticleSystem : CustomEntitySystem<Particle>
    {
        public override int MaxEntities => AllBeginningsClientConfig.Instance.MaxParticles;

        public override void OnModLoad()
        {
            base.OnModLoad();
            
            On.Terraria.Main.DrawDust += Main_DrawDust;
        }

        public override void Unload()
        {
            base.Unload();

            On.Terraria.Main.DrawDust -= Main_DrawDust;
        }

        public override void PostUpdateDusts()
        {
            for (int i = 0; i < Entities.Length; i++)
            {
                Entities[i]?.OnUpdate();
            }
        }

        private static void Main_DrawDust(On.Terraria.Main.orig_DrawDust orig, Main self)
        {
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, default, default, default, default, Main.GameViewMatrix.ZoomMatrix);
            for (int i = 0; i < Entities.Length; i++)
            {
                Entities[i]?.OnDraw();
            }
            Main.spriteBatch.End();

            orig(self);
        }
    }
}
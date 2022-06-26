using AllBeginningsMod.Common.Config;
using System.Collections.Generic;
using Terraria;

namespace AllBeginningsMod.Common.CustomEntities.PrimitiveTrails
{
    public sealed class PrimitiveTrailSystem : CustomEntitySystem<PrimitiveTrail>
    {
        public override int MaxEntities => AllBeginningsClientConfig.Instance.MaxPrimitiveTrails;

        public override void OnModLoad()
        {
            base.OnModLoad();

            On.Terraria.Main.DrawCachedProjs += Main_DrawCachedProjs;
        }

        public override void Unload()
        {
            base.Unload();
         
            On.Terraria.Main.DrawCachedProjs -= Main_DrawCachedProjs;
        }

        public override void PostUpdateEverything()
        {
            for (int i = 0; i < Entities.Length; i++)
            {
                Entities[i]?.OnUpdate();
            }
        }

        private static void Main_DrawCachedProjs(On.Terraria.Main.orig_DrawCachedProjs orig, Main self, List<int> projCache, bool startSpriteBatch)
        {
            for (int i = 0; i < Entities.Length; i++)
            {
                Entities[i]?.OnDraw();
            }

            orig(self, projCache, startSpriteBatch);
        }
    }
}
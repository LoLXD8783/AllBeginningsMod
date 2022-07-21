using System.Collections.Generic;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Physics
{
    public sealed class PhysicsSystem : ModSystem
    {
        public static List<PhysicsModule> Modules { get; private set; }

        public override void OnModLoad() {
            Modules = new List<PhysicsModule>();
        }

        public override void OnModUnload() {
            Modules?.Clear();
            Modules = null;
        }

        public override void PostUpdateEverything() {
            Modules.ForEach(x => x?.Update());
        }
    }
}
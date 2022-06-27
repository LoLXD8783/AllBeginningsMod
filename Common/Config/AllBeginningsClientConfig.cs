using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace AllBeginningsMod.Common.Config
{
    public sealed class AllBeginningsClientConfig : ModConfig
    {
        public static AllBeginningsClientConfig Instance => ModContent.GetInstance<AllBeginningsClientConfig>();

        public override ConfigScope Mode => ConfigScope.ClientSide;

        // TODO: Add attributes.

        [DefaultValue(500)]
        [ReloadRequired]
        public int MaxParticles { get; set; }

        [DefaultValue(500)]
        [ReloadRequired]
        public int MaxPrimitiveTrails { get; set; }
    }
}
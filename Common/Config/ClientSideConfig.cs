using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace AllBeginningsMod.Common.Config
{
    public sealed class ClientSideConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Visuals [i:237]")]

        [DefaultValue(1000)]
        [Range(0, 2000)]
        [Slider]
        [ReloadRequired]
        [Label("Max Primitives")]
        [Tooltip("Sets the max amount of primitive shapes that can be drawn by the mod.")]
        public int MaxPrimitives { get; set; }
    }
}
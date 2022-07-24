using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace AllBeginningsMod.Common.Config
{
    [BackgroundColor(86, 25, 20)]
    public sealed class ClientSideConfig : ModConfig
    {
        public static ClientSideConfig Instance => ModContent.GetInstance<ClientSideConfig>();

        public override ConfigScope Mode => ConfigScope.ClientSide;
        
        [Header("Visuals [i:237]")]

        [DefaultValue(-1)]
        [Range(-1, 4000)]
        [Slider]
        [SliderColor(255, 236, 134)]
        [BackgroundColor(211, 47, 74)]
        [Label("Max Particles")]
        [Tooltip("Sets the max amount of particles created by the mod. Set to -1 to have unlimited particles.")]
        public int MaxParticles { get; set; }
    }
}
using Terraria.ModLoader;

namespace AllBeginningsMod
{
    public sealed class AllBeginningsMod : Mod {
        public const string AssetsPath = $"{nameof(AllBeginningsMod)}/Assets/";

        public const string SoundsPath = AssetsPath + "Sounds/";
        public const string EffectsPath = AssetsPath + "Effects/";
        public const string TexturesPath = AssetsPath + "Textures/";

        public static AllBeginningsMod Instance => ModContent.GetInstance<AllBeginningsMod>();
    }
}
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Recipes
{
    public sealed class RecipeGroupSystem : ModSystem
    {
        public static RecipeGroup PlatinumBarGroup { get; private set; }

        public override void OnModUnload() {
            PlatinumBarGroup = null;
        }

        public override void AddRecipeGroups() {
            PlatinumBarGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")}", new int[] {
                ItemID.PlatinumBar,
                ItemID.GoldBar
            });

            RecipeGroup.RegisterGroup("AllBeginningsMod:PlatinumBar", PlatinumBarGroup);
        }
    }
}
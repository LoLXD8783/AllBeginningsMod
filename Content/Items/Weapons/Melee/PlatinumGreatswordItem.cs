using AllBeginningsMod.Content.Projectiles.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Melee;

public sealed class PlatinumGreatswordItem : ModItem
{
    public override void SetDefaults() {
        Item.noUseGraphic = true;
        Item.noMelee = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 35;
        Item.knockBack = 8f;

        Item.useTime = 70;
        Item.useAnimation = 70;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shoot = ModContent.ProjectileType<PlatinumGreatswordProjectile>();

        Item.UseSound = new SoundStyle($"{nameof(AllBeginningsMod)}/Assets/Sounds/Item/GreatswordSwing") {
            PitchVariance = 0.5f
        };
    }

    public override bool CanUseItem(Player player) {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }

    public override void AddRecipes() {
        Recipe recipe = CreateRecipe();
        recipe.AddRecipeGroup(RecipeGroupID.Wood, 4);
        recipe.AddIngredient(ItemID.PlatinumBar, 12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
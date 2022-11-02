using AllBeginningsMod.Content.Projectiles.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Melee;

public sealed class LeadGreatswordItem : ModItem
{
    public override void SetDefaults() {
        Item.noUseGraphic = true;
        Item.noMelee = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 29;
        Item.knockBack = 9f;

        Item.useTime = 73;
        Item.useAnimation = 73;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shoot = ModContent.ProjectileType<LeadGreatswordProjectile>();

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
        recipe.AddIngredient(ItemID.LeadBar, 12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
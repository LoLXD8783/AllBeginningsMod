﻿using AllBeginningsMod.Content.Projectiles.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Melee;

public sealed class EbonwoodGreatswordItem : ModItem
{
    public override void SetDefaults() {
        Item.noUseGraphic = true;
        Item.noMelee = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 22;
        Item.knockBack = 8f;

        Item.useTime = 70;
        Item.useAnimation = 70;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shoot = ModContent.ProjectileType<EbonwoodGreatswordProjectile>();

        Item.UseSound = new SoundStyle($"{nameof(AllBeginningsMod)}/Assets/Sounds/Item/GreatswordSwing") {
            PitchVariance = 0.5f
        };
    }

    public override bool CanUseItem(Player player) {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }

    public override void AddRecipes() {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Ebonwood, 25);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}
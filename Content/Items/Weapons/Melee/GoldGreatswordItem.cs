using AllBeginningsMod.Common.Bases.Items;
using AllBeginningsMod.Content.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Melee;

public sealed class GoldGreatswordItem : GreatswordItemBase<GoldGreatswordProjectile>
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.autoReuse = true;

        Item.width = 46;
        Item.height = 46;

        Item.useTime = 50;
        Item.useAnimation = 50;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.damage = 16;
        Item.knockBack = 8f;
        Item.DamageType = DamageClass.Melee;
    }

    public override void AddRecipes() {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GoldBar, 12);
        recipe.AddIngredient(ItemID.Wood, 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
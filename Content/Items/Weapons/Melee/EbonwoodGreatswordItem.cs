using AllBeginningsMod.Common.Bases.Items;
using AllBeginningsMod.Content.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace AllBeginningsMod.Content.Items.Weapons.Melee;

public sealed class EbonwoodGreatswordItem : GreatswordItemBase<EbonwoodGreatswordProjectile>
{
    public override void SetStaticDefaults() {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults() {
        base.SetDefaults();

        Item.autoReuse = true;

        Item.width = 46;
        Item.height = 46;

        Item.useTime = 60;
        Item.useAnimation = 60;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.damage = 28;
        Item.knockBack = 8f;
        Item.DamageType = DamageClass.Melee;
    }

    public override void AddRecipes() {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Ebonwood, 15);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}
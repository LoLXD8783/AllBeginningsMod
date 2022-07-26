using AllBeginningsMod.Common.Bases.Items;
using AllBeginningsMod.Content.Projectiles.Ranged;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Ranged;

public sealed class BorealTurnipItem : ModItemBase
{
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("Boreal Turnip");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.noMelee = true;
        Item.noUseGraphic = true;

        Item.DamageType = DamageClass.Ranged;
        Item.damage = 10;
        Item.knockBack = 3f;

        Item.width = 32;
        Item.height = 32;

        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shoot = ModContent.ProjectileType<BorealTurnipProjectile>();
        Item.shootSpeed = 10f;

        Item.rare = ItemRarityID.Blue;
    }
}
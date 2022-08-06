using AllBeginningsMod.Common.Bases.Items;
using AllBeginningsMod.Content.Projectiles.Melee;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Melee;

public sealed class WingedBoomerangItem : ModItemBase
{
    public override void SetStaticDefaults() {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.channel = true;

        Item.damage = 12;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.knockBack = 1f;

        Item.width = 18;
        Item.height = 34;

        Item.useTime = 40;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shoot = ModContent.ProjectileType<WingedBoomerangProjectile>();
        Item.shootSpeed = 12f;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(silver: 60);
    }
}
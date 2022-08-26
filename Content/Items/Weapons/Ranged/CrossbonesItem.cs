using AllBeginningsMod.Common.Bases.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Ranged;

public sealed class CrossbonesItem : ModItemBase
{
    public override void SetStaticDefaults() {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.noMelee = true;

        Item.DamageType = DamageClass.Ranged;
        Item.damage = 32;
        Item.knockBack = 6f;

        Item.width = 32;
        Item.height = 32;

        Item.useTime = 80;
        Item.useAnimation = 80;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Arrow;

        Item.rare = ItemRarityID.Blue;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
        Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI).penetrate = 5;
        return true;
    }
}
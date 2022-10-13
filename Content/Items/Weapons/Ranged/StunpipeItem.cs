using AllBeginningsMod.Common.Bases.Items;
using AllBeginningsMod.Content.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Ranged;

public sealed class StunpipeItem : ModItemBase
{
    public override void SetStaticDefaults() {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.noMelee = true;

        Item.DamageType = DamageClass.Ranged;
        Item.damage = 16;
        Item.knockBack = 2f;

        Item.width = 32;
        Item.height = 32;

        Item.useTime = 40;
        Item.useAnimation = 40;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 12f;
        Item.useAmmo = AmmoID.Dart;

        Item.rare = ItemRarityID.Blue;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<StunpipeProjectile>(), damage, knockback, player.whoAmI);
        return false;
    }
}
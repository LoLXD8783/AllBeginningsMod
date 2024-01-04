using AllBeginningsMod.Common.GlobalItems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Ranged
{
    internal class BIGGunItem : ModItem
    {
        public override void SetStaticDefaults() {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults() {
            Item.damage = 99;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 78;
            Item.height = 42;
            Item.useTime = Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<BIGGunHeldProjectile>();
            Item.channel = true;
            Item.knockBack = 5;
            Item.value = 10000;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override bool AltFunctionUse(Player player) {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            Projectile.NewProjectile(
                source,
                position,
                velocity,
                Item.shoot,
                damage,
                knockback,
                player.whoAmI,
                type
            );

            return false;
        }
    }
}

using AllBeginningsMod.Common.GlobalItems;
using AllBeginningsMod.Utilities.Extensions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items;

[ItemGlowmask]
internal sealed class PlasmicRepeaterItem : ModItem {
    public override void SetDefaults() {
        Item.damage = 99;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 0;
        Item.height = 0;
        Item.useTime = Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.shoot = ModContent.ProjectileType<PlasmicRepeaterHeldProjectile>();
        Item.knockBack = 5;
        Item.value = 10000;
        Item.autoReuse = true;
        Item.noUseGraphic = true;
        Item.noMelee = true;
    }

    public static Vector2 CalculateGunCenter(Player player, out Vector2 direction) {
        return (player.RotatedRelativePoint(player.MountedCenter) + new Vector2(-4 * player.direction, -2))
                .OffsetVerticallyTowardsPosition(Main.MouseWorld, 1f * player.direction, out direction);
    }

    public Vector2 GunCenter { get; private set; }
    public float GunRotation { get; private set; }
    public override void UpdateInventory(Player player) {
        if (Main.myPlayer == player.whoAmI) {
            GunCenter = CalculateGunCenter(player, out Vector2 direction);
            GunRotation = Utils.AngleLerp(GunRotation, direction.ToRotation(), 0.1f);
        }
    }
}

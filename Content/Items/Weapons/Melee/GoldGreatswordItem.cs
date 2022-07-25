using System;
using AllBeginningsMod.Common.Items.Melee;
using AllBeginningsMod.Common.Projectiles.Melee;
using AllBeginningsMod.Content.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Melee;

public class GoldGreatswordItem : GreatswordItemBase
{
    public override int HeldProjectileType => ModContent.ProjectileType<GoldGreatswordProjectile>();
    public override void SetStaticDefaults() => DisplayName.SetDefault("Gold Greatsword");
    public override void SetDefaults() {
        base.SetDefaults();
        
        Item.width = 46;
        Item.height = 46;

        Item.useTime = 75;
        Item.useAnimation = 75;
        Item.autoReuse = true;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.damage = 20;
        Item.DamageType = DamageClass.Melee;
    }
}
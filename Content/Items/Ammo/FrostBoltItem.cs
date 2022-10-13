using AllBeginningsMod.Common.Bases.Items;
using AllBeginningsMod.Content.Projectiles.Ranged;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Ammo;

public sealed class FrostBoltItem : ModItemBase
{
    public override void SetDefaults() {
        Item.consumable = true;
        
        Item.DamageType = DamageClass.Ranged;
        Item.knockBack = 3f;
        Item.damage = 10;

        Item.maxStack = 999;
        
        Item.width = 14;
        Item.height = 40;

        Item.shoot = ModContent.ProjectileType<FrostBoltProjectile>();
        Item.shootSpeed = 6f;
        Item.ammo = AmmoID.Arrow;

        Item.rare = ItemRarityID.White;
    }
}
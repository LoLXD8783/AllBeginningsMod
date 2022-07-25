using AllBeginningsMod.Common.Projectiles.Melee;
using AllBeginningsMod.Content.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Melee;

public class TungstenGreatswordItem : ModItem
{
    private Projectile heldProj;

    private bool holdingProjectile;

    public override void SetStaticDefaults() => DisplayName.SetDefault("Tungsten Greatsword");

    public override void SetDefaults() {
        Item.noMelee = true;
        Item.noUseGraphic = true;

        Item.width = 44;
        Item.height = 44;

        Item.useTime = 60;
        Item.useAnimation = 60;
        Item.autoReuse = true;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.damage = 20;
        Item.DamageType = DamageClass.Melee;
    }

    public override void HoldItem(Player player) {
        if (player.whoAmI != Main.myPlayer)
            return;

        if (heldProj == null || holdingProjectile == false) {
            heldProj = Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center, Vector2.Zero,
                ModContent.ProjectileType<TungstenGreatswordProjectile>(), Item.damage, 1f, player.whoAmI);
            holdingProjectile = true;
        }
    }

    public override void UpdateInventory(Player player) {
        if (player.whoAmI != Main.myPlayer)
            return;

        //Does not cover dropping
        //Make projectile variable in ModPlayer to hold the current greatsword
        if (player.HeldItem.type != Type && holdingProjectile) {
            heldProj.Kill();
            holdingProjectile = false;
        }
    }

    public override bool? UseItem(Player player) {
        if (player.whoAmI == Main.myPlayer)
            (heldProj.ModProjectile as BaseSwingableGreatswordProjectile).DoAttack();

        return false;
    }
}
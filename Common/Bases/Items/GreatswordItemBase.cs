using AllBeginningsMod.Common.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Items.Melee;

public abstract class GreatswordItemBase : ModItem
{
    protected Projectile HeldProjectile { get; private set; }
    public abstract int HeldProjectileType { get; }

    public override void SetDefaults() {
        //Kirtle: useTime and useAnimation must be manually tailored for each greatsword..
        //Reading TotalAnimationTime from the projectile doesn't seem to work
        Item.noMelee = true;
        Item.noUseGraphic = true;
    }

    public override void HoldItem(Player player) {
        if (player.whoAmI != Main.myPlayer)
            return;

        if (HeldProjectile == null || HeldProjectile?.active == false)
            HeldProjectile = NewGreatswordProjectile(player, HeldProjectileType, Item.damage, Item.knockBack, player.whoAmI, Type);
    }

    public override bool? UseItem(Player player) {
        if (player.whoAmI == Main.myPlayer)
            (HeldProjectile.ModProjectile as GreatswordProjectileBase).DoAttack();

        return false;
    }
    
    private static Projectile NewGreatswordProjectile(Player player, int type, int damage, float knockback, int owner,
        int itemTypeAssociated) {
        Projectile projectile = Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center, Vector2.Zero, type, damage, knockback, owner);
        (projectile.ModProjectile as GreatswordProjectileBase).ItemTypeAssociated = itemTypeAssociated;

        return projectile;
    }
}
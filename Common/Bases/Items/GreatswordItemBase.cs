using AllBeginningsMod.Common.Bases.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Items;

public abstract class GreatswordItemBase<T> : ModItemBase where T : GreatswordProjectileBase
{
    private T HeldProjectile { get; set; }
    
    public override void SetStaticDefaults() {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        //Kirtle: useTime and useAnimation must be manually tailored for each greatsword..
        //Reading TotalAnimationTime from the projectile doesn't seem to work
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.useTurn = false;
    }

    public override void HoldItem(Player player) {
        if (player.whoAmI != Main.myPlayer)
            return;

        if (HeldProjectile == null || HeldProjectile?.Projectile.active == false)
            HeldProjectile = NewGreatswordProjectile(player, ModContent.ProjectileType<T>(), Item.damage, Item.knockBack, player.whoAmI, Type);
    }

    public override bool? UseItem(Player player) {
        if (player.whoAmI == Main.myPlayer)
            HeldProjectile.TryAttacking();

        return false;
    }

    private static T NewGreatswordProjectile(Player player, int type, int damage, float knockback, int owner, int itemType) {
        Projectile projectile = Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center, Vector2.Zero, type, damage, knockback, owner);
        T greatswordProjectile = projectile.ModProjectile as T;
        greatswordProjectile?.SetAssociatedItemType(itemType);

        return greatswordProjectile;
    }
}
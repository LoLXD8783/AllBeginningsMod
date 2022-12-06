using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.GlobalItems;

public class CustomUseStyleItem : GlobalItem
{
    public override void SetDefaults(Item item) {
        if (item.ModItem is ICustomUseStyle) {
            item.shoot = ModContent.ProjectileType<CustomUseStyleProjectile>();
            item.noUseGraphic = true;
            item.noMelee = true;
        }
    }
}

public interface ICustomUseStyle
{
    public void Behaviour(Projectile projectile, Player player);
    public bool Colliding(Projectile projectile, Player player, Rectangle targetHitbox);
    public void Draw(Projectile projectile, Player player, Texture2D texture, Color lightColor);

    public void OnSpawn(Projectile projectile, Player player, IEntitySource source);
    public void SendExtraAI(Projectile projectile, Player player, BinaryWriter writer);
    public void RecieveExtraAI(Projectile projectile, Player player, BinaryReader reader);
}

public class CustomUseStyleProjectile : ModProjectile
{
    private ICustomUseStyle customUseStyle;
    private int usedItemType;
    public override string Texture => "Terraria/Images/Item_0";

    private Player Player => Main.player[Projectile.owner];

    public override void OnSpawn(IEntitySource source) {
        if (source is EntitySource_ItemUse_WithAmmo itemSource && itemSource.Item.ModItem is ICustomUseStyle useStyle) {
            useStyle.OnSpawn(Projectile, Player, source);

            customUseStyle = useStyle;
            usedItemType = itemSource.Item.type;
        }
        else {
            Projectile.Kill();
        }
    }

    public override void SetDefaults() {
        Projectile.friendly = true;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.ownerHitCheck = true;

        Projectile.penetrate = -1;
        Projectile.aiStyle = -1;
        AIType = -1;
    }

    public override void AI() {
        if (Player.ItemAnimationEndingOrEnded || Player.HeldItem.type != usedItemType) {
            Projectile.Kill();

            return;
        }

        Player.heldProj = Projectile.whoAmI;

        customUseStyle.Behaviour(Projectile, Player);
    }

    public override void SendExtraAI(BinaryWriter writer) {
        customUseStyle.SendExtraAI(Projectile, Player, writer);
    }

    public override void ReceiveExtraAI(BinaryReader reader) {
        customUseStyle.RecieveExtraAI(Projectile, Player, reader);
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
        return customUseStyle.Colliding(Projectile, Player, targetHitbox);
    }

    public override bool PreDraw(ref Color lightColor) {
        customUseStyle.Draw(Projectile, Player, TextureAssets.Item[usedItemType].Value, lightColor);

        return false;
    }
}
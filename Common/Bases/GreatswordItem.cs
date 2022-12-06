using System;
using System.IO;
using AllBeginningsMod.Common.GlobalItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases;

public abstract class GreatswordItem : ModItem, ICustomUseStyle
{
    public virtual float SwordLenght => 60;

    public void Behaviour(Projectile projectile, Player player) {
        if (Main.myPlayer == player.whoAmI) {
            SetArcRotation(projectile, player);
        }

        SetPositions(projectile, player);
        SetArmRotation(projectile, player);
    }

    public bool Colliding(Projectile projectile, Player player, Rectangle targetHitbox) {
        return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + projectile.rotation.ToRotationVector2() * SwordLenght);
    }

    public void Draw(Projectile projectile, Player player, Texture2D texture, Color lightColor) {
        Vector2 drawPosition = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
        Vector2 drawOrigin = new(0f, texture.Height);

        Main.EntitySpriteDraw(texture, drawPosition, null, lightColor, projectile.rotation + MathHelper.PiOver4, drawOrigin, projectile.scale, SpriteEffects.None, 0);
    }

    public void OnSpawn(Projectile projectile, Player player, IEntitySource source) { }

    public void SendExtraAI(Projectile projectile, Player player, BinaryWriter writer) { }

    public void RecieveExtraAI(Projectile projectile, Player player, BinaryReader reader) { }

    public override void SetDefaults() {
        Item.noUseGraphic = true;
        Item.noMelee = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 18;
        Item.knockBack = 7f;

        Item.useTime = Item.useAnimation = 75;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.autoReuse = true;

        Item.UseSound = new SoundStyle($"{nameof(AllBeginningsMod)}/Assets/Sounds/Item/GreatswordSwing") {
            PitchVariance = 0.5f
        };
    }

    private float Progress(Player player) {
        return (float)player.itemAnimation / player.itemAnimationMax;
    }

    protected void SetArcRotation(Projectile projectile, Player player) {
        projectile.rotation = player.Center.DirectionTo(Main.MouseWorld).ToRotation() +
            (MathF.Sin(MathF.Pow(Progress(player), 2) * MathHelper.Pi) * MathHelper.Pi - MathHelper.PiOver2) * player.direction;
        projectile.netUpdate = true;
    }

    protected void SetPositions(Projectile projectile, Player player) {
        projectile.Center = player.Center;
        projectile.velocity = Vector2.Zero;
    }

    protected void SetArmRotation(Projectile projectile, Player player) {
        player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Quarter, 0f);
        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, projectile.rotation - MathHelper.PiOver2);
    }
}
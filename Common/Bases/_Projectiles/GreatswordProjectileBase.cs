using System;
using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases;

public abstract class GreatswordProjectileBase : ModProjectile
{
    public Vector2 InitialDirection;
    public virtual float SwingStart => SwingRadians / 2f * SwingDirection;
    public virtual float SwingEnd => -SwingRadians / 2f * SwingDirection;

    public virtual float SwingRadians => MathHelper.Pi * 1.25f;

    public int SwingDirection => Math.Sign(InitialDirection.X);

    public Player Player => Main.player[Projectile.owner];

    public override void ModifyDamageHitbox(ref Rectangle hitbox) {
        float offset = MathHelper.PiOver4;

        hitbox.X += (int)(MathF.Cos(Projectile.rotation - offset) * Projectile.width / 2f);
        hitbox.Y += (int)(MathF.Sin(Projectile.rotation - offset) * Projectile.height / 2f);
    }

    public override void OnSpawn(IEntitySource source) {
        InitialDirection = Player.DirectionTo(Main.MouseWorld).SafeNormalize(Vector2.Zero);
        Projectile.rotation = InitialDirection.ToRotation();
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
        TryKillProjectile();

        Player.heldProj = Projectile.whoAmI;

        SetPositions();
        SetArmRotation();
        SetArcRotation();
    }

    public override bool PreDraw(ref Color lightColor) {
        Texture2D texture = TextureAssets.Projectile[Type].Value;

        Vector2 drawPosition = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
        Vector2 drawOrigin = new(0f, texture.Height);

        Main.EntitySpriteDraw(texture, drawPosition, null, lightColor, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);

        return false;
    }

    protected bool TryKillProjectile() {
        bool inUse = Player.itemTime > 0;

        if (!inUse) {
            ref float timer = ref Projectile.ai[0];

            if (timer > Player.itemTimeMax / 4f) {
                Projectile.Kill();
            }

            timer++;
        }

        return inUse;
    }

    protected void SetPositions() {
        Projectile.Center = Player.Center;
        Projectile.velocity = Vector2.Zero;
    }

    protected void SetArcRotation() {
        float arcRotation = MathHelper.Lerp(SwingStart, SwingEnd, GetProgress());
        float initialRotation = InitialDirection.ToRotation();

        Projectile.rotation = initialRotation + arcRotation + MathHelper.PiOver4;
    }

    protected void SetArmRotation() {
        float armRotation = Projectile.rotation - MathHelper.ToRadians(135f);

        Player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Quarter, 0f);
        Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, armRotation);
    }

    protected float GetProgress() {
        float itemTime = Player.itemTime / (float)Player.itemTimeMax;
        float realTime = 1f - itemTime * 2f;
        float progress = MathF.Abs(itemTime < 0.5f ? EaseUtils.QuadEaseIn(realTime) : EaseUtils.HexicEaseIn(realTime));

        return progress;
    }
}
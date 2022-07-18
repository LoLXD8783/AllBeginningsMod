using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AllBeginningsMod.Common.Bases.Projectiles
{
    public abstract class SpearProjectileBase : HeldProjectileBase
    {
        protected abstract float HoldoutRangeMin { get; }
        protected abstract float HoldoutRangeMax { get; }

        public override void SetDefaults() {
            ChangeOwnerItemData = false;

            Projectile.ownerHitCheck = true;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.hide = true;

            Projectile.aiStyle = ProjAIStyleID.Spear;
            Projectile.penetrate = -1;
            Projectile.scale = 1.3f;
        }

        public override void AI() {
            base.AI();

            int duration = Owner.itemAnimationMax;

            if (Projectile.timeLeft > duration) {
                Projectile.timeLeft = duration;
            }

            Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero); 

            float halfDuration = duration / 2f;
            float progress;

            if (Projectile.timeLeft < halfDuration) {
                progress = Projectile.timeLeft / halfDuration;
            }
            else {
                progress = (duration - Projectile.timeLeft) / halfDuration;
            }

            HoldoutOffset = Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);

            if (Projectile.spriteDirection == -1) {
                Projectile.rotation -= MathHelper.PiOver2;
            }
        }
    }
}
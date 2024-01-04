using AllBeginningsMod.Content.Items.Weapons.Ranged;
using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Projectiles
{
    internal abstract class HeldProjectile : ModProjectile
    {
        protected enum DespawnMode {
            Manual,
            Channel,
            Animation
        }

        protected void Despawn() {
            despawn = true;
        }

        protected bool ShouldDespawn(DespawnMode despawnMode) {
            return Player.noItems || Player.CCed || despawnMode switch {
                DespawnMode.Channel => !(Player.altFunctionUse == 2 ? Main.mouseRight && Main.myPlayer == Projectile.owner : Player.channel),
                DespawnMode.Animation => Player.ItemAnimationEndingOrEnded,
                _ => false
            };
        }

        private bool despawn;
        protected DespawnMode despawnMode = DespawnMode.Channel;
        protected Player Player { get; private set; }

        public virtual void SetDefaults_HeldProjectile() { }

        /// <summary>
        /// Sets the position of the projectile relative to player and sets Projectile.velocity to direction to the mouse.
        /// </summary>
        /// <param name="verticalOffset"></param>
        /// <returns></returns>
        protected void SetRelativePosition(float verticalOffset = 0f) {
            if (Main.myPlayer != Projectile.owner) {
                return;
            }

            Projectile.Center = Player.RotatedRelativePoint(Player.MountedCenter) + new Vector2(-4 * Player.direction, -2);
            if (verticalOffset != 0) {
                Projectile.Center = Projectile.Center.OffsetVerticallyTowardsPosition(Main.MouseWorld, verticalOffset, out Projectile.velocity);
            } else {
                Projectile.velocity = Projectile.Center.DirectionTo(Main.MouseWorld);
            }

            Projectile.netUpdate = true;
        }

        protected void DrawHeldProjectile(
            Texture2D texture,
            Vector2 position,
            Color color,
            float rotation,
            Vector2? origin = default,
            Rectangle? source = default,
            int spriteDirection = 1,
            float? scale = default
        ) {
            origin ??= texture.Size() * 0.5f;

            Main.spriteBatch.Draw(
                texture,
                position,
                source,
                color,
                rotation + (Player.direction == 1 ? 0 : MathHelper.Pi),
                Player.direction == 1 ? origin.Value : new Vector2(texture.Width - origin.Value.X, origin.Value.Y),
                scale ?? Projectile.scale,
                (Player.direction * spriteDirection) == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                0f
            );
        }

        public override sealed void SetDefaults() {
            Projectile.width = 0;
            Projectile.height = 0;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.friendly = false;
            Projectile.hostile = false;

            SetDefaults_HeldProjectile();
        }

        public override sealed bool ShouldUpdatePosition() {
            return false;
        }

        public override sealed bool PreAI() {
            if (Projectile.owner == -1) {
                Projectile.active = false;
                return false;
            }

            Player = Main.player[Projectile.owner];

            if (!despawn) {
                Projectile.timeLeft = (1 + Projectile.extraUpdates) * 2;
                Player.itemTime = 2;
                Player.itemAnimation = 2;
            }

            Player.heldProj = Projectile.whoAmI;
            if (ShouldDespawn(despawnMode)) {
                Despawn();
            }

            return true;
        }
    }
}

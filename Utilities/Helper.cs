using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;

namespace AllBeginningsMod.Utilities
{
    internal static class Helper
    {
        public const BindingFlags FlagsAll = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        public const BindingFlags FlagsPublicInstance = BindingFlags.Public | BindingFlags.Instance;
        public const BindingFlags FlagsPrivateInstance = BindingFlags.NonPublic | BindingFlags.Instance;

        public static Matrix WorldTransformationMatrix => Matrix.CreateTranslation(-Main.screenPosition.X, -Main.screenPosition.Y, 0f)
            * Main.GameViewMatrix.TransformationMatrix
            * Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, -1, 1);

        public static float Lerp3(float a, float b, float c, float progress, float upperBound = 0.5f) {
            if (progress < upperBound) {
                return MathHelper.Lerp(a, b, progress / upperBound);
            }

            return MathHelper.Lerp(b, c, (progress - upperBound) / (1f - upperBound));
        }

        public static void NewDustCircular(
            Vector2 center,
            float radius,
            Func<int, int> dustType,
            int count,
            (float min, float max) speed,
            float rotation = 0f,
            Action<Dust> action = null
        ) {
            Vector2[] positions = center.PositionsAround(count, radius, rotation);
            for (int i = 0; i < positions.Length; i++) {
                Vector2 velocity = center.DirectionTo(positions[i]) * Main.rand.NextFloat(speed.min, speed.max);
                Dust dust = Dust.NewDustDirect(positions[i], 0, 0, dustType.Invoke(i), velocity.X, velocity.Y);
                action?.Invoke(dust);
            }
        }

        public static Vector2 InitialVelocityRequiredToHitPosition(Vector2 initialPosition, Vector2 targetPosition, float gravity, float initialSpeed, bool secondAngle = false) {
            Vector2 localTargetPosition = targetPosition - initialPosition;
            localTargetPosition.X = MathF.Abs(localTargetPosition.X);
            float randomShit = MathF.Pow(initialSpeed, 4) - gravity * (gravity * MathF.Pow(localTargetPosition.X, 2) + 2f * localTargetPosition.Y * MathF.Pow(initialSpeed, 2));
            float angle = MathF.Atan(
                (MathF.Pow(initialSpeed, 2) + MathF.Sqrt(Math.Max(randomShit, 0f)) * (secondAngle ? -1 : 1))
                / (gravity * localTargetPosition.X)
            );

            Vector2 velocity = angle.ToRotationVector2() * initialSpeed;
            velocity.Y = -velocity.Y;
            velocity.X *= MathF.Sign(targetPosition.X - initialPosition.X);

            return velocity;
        }

        public static void ForEachPlayerInRange(Vector2 position, float range, Action<Player> action) {
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player player = Main.player[i];
                if (player is null || !player.active || !player.Hitbox.Intersects(position, range)) {
                    continue;
                }

                action(player);
            }
        }

        public static void ForEachNPCInRange(Vector2 position, float range, Action<NPC> action) {
            for (int i = 0; i < Main.maxNPCs; i++) {
                NPC npc = Main.npc[i];
                if (npc is null || !npc.active || !npc.Hitbox.Intersects(position, range)) {
                    continue;
                }

                action(npc);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="player"></param>
        /// <param name="predicate"></param>
        /// <returns>Distance to the closest player.</returns>
        public static float ClosestPlayer(Vector2 center, out Player player, Func<Player, bool> predicate = null) {
            (Player player, float distance)? closest = null;
            for (int i = 0; i <= Main.maxPlayers; i++) {
                Player checkPlayer = Main.player[i];
                if (checkPlayer is null || !checkPlayer.active) {
                    continue;
                }

                float distance = center.DistanceSQ(checkPlayer.Center);
                if ((closest is null || closest.Value.distance < distance) && (predicate is null || predicate.Invoke(checkPlayer))) {
                    closest = (checkPlayer, distance);
                }
            }


            if (closest is null) {
                player = null;
                return -1f;
            }

            player = closest.Value.player;
            return closest.Value.distance;
        }

        public static Projectile NewProjectileCheckCollision(
            IEntitySource spawnSource,
            Vector2 position,
            Vector2 offset,
            Vector2 velocity,
            int type,
            int damage,
            float knockback,
            int owner = -1,
            float ai0 = 0,
            float ai1 = 0,
            float ai2 = 0
        ) {
            if (Collision.CanHit(position, 0, 0, position + offset, 0, 0)) {
                position += offset;
            }

            return Projectile.NewProjectileDirect(
                spawnSource,
                position,
                velocity,
                type,
                damage,
                knockback,
                owner,
                ai0,
                ai1,
                ai2
            );
        }

        public static void DrawAnimatedTexture(Texture2D texture, int frameCount, int frameHeight, int ticksPerFrame, Vector2 position, Color drawColor, Vector2 origin, float scale, SpriteEffects effects) {

            int currentFrame = (int)(Main.GameUpdateCount / ticksPerFrame) % frameCount;

            Rectangle sourceRectangle = new(0, currentFrame * frameHeight, texture.Width, texture.Height);

            Main.spriteBatch.Draw(texture, position, sourceRectangle, drawColor, 0, origin, scale, effects, 0);
        }
    }
}

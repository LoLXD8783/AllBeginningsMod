using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace AllBeginningsMod.Utilities.Extensions
{
    internal static class EntityExtensions
    {
        public static bool SolidCollision(this Entity entity, Vector2 offset) {
            return Collision.SolidCollision(entity.position + offset, entity.width, entity.height);
        }

        public static bool SolidCollision(this Entity entity) {
            return SolidCollision(entity, Vector2.Zero);
        }

        public static void Move(this Entity entity, Vector2 position, float speed = 3f, float turnResistance = 0.5f) {
            Vector2 move = position - entity.Center;
            float magnitude = move.Length();
            if (magnitude > speed) {
                move *= speed / magnitude;
            }

            entity.velocity = (entity.velocity * turnResistance + move) / (turnResistance + 1f);
        }
    }
}

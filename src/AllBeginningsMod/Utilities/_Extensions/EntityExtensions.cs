using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Utilities;

public static class EntityExtensions {
    public static bool SolidCollision(this Entity entity, Vector2 offset) {
        return Collision.SolidCollision(entity.position + offset, entity.width, entity.height);
    }

    public static bool SolidCollision(this Entity entity) {
        return SolidCollision(entity, Vector2.Zero);
    }

    public static void MoveInDirection(this Entity entity, Vector2 direction, float acceleration = 1f, float turn = 0.5f) {
        entity.velocity = entity.velocity.RotatedBy(-Vector3.Cross(direction.ToVector3(), entity.velocity.ToVector3()).Z * turn) * acceleration;
    }
}
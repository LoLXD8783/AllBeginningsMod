using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Utilities.Extensions;

public static class EntityExtensions
{
    public static bool InRange(this Entity entity, Vector2 center, float range) {
        return Collision.CheckAABBvLineCollision(entity.TopLeft, entity.Size, center, center + center.DirectionTo(entity.Center) * range);
    }
}
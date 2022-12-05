using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Utilities.Extensions
{
    public static class EntityExtensions
    {
        public static bool InRange(this Entity entity, Vector2 center, float range) => Collision.CheckAABBvLineCollision(entity.TopLeft, entity.Size, center, center + center.DirectionTo(entity.Center) * range);
    }
}

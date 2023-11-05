using System;
using AllBeginningsMod.Utilities.Extensions;
using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Utilities;

public static class DustUtils
{
    public static void NewDustCircular(Vector2 center,
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
}
using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Utility;

public static class DustUtils
{
    public static void SpawnCircle(Vector2 position, int dustType, int dustCount, float radius, bool fadeOut = true) {
        for (int i = 0; i < dustCount; i++) {
            float rotation = MathHelper.TwoPi * i / dustCount;
            Vector2 velocity = rotation.ToRotationVector2() * radius;

            Dust dust = Dust.NewDustPerfect(
                fadeOut
                    ? position
                    : position + velocity,
                dustType,
                fadeOut
                    ? velocity
                    : null
            );
            dust.noGravity = true;
        }
    }
}
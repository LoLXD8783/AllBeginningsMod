using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Utilities;

public static class DustUtils
{
    public static Vector2[] GenerateCircularPositions(this Vector2 center, float radius, int amount = 8, float rotation = 0) {
        if (amount <= 0) {
            return Array.Empty<Vector2>();
        }

        Vector2[] postitions = new Vector2[amount];

        float angle = MathHelper.Pi * 2f / amount;
        angle += rotation;

        for (int i = 0; i < amount; i++) {
            Vector2 position = (angle * i).ToRotationVector2();
            position *= radius;
            position += center;

            postitions[i] = position;
        }

        return postitions;
    }

    public static void NewDustCircular(Vector2 center,
        float radius,
        Func<int, int> dustTypeFunc,
        int amount = 8,
        float rotation = 0,
        (float, float)? minMaxSpeedFromCenter = null,
        Action<Dust> dustAction = null) {
        Vector2[] positions = center.GenerateCircularPositions(radius, amount, rotation);

        for (int i = 0; i < positions.Length; i++) {
            Vector2 pos = positions[i];
            Vector2 velocity = minMaxSpeedFromCenter is not null ? center.DirectionTo(pos) * Main.rand.NextFloat(minMaxSpeedFromCenter.Value.Item1, minMaxSpeedFromCenter.Value.Item2) : Vector2.Zero;

            Dust dust = Dust.NewDustDirect(pos, 0, 0, dustTypeFunc.Invoke(i), velocity.X, velocity.Y);

            dustAction?.Invoke(dust);
        }
    }
}
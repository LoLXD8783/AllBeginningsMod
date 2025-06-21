﻿using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace AllBeginningsMod.Utilities;

public static class VectorExtensions {
    public static Vector3 ToVector3(this Vector2 vector, float z = 0f) {
        return new Vector3(vector.X, vector.Y, z);
    }
    
    public static Point ToPoint(this Vector2 vector, float z = 0f) {
        return new Point((int)vector.X, (int)vector.Y);
    }
    public static Vector2[] PositionsAround(this Vector2 vector, int count, Func<float, float> radius, out Vector2[] directions, float initialRotation = 0f) {
        Vector2[] positions = new Vector2[count];
        directions = new Vector2[count];
        for(int i = 0; i < positions.Length; i++) {
            float factor = (float)i / positions.Length;
            float rotation = initialRotation + 6.28318548f * factor;
            directions[i] = new Vector2(MathF.Cos(rotation), MathF.Sin(rotation));
            positions[i] = vector + directions[i] * radius(factor);
        }

        return positions;
    }

    public static Vector2[] PositionsAround(this Vector2 vector, int count, Func<float, float> radius, float initialRotation = 0f) {
        return vector.PositionsAround(count, radius, out Vector2[] _, initialRotation);
    }

    public static Vector2[] PositionsAround(this Vector2 vector, int count, float radius, float initialRotation = 0f) {
        return vector.PositionsAround(count, _ => radius, out Vector2[] _, initialRotation);
    }

    public static Vector2[] PositionsAround(this Vector2 vector, int count, float radius, out Vector2[] directions, float initialRotation = 0f) {
        return vector.PositionsAround(count, _ => radius, out directions, initialRotation);
    }

    public static Vector2 OffsetVerticallyTowardsPosition(this Vector2 vector, Vector2 position, float offset, out Vector2 direction) {
        Vector2 displacement = position - vector;
        float length = displacement.Length();
        if(length == 0f) {
            direction = Vector2.Zero;
            return vector;
        }

        Vector2 preRotationDirection = displacement / length;
        direction = preRotationDirection.RotatedBy(-MathF.Atan(offset / length));
        return vector + preRotationDirection.RotatedBy(MathHelper.PiOver2) * offset;
    }
}
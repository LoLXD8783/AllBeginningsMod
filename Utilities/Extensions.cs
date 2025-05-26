using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;

namespace AllBeginningsMod.Utilities; 
internal static class Extensions {
    public static Vector3 ToVector3(this Vector2 vector, float z = 0f) {
        return new Vector3(vector.X, vector.Y, z);
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

    public static bool SolidCollision(this Entity entity, Vector2 offset) {
        return Collision.SolidCollision(entity.position + offset, entity.width, entity.height);
    }

    public static bool SolidCollision(this Entity entity) {
        return SolidCollision(entity, Vector2.Zero);
    }

    public static void MoveInDirection(this Entity entity, Vector2 direction, float acceleration = 1f, float turn = 0.5f) {
        entity.velocity = entity.velocity.RotatedBy(-Vector3.Cross(direction.ToVector3(), entity.velocity.ToVector3()).Z * turn) * acceleration;
    }

    public static void Begin(this SpriteBatch spriteBatch, SpriteBatchData data) {
        spriteBatch.Begin(
            data.SortMode,
            data.BlendState,
            data.SamplerState,
            data.DepthStencilState,
            data.RasterizerState,
            data.Effect,
            data.TransformMatrix
        );
    }

    public static void EndBegin(this SpriteBatch spriteBatch, SpriteBatchData data) {
        spriteBatch.End();
        spriteBatch.Begin(data);
    }

    public static SpriteBatchData CaptureEndBegin(this SpriteBatch spriteBatch, SpriteBatchData data) {
        SpriteBatchData captureData = spriteBatch.Capture();
        spriteBatch.EndBegin(data);

        return captureData;
    }

    public static void InsertDraw(this SpriteBatch spriteBatch, SpriteBatchData data, Action<SpriteBatch> drawAction) {/*
        if ((bool)SpriteBatchCache.BeginCalled.GetValue(spriteBatch)) {
            SpriteBatchData rebeginData = spriteBatch.CaptureEndBegin(data);
            drawAction(spriteBatch);
            spriteBatch.EndBegin(rebeginData);
        }
        else {
            
        }*/

        SpriteBatchData initData = spriteBatch.CaptureEndBegin(data);
        drawAction(spriteBatch);
        spriteBatch.EndBegin(initData);
    }

    public static SpriteBatchData Capture(this SpriteBatch spriteBatch) {
        return SpriteBatchData.Capture(spriteBatch);
    }

    public static void DrawAdditive(this SpriteBatch spriteBatch,
        Texture2D texture,
        Vector2 position,
        Rectangle? source,
        Color color,
        float rotation,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects) {
        SpriteBatchData data = spriteBatch.Capture();
        spriteBatch.End();
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

        spriteBatch.Draw(
            texture,
            position,
            source,
            color,
            rotation,
            origin,
            scale,
            effects,
            0
        );

        spriteBatch.End();
        spriteBatch.Begin(data);
    }

    public static void End(this SpriteBatch spriteBatch, out SpriteBatchData snapshot) {
        snapshot = spriteBatch.Capture();
        spriteBatch.End();
    }

    public static bool Intersects(this Rectangle rectangle, Vector2 center, float radius) {
        Vector2 hitboxCenter = rectangle.Center.ToVector2();
        float distanceX = MathF.Abs(center.X - hitboxCenter.X);
        float distanceY = MathF.Abs(center.Y - hitboxCenter.Y);
        float halfWidth = rectangle.Width / 2f;
        float halfHeight = rectangle.Height / 2f;

        if(distanceX > halfWidth + radius || distanceY > halfHeight + radius) {
            return false;
        }

        if(distanceX < halfWidth || distanceY < halfHeight) {
            return true;
        }

        float distanceSquared = MathF.Pow(distanceX - halfWidth, 2) + MathF.Pow(distanceY - halfHeight, 2);
        return distanceSquared < MathF.Pow(radius, 2);
    }

    public static bool Intersects(this Rectangle rectangle, Point center, float radius) {
        return rectangle.Intersects(center.ToVector2(), radius);
    }

    public static Rectangle SourceRectangle(this Projectile projectile) {
        Texture2D texture = TextureAssets.Projectile[projectile.type].Value;
        int sourceHeight = texture.Height / Main.projFrames[projectile.type];
        return new Rectangle(0, projectile.frame * sourceHeight, texture.Width, sourceHeight);
    }

    public static bool CanBeDamagedByPlayer(this NPC npc, Player player) {
        return !npc.friendly
            && !npc.dontTakeDamage
            && npc.active
            && npc.immune[player.whoAmI] <= 0;
    }

    public static Vector2 GetCenter(this Texture2D tex)
        => new Vector2(tex.Width / 2, tex.Height / 2);
}
using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Graphics;

public sealed class PrimitiveDrawing : ModSystem
{
    private static readonly PrimitiveBuffer primitiveBuffer = new();

    public static void DrawPrimitiveShape(IPrimitiveShape shape, PrimitiveType type, Effect effect) {
        int primitiveCount = primitiveBuffer.VertexBuffer.GetPrimitiveCount(type);

        foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
            pass.Apply();
            Main.graphics.GraphicsDevice.DrawIndexedPrimitives(type, 0, 0, primitiveBuffer.VertexBuffer.VertexCount, 0, primitiveCount);
        }
    }
}
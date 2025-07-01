using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Content.Bosses.Nightgaunt;

public enum BoneOrientation {
    Horizontal,
    Vertical
}

//todo: should be mutable, or have functions to change the source rect and offset

public readonly struct BoneTexture {
    public readonly Texture2D Texture;
    public readonly Rectangle? SourceRectangle;
    public readonly Vector2 DrawingOffset;
    public readonly BoneOrientation Orientation;

    public BoneTexture(Texture2D texture, Rectangle? sourceRectangle, BoneOrientation orientation, Vector2 drawingOffset = default) {
        Texture = texture;
        SourceRectangle = sourceRectangle;
        Orientation = orientation;
        DrawingOffset = drawingOffset;
    }
}
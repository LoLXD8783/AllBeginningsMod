using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Content.Bosses.Nightgaunt;

public class NightgauntBone {
    public float RelativeAngle;
    public float WorldAngle;
    public Vector2 StartPosition;
    public Vector2 EndPosition;

    public float Length { get; }
    public NightgauntBone Parent { get; }
    public IConstraint Constraint { get; }
    
    public BoneTexture BoneTexture { get; set; }
    
    public NightgauntBone(
        float length,
        NightgauntBone parent = null,
        BoneTexture texture = default,
        IConstraint constraint = null
    ) {
        Length = length;
        Parent = parent;
        Constraint = constraint;
        BoneTexture = texture;
    }
}
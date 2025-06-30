namespace AllBeginningsMod.Content.Bosses.Nightgaunt;

/// <summary> Bone modifier that constrains a bones angle (eg for knees, elbows).</summary>
public interface IConstraint {
    float Apply(float relativeAngle);
}
using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Common.Graphics.Particles;

public abstract class ParallaxParticle : Particle
{
    /// <summary>
    /// Represents the position where this particle was spawned.
    /// </summary>
    public Vector2 SpawnPosition;
    
    /// <summary>
    /// Represents the parallax offset this particle was offset by when spawned.
    /// </summary>
    public Vector2 ParallaxOffset;

    /// <summary>
    /// Represents the intensity of the parallax effect on this particle.
    /// </summary>
    public float Parallax = 0.1f;

    public override void OnSpawn() {
        SpawnPosition = Position;
        ParallaxOffset = 2f * (SpawnPosition - Main.screenPosition) * Parallax;

        Position -= ParallaxOffset;
    }

    public override void Draw() {
        Vector2 parallax = Vector2.Lerp(Main.screenPosition, Main.screenPosition - 2f * (SpawnPosition - Main.screenPosition), Parallax);

        Main.EntitySpriteDraw(Texture, Position - parallax, Frame, Color * Alpha, Rotation, Origin, Scale, Effects, 0);
    }
}
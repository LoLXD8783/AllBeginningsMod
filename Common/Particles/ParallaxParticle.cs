using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;

namespace AllBeginningsMod.Common.Systems.Particles;

public abstract class ParallaxParticle : Particle
{
    public Vector2 SpawnPosition;
    public Vector2 ParallaxOffset;

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
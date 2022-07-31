using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;

namespace AllBeginningsMod.Common.Systems.Particles;

public abstract class ParallaxParticle : Particle
{
    public Vector2 SpawnPosition;

    public float Parallax = 0.1f;

    public override void OnSpawn() {
        base.OnSpawn();

        SpawnPosition = Position;
    }

    public override void Draw() {
        Texture2D texture = ModContent.Request<Texture2D>(TexturePath).Value;
        Vector2 position = Position - Vector2.Lerp(Main.screenPosition, Main.screenPosition - 2f * (SpawnPosition - Main.screenPosition), Parallax);
        
        Main.EntitySpriteDraw(texture, position, Frame, Color, Rotation, Origin, Scale, Effects, 0);
    }
}
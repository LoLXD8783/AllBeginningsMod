using AllBeginningsMod.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.Particles;

public abstract class Particle
{
    public float Alpha = 1f;

    public Color Color = Color.White;

    public SpriteEffects Effects;

    public Rectangle? Frame;
    public Vector2 Origin;

    public Vector2 Position;
    public float Rotation;
    public float Scale = 1f;
    public Vector2 Velocity;
    public virtual string TexturePath => GetType().FullName.Replace('.', '/');

    public virtual void Update() => Position += Velocity;

    public virtual void Draw() {
        if (!DrawUtils.WorldOnScreen(Position))
            return;

        Texture2D texture = ModContent.Request<Texture2D>(TexturePath).Value;
        Main.EntitySpriteDraw(texture, Position - Main.screenPosition, Frame, Color * Alpha, Rotation, Origin, Scale, Effects, 0);
    }

    public virtual void OnKill() { }

    public virtual void OnSpawn() { }
}
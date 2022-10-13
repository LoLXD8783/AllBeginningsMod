using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Graphics.Particles;

public abstract class Particle
{
    /// <summary>
    /// Represents the file name of this particle's texture file directly from the mod's assets directory.
    /// </summary>
    public virtual string TexturePath => GetType().FullName.Replace('.', '/').Replace("Content", "Assets");

    /// <summary>
    /// Shorthand for getting this particle's texture.
    /// </summary>
    public Texture2D Texture => ModContent.Request<Texture2D>(TexturePath).Value;

    /// <summary>
    /// Represents the <see cref="Color"/> of this particle.
    /// </summary>
    public Color Color = Color.White;

    /// <summary>
    /// Represents the sprite effects of this particle.
    /// </summary>
    public SpriteEffects Effects;

    /// <summary>
    /// Represents the source rectangle of this particle.
    /// </summary>
    public Rectangle? Frame;

    /// <summary>
    /// Represents the world position of this particle.
    /// </summary>
    public Vector2 Position;
    
    /// <summary>
    /// Represents the velocity of this particle.
    /// </summary>
    public Vector2 Velocity;
    
    /// <summary>
    /// Represents the rotation origin of this particle.
    /// </summary>
    public Vector2 Origin;
    
    /// <summary>
    /// Represents the scale of this particle.
    /// </summary>
    public Vector2 Scale = Vector2.One;

    /// <summary>
    /// Represents the opacity of this particle.
    /// </summary>
    public float Alpha = 1f;
    
    /// <summary>
    /// Represents the rotation of this particle.
    /// </summary>
    public float Rotation;

    /// <summary>
    /// Represents the width of this particle. Used for off-screen drawing.
    /// </summary>
    public int Width = 16;
    
    /// <summary>
    /// Represents the height of this particle. Used for off-screen drawing.
    /// </summary>
    public int Height = 16;

    /// <summary>
    /// Represents whether this particle uses additive blending mode or not.
    /// </summary>
    public bool IsAdditive;

    /// <summary>
    /// Called when this particle is spawned.
    /// </summary>
    public virtual void OnSpawn() { }

    /// <summary>
    /// Called when this particle is killed.
    /// </summary>
    public virtual void OnKill() { }

    /// <summary>
    /// Called when this particle is updated. Make sure to call the base method for position calculations.
    /// </summary>
    public virtual void Update() {
        Position += Velocity;
    }

    /// <summary>
    /// Called when this particle is drawn. Make sure to call the base method if you want the particle to be drawn.
    /// </summary>
    public virtual void Draw() {
        Main.EntitySpriteDraw(Texture, Position - Main.screenPosition, Frame, Color * Alpha, Rotation, Origin, Scale, Effects, 0);
    }

    /// <summary>
    /// Attempts to kill this particle.
    /// </summary>
    /// <returns>Whether the particle has been successfully killed or not.</returns>
    public bool Kill() {
        return ParticleManager.Kill(this);
    }

    /// <inheritdoc cref="ParticleManager.Spawn"/>
    public static T Spawn<T>(Vector2 position, Vector2 velocity = default, Color? color = null, Vector2? scale = null, float rotation = 0f, float alpha = 1f) where T : Particle, new() {
        return ParticleManager.Spawn<T>(position, velocity, color, scale, rotation, alpha);
    }
}
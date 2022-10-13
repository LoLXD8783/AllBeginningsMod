using System.Collections.Generic;
using AllBeginningsMod.Common.Configuration;
using AllBeginningsMod.Utility.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Graphics.Particles;

[Autoload(Side = ModSide.Client)]
public sealed class ParticleManager : ILoadable
{
    /// <summary>
    /// Represents the max allowed amount of particles. Set by the client through a config setting.
    /// </summary>
    public static int MaxParticles => ClientSideConfiguration.Instance.MaxParticles;

    /// <summary>
    /// Represents the list of currently active particles.
    /// </summary>
    public static List<Particle> Particles { get; private set; }

    void ILoadable.Load(Mod mod) {
        Particles = new List<Particle>(MaxParticles);
        
        On.Terraria.Dust.UpdateDust += UpdateParticles;
        On.Terraria.Main.DrawDust += DrawParticles;
    }

    void ILoadable.Unload() {
        On.Terraria.Dust.UpdateDust -= UpdateParticles;
        On.Terraria.Main.DrawDust -= DrawParticles;

        Particles?.Clear();
        Particles = null;
    }

    /// <summary>
    /// Attempts to spawn a particle.
    /// </summary>
    /// <param name="position">The position of the particle.</param>
    /// <param name="velocity">The velocity of the particle.</param>
    /// <param name="color">The color of the particle.</param>
    /// <param name="scale">The scale of the particle.</param>
    /// <param name="rotation">The rotation of the particle.</param>
    /// <param name="alpha">The opacity of the particle.</param>
    /// <typeparam name="T">The type of the particle.</typeparam>
    /// <returns>Whether the particle has been successfully spawned or not.</returns>
    public static T Spawn<T>(Vector2 position, Vector2 velocity = default, Color? color = null, Vector2? scale = null, float rotation = 0f, float alpha = 1f) where T : Particle, new() {
        T particle = new();

        particle.Position = position;
        particle.Velocity = velocity;
        particle.Color = color ?? Color.White;
        particle.Scale = scale ?? Vector2.One;
        particle.Rotation = rotation;
        particle.Alpha = alpha;

        particle.OnSpawn();

        Particles.Add(particle);

        return particle;
    }

    /// <summary>
    /// Attempts to kill a particle.
    /// </summary>
    /// <param name="particle">The particle.</param>
    /// <returns>Whether the particle has been successfully killed or not.</returns>
    public static bool Kill<T>(T particle) where T : Particle {
        bool success = Particles.Remove(particle);

        if (success)
            particle.OnKill();

        return success;
    }

    private static void UpdateParticles(On.Terraria.Dust.orig_UpdateDust orig) {
        orig();

        if (Particles.Count >= MaxParticles)
            Particles.RemoveAt(0);

        for (int i = 0; i < Particles.Count; i++)
            Particles[i]?.Update();
    }

    private static void DrawParticles(On.Terraria.Main.orig_DrawDust orig, Main self) {
        orig(self);

        Main.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.NonPremultiplied, SamplerState.PointClamp, default, Main.Rasterizer, default, Main.GameViewMatrix.TransformationMatrix);

        for (int i = 0; i < Particles.Count; i++) {
            Particle particle = Particles[i];

            if (particle == null || particle.IsAdditive || !particle.Position.IsWorldOnScreen(particle.Width, particle.Height))
                continue;

            particle.Draw();
        }

        Main.spriteBatch.End();

        Main.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.NonPremultiplied, SamplerState.PointClamp, default, Main.Rasterizer, default, Main.GameViewMatrix.TransformationMatrix);

        for (int i = 0; i < Particles.Count; i++) {
            Particle particle = Particles[i];

            if (particle == null || !particle.IsAdditive || !particle.Position.IsWorldOnScreen(particle.Width, particle.Height))
                continue;

            particle.Draw();
        }

        Main.spriteBatch.End();
    }
}
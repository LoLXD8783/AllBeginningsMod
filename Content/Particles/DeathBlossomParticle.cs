using System;
using AllBeginningsMod.Common.Graphics.Particles;
using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Content.Particles;

public sealed class DeathBlossomParticle : Particle
{
    private float progress;

    private bool fadeOut;

    public override void OnSpawn() {
        IsAdditive = true;

        Rotation = Main.rand.NextFloat(MathHelper.TwoPi);
        Alpha = 0f;

        Frame = new Rectangle(0, Main.rand.Next(3) * 10, 10, 10);
        Scale = new Vector2(Main.rand.NextFloat(0.5f, 1f));
        Origin = Texture.Size() / 2f;
    }

    public override void Update() {
        base.Update();

        progress++;

        Velocity.Y -= 0.001f;
        Velocity.X += MathF.Cos(progress * 0.05f) * 0.05f;

        if (!fadeOut) {
            Alpha += 0.005f;

            if (Alpha >= 0.75f)
                fadeOut = true;
        }
        else {
            Alpha -= 0.005f;

            if (Alpha <= 0f)
                Kill();
        }
    }
}
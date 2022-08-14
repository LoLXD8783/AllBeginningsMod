using AllBeginningsMod.Common.Graphics.Particles;
using AllBeginningsMod.Utility.Extensions;
using Microsoft.Xna.Framework;
using Terraria;

namespace AllBeginningsMod.Content.Particles;

public sealed class ExplosionParticle : Particle
{
    private bool fadeOut;

    public override void OnSpawn() {
        Frame = new Rectangle(0, Main.rand.Next(3) * 36, 36, 36);
        Origin = Frame.Value.GetCenterOrigin();

        Rotation = Main.rand.NextFloat(MathHelper.TwoPi);
        Alpha = 0.25f;
        Scale *= Main.rand.NextFloat(0.6f, 0.9f);
    }

    public override void Update() {
        Velocity.X *= 0.92f;
        Velocity.Y -= 0.25f + Velocity.Y * 0.25f;

        Scale.X += 0.025f;
        Scale.Y += 0.025f;

        if (!fadeOut) {
            Alpha += 0.025f;

            if (Alpha >= 0.9f)
                fadeOut = true;
        }
        else {
            Alpha -= 0.025f;

            if (Alpha <= 0f)
                Kill();
        }

        base.Update();
    }
}
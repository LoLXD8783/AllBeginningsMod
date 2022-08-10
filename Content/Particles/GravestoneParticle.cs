using AllBeginningsMod.Common.Graphics.Particles;
using AllBeginningsMod.Utility.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Particles;

public sealed class GraveyardScreenParticle : ParallaxParticle
{
    private bool fadeOut;

    public override void OnSpawn() {
        base.OnSpawn();

        Origin = Texture.GetCenterOrigin();

        IsAdditive = true;

        Alpha = 0f;
    }

    public override void Update() {
        base.Update();

        Velocity.Y -= 0.005f - Velocity.Y * 0.005f;

        if (!fadeOut) {
            Alpha += 0.01f;

            if (Alpha >= 0.75f)
                fadeOut = true;
        }
        else {
            Alpha -= 0.0025f;

            if (Alpha <= 0f)
                Kill();
        }
    }
}
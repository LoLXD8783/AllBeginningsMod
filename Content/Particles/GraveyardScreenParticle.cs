using AllBeginningsMod.Common.Systems.Particles;
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

        Origin = ModContent.Request<Texture2D>(TexturePath).Value.Size() / 2f;
        
        IsAdditive = true;
        
        Alpha = 0f;
    }

    public override void Update() {
        base.Update();

        Velocity.Y -= (0.005f - (Velocity.Y * 0.005f));

        if (!fadeOut) {
            Alpha += 0.0025f;

            if (Alpha >= 0.75f) {
                fadeOut = true;
            }
        }
        else {
            Alpha -= 0.0025f;

            if (Alpha <= 0f) {
                Kill();
            }
        }
    }
}
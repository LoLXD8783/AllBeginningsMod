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
        base.OnSpawn();

        SpawnPosition = Position; 
        ParallaxOffset = 2f * (SpawnPosition - Main.screenPosition) * Parallax;
        Position -= ParallaxOffset; // one time for spawn
    }  

    public override void Update() { 
        base.Update();
        
        // ok wait im confused nwo bc to me the parallax was the offset OH WAIT i think i get it
    }

    public override void Draw() {
        Texture2D texture = ModContent.Request<Texture2D>(TexturePath).Value; 
        
        Vector2 parallax = Vector2.Lerp(Main.screenPosition, Main.screenPosition - 2f * (SpawnPosition - Main.screenPosition), Parallax);
        
        Main.EntitySpriteDraw(texture, Position - parallax, Frame, Color * Alpha, Rotation, Origin, Scale, Effects, 0);        
    }
}
using AllBeginningsMod.Common.Bases.Projectiles;
using AllBeginningsMod.Common.Graphics.Particles;
using AllBeginningsMod.Content.Particles;
using AllBeginningsMod.Utility;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace AllBeginningsMod.Content.Projectiles.Ranged;

public sealed class FrostBoltProjectile : ModProjectileBase
{
    public ref float Timer => ref Projectile.ai[1];
    
    public override string Texture => base.Texture.Replace("/Projectiles/Ranged/", "/Items/Ammo/").Replace("Projectile", "Item");

    public override void SetDefaults() {
        Projectile.friendly = true;
        
        Projectile.width = 12;
        Projectile.height = 12;

        Projectile.aiStyle = ProjAIStyleID.Arrow;
    }

    public override bool OnTileCollide(Vector2 oldVelocity) {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        return true;
    }

    public override void AI() {
        Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;

        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Frost);
        dust.noGravity = true;
        dust.alpha = 100;
        dust.velocity = Vector2.Zero;
        dust.scale = Main.rand.NextFloat(0.7f, 0.9f);
    }

    public override void Kill(int timeLeft) {
        for (int i = 0; i < 10; i++) {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Frost);
            dust.noGravity = true;
            dust.alpha = 100;
            dust.scale = Main.rand.NextFloat(0.7f, 0.9f);
        }
    }
}
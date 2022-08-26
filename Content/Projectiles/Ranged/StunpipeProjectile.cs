using AllBeginningsMod.Common.Bases.Projectiles;
using AllBeginningsMod.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Ranged;

public sealed class StunpipeProjectile : ModProjectileBase
{
    public override void SetDefaults() {
        Projectile.friendly = true;

        Projectile.width = 14;
        Projectile.height = 24;

        Projectile.timeLeft = 300;
        Projectile.aiStyle = ProjAIStyleID.Arrow;
    }
    public override bool OnTileCollide(Vector2 oldVelocity) {
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        return true;
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
        Dust.NewDustDirect(target.Center, 1, 1, DustID.Bone, Main.rand.Next(2, 6), Main.rand.Next(2, 6));
    }
}
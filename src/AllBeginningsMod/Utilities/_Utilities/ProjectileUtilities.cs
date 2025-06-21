using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace AllBeginningsMod.Utilities;

public static class ProjectileUtilities {
    public static Projectile NewProjectileCheckCollision(
        IEntitySource spawnSource,
        Vector2 position,
        Vector2 offset,
        Vector2 velocity,
        int type,
        int damage,
        float knockback,
        int owner = -1,
        float ai0 = 0,
        float ai1 = 0,
        float ai2 = 0
    ) {
        if(Collision.CanHit(position, 0, 0, position + offset, 0, 0)) {
            position += offset;
        }

        return Projectile.NewProjectileDirect(
            spawnSource,
            position,
            velocity,
            type,
            damage,
            knockback,
            owner,
            ai0,
            ai1,
            ai2
        );
    }
}
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.Projectiles;

public abstract class MinionProjectileBase<T> : ModProjectileBase where T : ModBuff
{
    /// <summary>
    /// Represents the currently targetted enemy.
    /// </summary>
    public NPC Target { get; protected set; }
    
    /// <summary>
    /// Represents whether the minion has a valid target or not.
    /// </summary>
    public bool HasTarget => Target != null && Target.active && Target.CanBeChasedBy();
    
    /// <summary>
    /// Whether this minion features right click targetting or not.
    /// </summary>
    public virtual bool CanTargetRightClick => true;
    
    /// <summary>
    /// Whether this minion can target enemies within tile collision or not.
    /// </summary>
    public virtual bool CanTargetThroughTiles => false;
    
    /// <summary>
    /// Represents the max distance allowed for targetting a enemy.
    /// </summary>
    public abstract float TargetDistance { get; }
    
    public override void SetStaticDefaults() {
        Main.projPet[Projectile.type] = true; 
        
        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; 
        ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
    }

    public override void SetDefaults() {
        Projectile.DamageType = DamageClass.Summon; 
        
        Projectile.friendly = true;
        Projectile.minion = true;
    }

    public override bool PreAI() {
        return CheckActive();
    }

    public override void AI() {
        FindTarget();
    }

    /// <summary>
    /// Attempts to find and set a valid target for this minion.
    /// </summary>
    protected void FindTarget() {
        // TODO: Targetting code.
    }

    /// <summary>
    /// Represents whether this minion meets all conditions for staying active or not.
    /// </summary>
    protected bool CheckActive() {
        bool active = Owner.HasBuff<T>() && Owner.active && !Owner.ghost;

        if (!active) {
            Owner.ClearBuff(ModContent.BuffType<T>());
            Projectile.Kill();
        }
        else {
            Projectile.timeLeft = 2;
        }

        return active;
    }
}
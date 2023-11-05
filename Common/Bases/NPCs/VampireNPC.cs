using AllBeginningsMod.Common.Graphics;
using AllBeginningsMod.Utilities.Extensions;
using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Common.Bases.NPCs;

public abstract class VampireNPC : ModNPC
{
    protected virtual int MaxExplodingTime => 110;
    protected virtual float FollowRange => 16 * 30;
    protected virtual float SlowDownFactor => 0.95f;
    protected virtual float ExplosionRange => 64f;
    //protected virtual void ExlodingEffects() { }
    protected virtual void ExplosionEffects() { }
    protected virtual void FollowBehaviour(Player target) {
        if (NPC.velocity.LengthSquared() > 0.8f) {
            return;
        }

        NPC.velocity += 0.05f * NPC.Center.DirectionTo(target.Center);
    }
    protected virtual void PostSetDefaults() { }
    protected virtual void Draw(SpriteBatch spriteBatch, Color drawColor, float explodingProgress) { }
    private Player Target => Main.player[NPC.target];
    protected ref float ExplodingTimer => ref NPC.ai[0];

    public sealed override void SetDefaults() {
        NPC.width = 36;
        NPC.height = 36;
        NPC.damage = 25;
        NPC.defense = 20;
        NPC.lifeMax = 80;
        NPC.value = 67f;
        NPC.netAlways = true;
        NPC.noTileCollide = true;
        NPC.aiStyle = -1;
        NPC.noTileCollide = true;
        NPC.noGravity = true;
        NPC.knockBackResist = 0.25f;

        NPC.HitSound = SoundID.NPCHit23;

        PostSetDefaults();
    }

    public sealed override void AI() {
        NPC.rotation = NPC.velocity.X * 0.1f;
        NPC.velocity *= SlowDownFactor;
        if (ExplodingTimer > 0f) {
            if (--ExplodingTimer == 0f) {
                if (!Main.dedServ) {
                    ExplosionEffects();
                } else {
                    TargetingUtils.ForEachPlayerInRange(
                        NPC.Center,
                        ExplosionRange,
                        player => player.Hurt(PlayerDeathReason.ByNPC(NPC.whoAmI), 40, MathF.Sign(player.Center.X - NPC.Center.X))
                    );
                }

                NPC.active = false;
                return;
            }

            if (!Main.dedServ) {
                if (Main.rand.NextBool((int)(ExplodingTimer / MaxExplodingTime * 10f + 3f))) {
                    Dust.NewDust(
                        NPC.Center + Main.rand.NextVector2Unit() * 20 * Main.rand.NextFloat(),
                        0,
                        0,
                        DustID.Stone,
                        Main.rand.NextFloatDirection() * 5,
                        -5 * Main.rand.NextFloat()
                    );
                }

                float lightAmount = ExplodingTimer / MaxExplodingTime;
                Lighting.AddLight(NPC.Center, 0.5f * lightAmount, 0.2f * lightAmount, 0.05f * lightAmount);
            }

            return;
        }

        if (
            NPC.target < 0 
            || NPC.target == 255 
            || Main.player[NPC.target].dead 
            || !Main.player[NPC.target].active 
            || NPC.DistanceSQ(Target.Center) > MathF.Pow(FollowRange, 2)
        ) {
            NPC.TargetClosest();
        }
        else {
            FollowBehaviour(Target);
        }

        NPC.Center += 0.25f * MathF.Sin(Main.GameUpdateCount * 0.025f) * Vector2.UnitY;
    }

    public sealed override bool CheckDead() {
        NPC.life = 1;
        ExplodingTimer = MaxExplodingTime;

        return false;
    }

    public override bool? CanBeHitByItem(Player player, Item item) {
        return ExplodingTimer == 0;
    }

    public sealed override bool? CanBeHitByProjectile(Projectile projectile) {
        return ExplodingTimer == 0;
    }

    public sealed override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
        Draw(spriteBatch, drawColor, ExplodingTimer == 0f ? 0f : 1f - ExplodingTimer / MaxExplodingTime);
        return false;
    }
}

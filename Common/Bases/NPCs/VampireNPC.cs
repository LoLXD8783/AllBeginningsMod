using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Bases.NPCs;

public abstract class VampireNPC : ModNPC {
    protected virtual int MaxExplodingTime => 110;
    protected virtual float FollowRange => 16 * 30;
    protected virtual float SlowDownFactor => 0.95f;
    protected virtual float ExplosionRange => 64f;
    protected virtual void Exploding(float progress) { }
    protected virtual void OnExplode() { }
    protected virtual void FollowBehaviour(Player target) {
        if(NPC.velocity.LengthSquared() > 0.8f) {
            return;
        }

        NPC.velocity += 0.05f * NPC.Center.DirectionTo(target.Center);
    }
    protected virtual void PostSetDefaults() { }
    protected virtual void Draw(SpriteBatch spriteBatch, Color drawColor, float explodingProgress) { }
    private Player Target => Main.player[NPC.target];
    private bool killed;
    protected ref float ExplodingTimer => ref NPC.ai[0];
    protected bool IsExploding => ExplodingTimer > 0;

    public sealed override void SetDefaults() {
        NPC.width = 36;
        NPC.height = 36;
        NPC.damage = 25;
        NPC.defense = 35;
        NPC.lifeMax = 120;
        NPC.value = 67f;
        NPC.noTileCollide = false;
        NPC.aiStyle = -1;
        NPC.noGravity = true;
        NPC.knockBackResist = 0.05f;
        NPC.friendly = false;

        NPC.HitSound = SoundID.NPCHit23;

        PostSetDefaults();
    }

    public sealed override void AI() {
        if(NPC.collideX) {
            NPC.velocity.X -= MathF.Sign(NPC.velocity.X) * 2f;
        }

        if(NPC.collideY) {
            NPC.velocity.Y -= MathF.Sign(NPC.velocity.Y) * 2f;
        }

        NPC.rotation = NPC.velocity.X * 0.1f;
        NPC.velocity *= SlowDownFactor;
        if(ExplodingTimer > 0f) {
            Exploding(ExplodingTimer == 0f ? 0f : 1f - ExplodingTimer / MaxExplodingTime);
            if(--ExplodingTimer == 0f) {
                if(!Main.dedServ) {
                    OnExplode();
                }

                if(Main.netMode != NetmodeID.MultiplayerClient) {
                    Helper.ForEachPlayerInRange(
                        NPC.Center,
                        ExplosionRange,
                        player => player.Hurt(
                            PlayerDeathReason.ByNPC(NPC.whoAmI),
                            40,
                            MathF.Sign(player.Center.X - NPC.Center.X),
                            knockback: 8f
                        )
                    );
                }

                NPC.StrikeInstantKill();
                return;
            }

            /*if (!Main.dedServ) {
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
            }*/

            return;
        }

        if(
            NPC.target < 0
            || NPC.target >= 255
            || Main.player[NPC.target].dead
            || !Main.player[NPC.target].active
            || !Main.player[NPC.target].Hitbox.Intersects(NPC.Center, FollowRange)
        ) {
            NPC.TargetClosest();
        }
        else {
            FollowBehaviour(Target);
        }

        NPC.velocity.Y += 0.025f * MathF.Sin(Main.GameUpdateCount * 0.025f);
    }

    public sealed override bool? CanFallThroughPlatforms() {
        return true;
    }

    public sealed override bool CheckDead() {
        if(killed) {
            return true;
        }

        killed = true;

        ExplodingTimer = MaxExplodingTime;
        NPC.dontTakeDamage = true;
        NPC.life = 1;

        return false;
    }

    public sealed override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
        Draw(spriteBatch, drawColor, ExplodingTimer == 0f ? 0f : 1f - ExplodingTimer / MaxExplodingTime);
        return false;
    }
}
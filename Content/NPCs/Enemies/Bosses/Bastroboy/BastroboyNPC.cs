using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.NPCs.Enemies.Bosses.Bastroboy;

internal class BastroboyNPC : ModNPC
{
    // Coner idae = make him jump then squish in air and send with giga speed towards player

    private const int MaxSquashTime = 28;
    public const int StarWhirlTime = 60 * 10;
    private enum Attacks {
        None,
        StarToss,
        CrescentToss,
        StarWhirl,
        CandyToss,
        RayBeams,
        Hammuh,
        RayBeamSword
    }

    private enum Phases {
        Phase1,
        Phase2,
        Phase3
    }

    private float squishSquash;
    private float SquishSquash 
    {
        get => squishSquash;
        set
        {
            squishSquash = Math.Clamp(value, -1f, 1f);
        } 
    }
    private Phases Phase {
        get => (Phases)NPC.ai[0];
        set {
            NPC.ai[0] = (int)value;
            NPC.netUpdate = true;
        }
    }
    private ref float AttackTimer => ref NPC.ai[1];
    private Attacks Attack { 
        get => (Attacks)NPC.ai[2];
        set {

            NPC.ai[2] = (int)value;
            NPC.netUpdate = true;
        }
    }
    public bool HitWithExplodingProjectile { get; set; }
    private const float Gravity = 0.4f;
    public override void SetDefaults() {
        NPC.width = 30;
        NPC.height = 80;
        NPC.knockBackResist = 0f;

        NPC.defense = 50;
        NPC.damage = 80;
        NPC.lifeMax = 100_000;

        NPC.boss = true;
        NPC.noGravity = true;
        NPC.noTileCollide = false;
        NPC.friendly = false;

        NPC.alpha = 0;
        NPC.aiStyle = -1;

        NPC.npcSlots = 40f;
        NPC.HitSound = SoundID.NPCHit2;

        /*if (!Main.dedServ)
            Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music");*/
    }

    public override void AI() {
        float distanceToTargetSQ = 9999999f;
        if (NPC.target != -1) {
            distanceToTargetSQ = NPC.Center.DistanceSQ(Main.player[NPC.target].Center);
        }
        switch (Phase) {
            case Phases.Phase1:
                if (Attack != Attacks.CandyToss && Attack != Attacks.StarWhirl) {
                    DoJumpingMovement();
                }

                switch (Attack) {
                    case Attacks.StarToss:
                    case Attacks.CrescentToss:
                        if (AttackTimer++ > 60) {
                            Attack = Attacks.None;
                            AttackTimer = Main.rand.Next(40, 60);
                        } else if (AttackTimer % 10 == 0) {
                            NPC.velocity *= 0.8f;
                            Projectile.NewProjectile(
                                NPC.GetSource_FromAI(),
                                NPC.Center,
                                Vector2.Zero,
                                ModContent.ProjectileType<BastroboyExplodingProjectile>(),
                                NPC.damage,
                                11f,
                                -1,
                                NPC.target,
                                Attack == Attacks.StarToss ? 0 : 1
                            );
                        }

                        break;
                    case Attacks.StarWhirl:
                        if (AttackTimer == 0) {
                            Projectile.NewProjectile(
                                NPC.GetSource_FromAI(), 
                                NPC.Center,
                                Vector2.Zero,
                                ModContent.ProjectileType<BastroboyStarWhirlProjectile>(), 
                                NPC.damage,
                                0f,
                                -1
                            );
                        }

                        SquishSquash *= 0.94f;
                        NPC.velocity *= 0.94f;
                        NPC.rotation *= 0.94f;

                        if (AttackTimer++ > StarWhirlTime) {
                            Attack = Attacks.None;
                            AttackTimer = Main.rand.Next(60, 90);
                            HitWithExplodingProjectile = false;
                        }
                        break;
                    case Attacks.CandyToss: 
                        break;
                    case Attacks.None:
                        if (AttackTimer == 0f) {
                            if (HitWithExplodingProjectile) {
                                if (NPC.velocity.Y < -0.005f && distanceToTargetSQ < 120_000) {
                                    Attack = Attacks.StarWhirl;
                                }
                            } else {
                                Attack = Main.rand.NextFromList(Attacks.StarToss, Attacks.CrescentToss);
                            }
                        } else {
                            AttackTimer--;
                        }
                        break;
                    default:
                        break;
                }

                break;
            case Phases.Phase2:
                break;
            case Phases.Phase3:
                break;
        }

        /*switch (Phase) {
            case AIState.Jumping:
                if (NPC.velocity.Y >= 0 && Collision.SolidCollision(NPC.position + Vector2.UnitY * NPC.height, NPC.width, 4)) {
                    Phase = AIState.OnGround;
                    NPC.velocity = Vector2.Zero;
                    Timer = 0f;
                    return;
                }

                if (Main.netMode != NetmodeID.MultiplayerClient && Timer % 25 == 0) {
                    Projectile.NewProjectile(
                        NPC.GetSource_FromAI(),
                        NPC.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<BastroboyExplodingProjectile>(),
                        NPC.damage,
                        10f,
                        -1,
                        NPC.target,
                        Main.rand.Next(2)
                    );
                }

                SquishSquash = -Math.Clamp(Math.Abs(NPC.velocity.Y), -10, 10) * 0.03f;
                NPC.velocity.Y += gravity;
                NPC.velocity.X += jumpVelocity.X * 0.001f;
                NPC.rotation = -0.05f * Math.Clamp(NPC.velocity.Y, -4, 4) * MathF.Sign(NPC.velocity.X);
                Timer++;
                break;
            case AIState.OnGround:
                if (Timer > MaxSquashTime) {
                    Phase = AIState.Jumping;
                    Timer = 0f;

                    CalculateJumpVelocity();
                    NPC.velocity = jumpVelocity;
                    for (int i = 0; i < 3; i++) {
                        Dust.NewDustPerfect(NPC.BottomLeft + Vector2.UnitX * Main.rand.Next(NPC.width), DustID.Cloud);
                    }
                    
                    return;
                }

                SquishSquash = MathF.Sin(MathHelper.Pi * Timer / MaxSquashTime) * 0.4f;
                NPC.rotation = MathHelper.Lerp(NPC.rotation, -0.05f * Math.Clamp(jumpVelocity.Y, -4, 4) * MathF.Sign(jumpVelocity.X), 0.05f);
                Timer++;
                break;
            case AIState.Fleeing:
                break;
        }*/
    }

    private float squashTimer;
    private Vector2 initialJumpVelocity;
    private void DoJumpingMovement() {
        if (squashTimer > 0) {
            SquishSquash = MathF.Sin(MathHelper.Pi * squashTimer / MaxSquashTime) * 0.4f;
            NPC.rotation = MathHelper.Lerp(NPC.rotation, -0.05f * Math.Clamp(initialJumpVelocity.Y, -4, 4) * MathF.Sign(initialJumpVelocity.X), 0.05f);
            squashTimer--;
        } else if (squashTimer == 0) {
            NPC.velocity = initialJumpVelocity;
            squashTimer--;
        } else {
            if (NPC.velocity.Y == 0) {
                NPC.velocity = Vector2.Zero;
                CalculateInitialJumpVelocity();
                squashTimer = MaxSquashTime;
            }

            SquishSquash = -Math.Clamp(Math.Abs(NPC.velocity.Y), -10, 10) * 0.03f;
            NPC.velocity.Y += Gravity;
            NPC.velocity.X += initialJumpVelocity.X * 0.001f;
            NPC.rotation = -0.05f * Math.Clamp(NPC.velocity.Y, -4, 4) * MathF.Sign(NPC.velocity.X);
        }
    }

    public void CalculateInitialJumpVelocity() {
        NPC.TargetClosest();
        if (NPC.target == -1) {
            initialJumpVelocity = -Vector2.UnitY * 3f;
            return;
        }

        Player target = Main.player[NPC.target];
        initialJumpVelocity = PhysicsUtils.InitialVelocityRequiredToHitPosition(
            NPC.Bottom,
            target.Bottom + target.velocity * 100f * Main.rand.NextFloat(0.5f, 0.95f), 
            Gravity, 
            16f
        );
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
        Texture2D texture = TextureAssets.Npc[Type].Value;
        float aspectRatio = (float)texture.Height / texture.Width;
        Main.spriteBatch.Draw(
             texture,
             NPC.Center + 0.5f * NPC.height * Vector2.UnitY - screenPos,
             null,
             drawColor,
             NPC.rotation,
             new Vector2(texture.Width / 2f, texture.Height),
             NPC.scale * new Vector2(1f + SquishSquash, 1f - SquishSquash * aspectRatio),
             SpriteEffects.None,
             0f
        );

        return false;
    }
}

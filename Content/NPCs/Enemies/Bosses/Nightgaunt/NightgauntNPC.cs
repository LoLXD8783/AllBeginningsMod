using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.NPCs.Enemies.Bosses.Nightgaunt
{
    internal class NightgauntNPC : ModNPC {
        private enum Phases {
            Phase1,
            Phase2
        }

        private enum Attacks {
            None,
            Swing,
            Jump,
            Shield
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

        private Player Target => Main.player[NPC.target];
        //private bool Grounded => Collision.SolidCollision(NPC.position + Vector2.UnitY * NPC.height, NPC.width, 4);
        private int Frame => Attack switch {
            Attacks.None => 0,
            Attacks.Swing => (int)(18 * AttackTimer / (swingTime + 1)),
            Attacks.Jump => AttackTimer > jumpTime ? 5 : (int)(6 * AttackTimer / (jumpTime + 1)),
            _ => 0,
        };

        private int LastFrame => Attack switch {
            Attacks.None => 0,
            Attacks.Swing => (int) (18 * (AttackTimer - 1) / (swingTime + 1)),
            Attacks.Jump => (AttackTimer - 1) > jumpTime? 5 : (int) (6 * (AttackTimer - 1) / (jumpTime + 1)),
            _ => 0,
        };

        private const int BigSwingAreaWidth = 400;
        private const int BigSwingAreaHeight = 150;
        private const int BigSwingAreaOffset = 50;
        private Rectangle BigSwingHitArea => new(
            (int)NPC.Center.X + (NPC.direction == -1 ? -BigSwingAreaWidth - BigSwingAreaOffset : BigSwingAreaOffset), 
            (int)NPC.Center.Y - BigSwingAreaHeight / 2, 
            BigSwingAreaWidth,
            BigSwingAreaHeight
        );

        private const int SmallSwingAreaWidth = 250;
        private const int SmallSwingAreaHeight = 175;
        private const int SmallSwingAreaOffset = 50;
        private Rectangle SmallSwingHitArea => new(
            (int)NPC.Center.X + (NPC.direction == -1 ? -SmallSwingAreaWidth - SmallSwingAreaOffset : SmallSwingAreaOffset),
            (int)NPC.Center.Y - SmallSwingAreaHeight / 2,
            SmallSwingAreaWidth,
            SmallSwingAreaHeight
        );

        private int shieldAttackCooldown;
        private int swingTime;
        private int shieldTime;
        private int jumpTime;
        private bool appliedJumpVelocity = false;
        private const float Gravity = 0.4f;

        public override string Texture => base.Texture + "_Swing";
        private Texture2D jumpTexture;
        private Texture2D shieldTexture;
        public override void SetDefaults() {
            NPC.width = 150;
            NPC.height = 175;
            NPC.knockBackResist = 0f;

            NPC.defense = 10;
            NPC.damage = 80;
            NPC.lifeMax = 10_000;

            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            NPC.friendly = false;

            NPC.alpha = 0;
            NPC.aiStyle = -1;

            NPC.npcSlots = 40f;
            NPC.HitSound = SoundID.NPCHit2;


            swingTime = 90;
            shieldTime = 300;
            jumpTime = 40;

            /*if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music");*/
        }

        public override void AI() {
            switch (Phase) {
                case Phases.Phase1:
                    switch (Attack) {
                        case Attacks.None:
                            AttackTimer++;
                            if (AttackTimer > 20) {
                                if (NPC.target == -1 || !Target.active || Target.dead) {
                                    NPC.TargetClosest();
                                }

                                if (!BigSwingHitArea.Intersects(Target.Hitbox)) {
                                    Attack = Attacks.Jump;
                                } else {
                                    Attack = Main.rand.NextFloat() < 0.2f && shieldAttackCooldown <= 0 ? Attacks.Shield : Attacks.Swing;
                                }

                                AttackTimer = 0;
                            }

                            FaceTarget();
                            break;
                        case Attacks.Swing:
                            AttackTimer++;
                            if (AttackTimer > swingTime) {
                                Attack = Attacks.None;
                                AttackTimer = 0;
                            }

                            if (Target.active && !Target.dead) {
                                if (Frame == 8 && LastFrame == 7 && BigSwingHitArea.Intersects(Target.Hitbox)) {
                                    Target.Hurt(
                                        new Player.HurtInfo() {
                                            Damage = NPC.damage,
                                            DamageSource = PlayerDeathReason.ByNPC(NPC.whoAmI),
                                            HitDirection = MathF.Sign(Target.Center.X - NPC.Center.X),
                                            Knockback = 10f,
                                        }
                                    );
                                }

                                if (Frame == 14 && LastFrame == 7 && SmallSwingHitArea.Intersects(Target.Hitbox)) {
                                    Target.Hurt(
                                        new Player.HurtInfo() {
                                            Damage = NPC.damage / 2,
                                            DamageSource = PlayerDeathReason.ByNPC(NPC.whoAmI),
                                            HitDirection = MathF.Sign(Target.Center.X - NPC.Center.X),
                                            Knockback = 14f,
                                        }
                                    );
                                }
                            }

                            break;
                        case Attacks.Jump:
                            AttackTimer++;
                            if (Frame == 4 && !appliedJumpVelocity) {
                                appliedJumpVelocity = true;
                                NPC.noTileCollide = true;
                                NPC.velocity = PhysicsUtils.InitialVelocityRequiredToHitPosition(NPC.Bottom, Target.Bottom, Gravity, Main.rand.NextFloat(15f, 19f));
                            }
                            
                            if (Frame < 4) {
                                FaceTarget();
                            }
                            
                            if (NPC.velocity.Y >= 0 && Frame == 5) {
                                NPC.noTileCollide = false;
                                if (NPC.velocity.Y == 0f) {
                                    Attack = Attacks.None;
                                    appliedJumpVelocity = false;
                                    TargetingUtils.ForEachPlayerInRange(
                                        NPC.Center,
                                        120,
                                        player => {
                                            player.Hurt(
                                                new Player.HurtInfo() {
                                                    Damage = NPC.damage / 2,
                                                    DamageSource = PlayerDeathReason.ByNPC(NPC.whoAmI),
                                                    HitDirection = MathF.Sign(Target.Center.X - NPC.Center.X),
                                                    Knockback = 14f,
                                                }
                                            );
                                        }
                                    );
                                    JumpSmashEffects();
                                    AttackTimer = 0;
                                }
                            }

                            break;
                        case Attacks.Shield:
                            if (AttackTimer >= shieldTime) {
                                shieldAttackCooldown = 900;
                                Attack = Attacks.None;
                                AttackTimer = 0;
                            }

                            FaceTarget();
                            AttackTimer++;
                            break;
                    }
                    break;
                case Phases.Phase2:
                    break;
            }

            if (Attack != Attacks.Jump && NPC.velocity.Y == 0f) {
                NPC.velocity.X *= 0.885f;
            }

            if (shieldAttackCooldown > 0) {
                shieldAttackCooldown--;
            }
            NPC.velocity.Y += Gravity;
        }

        public override bool? CanFallThroughPlatforms() {
            return Target is not null && Target.Top.Y > NPC.Bottom.Y;
        }

        private void FaceTarget() {
            if (NPC.target == -1) {
                return;
            }

            NPC.direction = MathF.Sign(Target.Center.X - NPC.Center.X); 
        }

        private void JumpSmashEffects() {
            int dustCount = 25;
            for (int i = 0; i < dustCount; i++) {
                Vector2 position = NPC.BottomLeft + Vector2.UnitX * Main.rand.NextFloat(NPC.width);
                Dust.NewDustPerfect(position, DustID.Dirt, new Vector2(MathF.Sign(position.X - NPC.Center.X) * 2f, -6f) * Main.rand.NextFloat());
            }
            
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot) {
            return Attack == Attacks.Jump;
        }

        public override void FindFrame(int frameHeight) {
            switch (Attack) {
                case Attacks.None:
                    NPC.frame = new(0, 0, 732, 288);
                    break;
                case Attacks.Swing:
                    NPC.frame = new Rectangle(0, 0, 732, 288);
                    NPC.frame.X = NPC.frame.Width * (Frame / 6);
                    NPC.frame.Y = NPC.frame.Height * (Frame % 6);
                    break;
                case Attacks.Jump:
                    NPC.frame = new(0, 0, 274, 284);
                    NPC.frame.Y = NPC.frame.Height * Frame;
                    break;
                case Attacks.Shield:
                    NPC.frame = new(0, 240, 336, 240);
                    break;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
            Texture2D swingTexture = TextureAssets.Npc[Type].Value;
            jumpTexture ??= ModContent.Request<Texture2D>(Texture.Replace("Swing", "Jump"), ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            shieldTexture ??= ModContent.Request<Texture2D>(Texture.Replace("Swing", "Shield"), ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

            Texture2D texture = Attack switch {
                Attacks.None or Attacks.Swing => swingTexture,
                Attacks.Jump => jumpTexture,
                _ => shieldTexture,
            };

            Vector2 offset = Attack switch {
                Attacks.None or Attacks.Swing => new(70, 55),
                Attacks.Jump => new Vector2(19, 53) + Frame switch {
                    4 => new Vector2(-20, -20),
                    5 => new Vector2(-50, -80),
                    _ => Vector2.Zero
                },
                _ => new(0, 31),
            };
            offset.X *= -NPC.direction;

            Main.spriteBatch.Draw(
                texture,
                NPC.Center - screenPos,
                NPC.frame,
                drawColor,
                NPC.rotation,
                NPC.frame.Size() / 2f + offset,
                NPC.scale,
                NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0f
            );

            return false;
        }
    }
}

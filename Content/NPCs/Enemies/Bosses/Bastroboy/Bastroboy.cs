using AllBeginningsMod.Common.PrimitiveDrawing;
using AllBeginningsMod.Utilities;
using AllBeginningsMod.Utilities.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.NPCs.Enemies.Bosses.Bastroboy
{
    internal class Bastroboy : ModNPC
    {
        private enum AIState {
            Jumping,
            OnGround,
            Fleeing
        }

        private float SquishSquash 
        { 
            get => NPC.ai[2];
            set
            {
                NPC.ai[2] = Math.Clamp(value, -1f, 1f);
            } 
        }

        private Vector2 jumpVelocity;
        private ref float Timer => ref NPC.ai[0];
        private readonly int maxSquashTime = 20;
        private AIState State { get => (AIState)NPC.ai[1]; set => NPC.ai[1] = (int)value; }
        private readonly float gravity = 0.4f;
        public override void SetDefaults() {
            NPC.width = 30;
            NPC.height = 80;
            NPC.knockBackResist = 0f;

            NPC.defense = 50;
            NPC.damage = 80;
            NPC.lifeMax = 5000;

            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = false;

            NPC.alpha = 0;
            NPC.aiStyle = -1;

            NPC.npcSlots = 40f;
            NPC.HitSound = SoundID.NPCHit2;

            /*if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music");*/
        }

        public override void OnSpawn(IEntitySource source) {
            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<BastroboyOrbitingProjectile>(), NPC.damage, 11f);
            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<BastroboyOrbitingProjectile>(), NPC.damage, 11f, -1, 1);
        }

        public override void AI() {
            switch (State) {
                case AIState.Jumping:
                    if (NPC.velocity.Y >= 0 && Collision.SolidCollision(NPC.position + Vector2.UnitY * NPC.height, NPC.width, 4)) {
                        State = AIState.OnGround;
                        NPC.velocity = Vector2.Zero;
                        CalculateJumpVelocity();
                        return;
                    }

                    SquishSquash = -Math.Clamp(Math.Abs(NPC.velocity.Y), -10, 10) * 0.03f;
                    NPC.velocity.Y += gravity;
                    NPC.rotation = -0.05f * Math.Clamp(NPC.velocity.Y, -4, 4) * MathF.Sign(NPC.velocity.X);
                    break;
                case AIState.OnGround:
                    if (Timer > maxSquashTime) {
                        State = AIState.Jumping;
                        Timer = 0f;
                        
                        NPC.velocity = jumpVelocity;
                        for (int i = 0; i < 3; i++) {
                            Dust.NewDustPerfect(NPC.BottomLeft + Vector2.UnitX * Main.rand.Next(NPC.width), DustID.Cloud);
                        }
                        
                        return;
                    }

                    SquishSquash = MathF.Sin(MathHelper.Pi * Timer / maxSquashTime) * 0.4f;
                    NPC.rotation = MathHelper.Lerp(NPC.rotation, -0.05f * Math.Clamp(jumpVelocity.Y, -4, 4) * MathF.Sign(jumpVelocity.X), 0.05f);
                    Timer++;
                    break;
                case AIState.Fleeing:
                    break;
            }
        }

        public void CalculateJumpVelocity() {
            NPC.TargetClosest();
            if (NPC.target == -1) {
                State = AIState.Fleeing;
                return;
            }

            Player target = Main.player[NPC.target];
            jumpVelocity = PhysicsUtils.InitialVelocityRequiredToHitPosition(NPC.Bottom, target.Bottom, gravity, 12f);
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
}

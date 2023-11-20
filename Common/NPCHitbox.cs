using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common
{
    internal class NPCHitbox
    {
        /// <param name="parent">NPC to attach to.</param>
        /// <param name="localCenter">Position with origin in NPC.Center</param>
        public static void Create(NPC parent, Func<Vector2> localCenter, int width, int height, bool friendly = false) {
            NPCHitboxNPC hitboxNPC = (NPCHitboxNPC)NPC.NewNPCDirect(parent.GetSource_FromThis(), 0, 0, ModContent.NPCType<NPCHitboxNPC>()).ModNPC;
            hitboxNPC.Parent = parent;
            hitboxNPC.LocalCenter = localCenter;
            hitboxNPC.NPC.width = width;
            hitboxNPC.NPC.height = height;
            hitboxNPC.NPC.friendly = friendly;
        }

        private class NPCHitboxNPC : ModNPC {
            public override string Texture => "Terraria/Images/Item_0";
            public NPC Parent { get; set; }
            public Func<Vector2> LocalCenter { get; set; }
            public override void SetDefaults() {
                NPC.width = 90;
                NPC.height = 140;
                NPC.knockBackResist = 0f;

                NPC.defense = 100;
                NPC.damage = 80;
                NPC.lifeMax = 9999999;

                NPC.noGravity = true;
                NPC.noTileCollide = true;

                NPC.alpha = 0;
                NPC.aiStyle = -1;

                NPC.HitSound = SoundID.NPCHit2;
            }

            public override void AI() {
                if (Parent is null || !Parent.active) {
                    NPC.active = false;
                    return;
                }

                NPC.Center = Parent.Center + LocalCenter();
            }

            public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone) {
                Parent.StrikeNPC(hit);
                NetMessage.SendStrikeNPC(Parent, hit);
            }

            public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone) {
                Parent.StrikeNPC(hit);
                NetMessage.SendStrikeNPC(Parent, hit);
            }

            public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) {
                return false;
            }

            public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Buffs
{
    internal class OnGasDebuff : ModBuff
    {
        public override void SetStaticDefaults() {
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex) {
            if (npc.dontTakeDamage || npc.friendly) {
                return;
            }
            if ((Main.GameUpdateCount + buffIndex * 11) % 20 == 0) {
                NPC.HitInfo info = npc.CalculateHitInfo(10, 1);
                npc.StrikeNPC(info);
                if (Main.netMode == NetmodeID.SinglePlayer) {
                    NetMessage.SendStrikeNPC(npc, in info);
                }
            }
        }
    }

    internal class OnGasDebuffPlayer : ModPlayer {
        public override void UpdateBadLifeRegen() {
            if (!Player.HasBuff<OnGasDebuff>()) {
                return;
            }

            Player.lifeRegenTime = 0;
            Player.lifeRegen = Math.Min(0, Player.lifeRegen) - 20;
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace AllBeginningsMod.Utilities.Extensions
{
    internal static class NPCExtensions {
        public static bool SolidCollision(this NPC npc, Vector2 offset) {
            return Collision.SolidCollision(npc.position + offset, npc.width, npc.height);
        }

        public static bool SolidCollision(this NPC npc) {
            return SolidCollision(npc, Vector2.Zero);
        }
    }
}

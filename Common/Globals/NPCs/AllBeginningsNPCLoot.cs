using AllBeginningsMod.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Globals.NPCs
{
    public sealed class AllBeginningsNPCLoot : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (NPCID.Sets.Zombies[npc.type])
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DeathEssence>(), 3));
            }
        }
    }
}
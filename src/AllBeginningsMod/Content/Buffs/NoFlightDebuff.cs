using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Buffs; 

internal class NoFlightDebuff : ModBuff {
    public override string Texture => Assets.Assets.Textures.Buffs.KEY_NoFlightDebuff;
    
    public override void SetStaticDefaults() {
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        Main.debuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex) {
        player.wingTime = 0;
    }
}
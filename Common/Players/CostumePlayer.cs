using AllBeginningsMod.Common.Bases;
using AllBeginningsMod.Utilities;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Players;

public sealed class CostumePlayer : ModPlayer
{
    public override void FrameEffects() {
        foreach (CostumeItemBase costumeItem in Mod.GetContent<CostumeItemBase>()) {
            if (Player.HasEquip(costumeItem.Type) || Player.HasVanityEquip(costumeItem.Type)) {
                Player.head = EquipLoader.GetEquipSlot(Mod, costumeItem.Name, EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, costumeItem.Name, EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, costumeItem.Name, EquipType.Legs);
            }
        }
    }
}
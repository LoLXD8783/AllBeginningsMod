using AllBeginningsMod.Content.Items.Accessories;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Players;

public sealed class AccessoryPlayer : ModPlayer
{
    public bool FinoftheDolphin { get; set; }

    public override void ResetEffects() {
        FinoftheDolphin = false;
    }

    public override void FrameEffects() {
        if (FinoftheDolphin) {
            FinoftheDolphinItem exampleCostume = ModContent.GetInstance<FinoftheDolphinItem>();

            Player.head = EquipLoader.GetEquipSlot(Mod, exampleCostume.Name, EquipType.Head);
            Player.body = EquipLoader.GetEquipSlot(Mod, exampleCostume.Name, EquipType.Body);
            Player.legs = EquipLoader.GetEquipSlot(Mod, exampleCostume.Name, EquipType.Legs);
        }
    }
}
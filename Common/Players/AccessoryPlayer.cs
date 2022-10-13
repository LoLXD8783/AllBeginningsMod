using AllBeginningsMod.Content.Items.Accessories;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Players;

public sealed class AccessoryPlayer : ModPlayer
{
    public bool FinoftheDolphin { get; set; }
    public bool ScroungerPendant { get; set; }

    public override void ResetEffects() {
        FinoftheDolphin = false;
        ScroungerPendant = false;
    }
    public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
        if (ScroungerPendant && item.DamageType == DamageClass.Ranged) {
            if (Main.rand.NextBool(3)) {
                Projectile.NewProjectileDirect(source, position, velocity, ProjectileID.BoneGloveProj, damage, knockback, Main.LocalPlayer.whoAmI).timeLeft = 40;
            }
        }
        return true;
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
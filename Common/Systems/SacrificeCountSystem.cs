using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems;

public sealed class SacrificeCountSystem : ModSystem
{
    public override void PostSetupContent() {
        foreach (ModItem modItem in Mod.GetContent<ModItem>()) {
            Item item = new(modItem.Type);

            bool isTile = item.createTile > -1;
            bool isWall = item.createWall > -1;

            bool isWeapon = item.damage > 0;
            bool isEquip = item.accessory || item.defense > 0;

            int sacrificeCount = 25;

            if (isEquip) {
                sacrificeCount = 1;
            }
            else if (isTile) {
                sacrificeCount = 100;
            }
            else if (isWall) {
                sacrificeCount = 400;
            }
            else if (isWeapon) {
                sacrificeCount = item.consumable ? 100 : 1;
            }

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[modItem.Type] = sacrificeCount;
        }
    }
}
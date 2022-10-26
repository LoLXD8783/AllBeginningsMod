using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.WorldGeneration;

public sealed class ChestLootSystem : ModSystem
{
    public override void PostWorldGen() {
        for (int i = 0; i < Main.maxChests; i++) {
            Chest chest = Main.chest[i];

            if (chest == null) {
                continue;
            }

            SetChestLoot(Framing.GetTileSafely(chest.x, chest.y), chest);
        }
    }

    private static void SetChestLoot(Tile tile, Chest chest) {
        foreach (ChestLoot chestLoot in ModContent.GetContent<ChestLoot>()) {
            if (!tile.HasTile || tile.TileType != TileID.Containers || tile.TileFrameX != chestLoot.ChestFrame) {
                continue;
            }

            chestLoot.SetLoot(chest);
        }
    }
}
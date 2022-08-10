using AllBeginningsMod.Content.Tiles.Plants;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Globals.Tiles;

public sealed class RandomUpdatesGlobalTile : GlobalTile
{
    public override void RandomUpdate(int i, int j, int type) {
        if (type == TileID.Grass && Main.rand.NextBool(100) && !Main.dayTime) {
            WorldGen.PlaceObject(i, j - 1, ModContent.TileType<DeathBlossomTile>());
            NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<DeathBlossomTile>(), 0, 0, -1, -1);
        }
    }
}
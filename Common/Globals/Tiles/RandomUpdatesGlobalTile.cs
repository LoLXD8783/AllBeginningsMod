using AllBeginningsMod.Content.Tiles.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Globals.Tiles
{
    public sealed class RandomUpdatesGlobalTile : GlobalTile
    {
        public override void RandomUpdate(int i, int j, int type) {
            if (type == TileID.Tombstones) {
                int offset = WorldGen.genRand.Next(-4, 4);

                WorldGen.PlaceObject(i + offset, j, ModContent.TileType<DeathBlossomTile>());
                NetMessage.SendObjectPlacment(-1, i + offset, j, ModContent.TileType<DeathBlossomTile>(), 0, 0, -1, -1);
            }
        }
    }
}
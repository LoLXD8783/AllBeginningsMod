using AllBeginningsMod.Content.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Globals.Tiles
{
    public sealed class ABRandomUpdatesTile : GlobalTile
    {
        public override void RandomUpdate(int i, int j, int type)
        {
            if (type == TileID.Tombstones)
            {
                int offset = WorldGen.genRand.Next(-4, 4);

                if (WorldGen.PlaceObject(i + offset, j, ModContent.TileType<DeathBlossomTile>()))
                {
                    for (int k = 0; k < 8; k++)
                    {
                        Dust.NewDust(new Vector2(i + offset, j) * 16f, 16, 32, ModContent.GetInstance<DeathBlossomTile>().DustType);
                    }
                    
                    NetMessage.SendObjectPlacment(-1, i + offset, j, ModContent.TileType<DeathBlossomTile>(), 0, 0, -1, -1);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using AllBeginningsMod.Common.Loaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AllBeginningsMod.Content.NPCs.Enemies.Bosses.Chafer
{
    internal class ChaferArenaWorldGenSystem : ModSystem
    {
        public static int ArenaX;
        public static int ArenaY;
        public static int ArenaWidth;
        public static int ArenaHeight;
        
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) {
            int index = tasks.FindIndex(task => task.Name is "Remove Broken Traps");
            if (index == -1) {
                return;
            }

            tasks.Insert(
                index + 1, 
                new PassLegacy(
                    "",
                    (_, _) => {
                        ImageData data = ImageStructureLoader.Get("ChaferArena");
                        ArenaWidth = data.Width;
                        ArenaHeight = data.Height;
                        
                        const int spawnWidth = 200;
                        ArenaX = Main.rand.Next(Main.maxTilesX / 2 - spawnWidth, Main.maxTilesX / 2 + spawnWidth);
                        ArenaY = Main.rand.Next((int)Main.rockLayer, Main.UnderworldLayer - ArenaHeight);
                        data.EnumeratePixels(
                            (i, j, color) => {
                                i += ArenaX;
                                j += ArenaY;
                                
                                if (color.R != 49) {
                                    PlaceWall(i, j, WallID.LunarBrickWall);
                                    RemoveTile(i, j);
                                }

                                switch (color.R) {
                                    case 0:
                                    case 78:
                                        PlaceTile(i, j, TileID.Stone);
                                        break;
                                    case 128:
                                        WorldGen.PlaceTile(i, j, TileID.Platforms, style: 2);
                                        break;
                                    case 200:
                                        WorldGen.PlaceTile(i, j, TileID.Torches);
                                        break;
                                }
                            }
                        );

                        return;

                        static void PlaceWall(int i, int j, int wallType) {
                            Main.tile[i, j].WallType = (ushort)wallType;
                        }

                        static void PlaceTile(int i, int j, int tileType) {
                            Tile tile = Main.tile[i, j];
                            tile.TileType = (ushort)tileType;
                            tile.BlockType = BlockType.Solid;
                            tile.Get<TileWallWireStateData>().HasTile = true;
                        }

                        static void RemoveTile(int i, int j) {
                            Tile tile = Main.tile[i, j];
                            tile.Get<TileWallWireStateData>().HasTile = false;
                            tile.LiquidAmount = 0;
                        }
                    }
                )
            );
        }
    }
}

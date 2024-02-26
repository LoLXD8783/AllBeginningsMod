using AllBeginningsMod.Common;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AllBeginningsMod.Content.Biomes
{
    internal class GardenBiome : ModBiome
    {
        private class GardenSystem : ModSystem
        {
            public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) {
                int index = tasks.FindIndex(genpass => genpass.Name.Equals("Spawn Point"));
                if (index == -1) {
                    return;
                }

                tasks.Insert(index + 1, new PassLegacy("Garden", GenerateBiome));
            }

            public static void GenerateBiome(GenerationProgress progress, GameConfiguration configuration) {
                const int height = 75;
                const int width = 128;
                Rectangle bounds = new(
                    Main.spawnTileX + Main.rand.Next(-40, 40) - width / 2,
                    Main.rand.Next(700, 900) - height / 2,
                    width,
                    height
                );


                for (int i = bounds.X; i < bounds.X + bounds.Width; i++) {
                    for (int j = bounds.Y; j < bounds.Y + bounds.Height; j++) {
                        WorldGen.KillTile(i, j);
                        WorldGen.KillWall(i, j);
                    }
                }



                AllBeginningsWorld.GardenBounds = bounds;
            }
        }
    }
}

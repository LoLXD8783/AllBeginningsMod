using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AllBeginningsMod.Common;

internal class AllBeginningsWorld : ModSystem {
    public bool BastroboyDead = false;

    public static Rectangle GardenBounds = new();

    public override void SaveWorldData(TagCompound tag) {
        tag["BastroBoyDead"] = BastroboyDead;
    }

    public override void LoadWorldData(TagCompound tag) {
        BastroboyDead = tag.Get<bool>("BastroBoyDead");
    }
}
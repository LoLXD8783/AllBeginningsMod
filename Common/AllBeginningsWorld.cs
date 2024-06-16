using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AllBeginningsMod.Common;

internal class AllBeginningsWorld : ModSystem {
    public bool BastroboyDead = false;

    public static Rectangle GardenBounds = new();

    private void SaveRectangle(BinaryWriter writer, Rectangle rect) {
        writer.Write(rect.X);
        writer.Write(rect.Y);
        writer.Write(rect.Width);
        writer.Write(rect.Height);
    }

    private Rectangle LoadRectangle(BinaryReader reader) {
        return new Rectangle(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
    }

    public override void SaveWorldData(TagCompound tag) {
        tag["BastroBoyDead"] = BastroboyDead;
    }

    public override void LoadWorldData(TagCompound tag) {
        BastroboyDead = tag.Get<bool>("BastroBoyDead");
    }

    public override void NetSend(BinaryWriter writer) {
        SaveRectangle(writer, GardenBounds);
        base.NetSend(writer);
    }

    public override void NetReceive(BinaryReader reader) {
        GardenBounds = LoadRectangle(reader);
        base.NetReceive(reader);
    }
}
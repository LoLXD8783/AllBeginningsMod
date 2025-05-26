using System.IO;
using Terraria.ModLoader;

namespace AllBeginningsMod;

public sealed class AllBeginningsMod : Mod {
    public static AllBeginningsMod Instance { get; private set; }
    public AllBeginningsMod() {
        Instance = this;
    }

    public override void Unload() {
        Instance = null;
    }

    public override void HandlePacket(BinaryReader reader, int whoAmI) {
        //PacketManager.Receive(reader, whoAmI);
    }
}
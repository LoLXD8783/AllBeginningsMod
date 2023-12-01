using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria;
using Terraria.ModLoader;
using System.IO;
using AllBeginningsMod.Common.Netcode;

namespace AllBeginningsMod;

public sealed class AllBeginningsMod : Mod
{
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
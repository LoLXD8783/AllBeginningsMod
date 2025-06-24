using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AllBeginningsMod.Content.Tiles.Crimson;

public class BigShadewoodTreeTrunk : ModTile {
    public override string Texture => Assets.Assets.Textures.Tiles.Crimson.KEY_BigShadewoodTrunk;

    public override void SetStaticDefaults() {
        Main.tileMergeDirt[Type] = false;
        Main.tileBlockLight[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileFrameImportant[Type] = true;   
        
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.addTile(Type);

        TileID.Sets.IsATreeTrunk[Type] = true;
        AddMapEntry(new Color(73, 89, 97), Language.GetText("MapObject.Tree"));

        RegisterItemDrop(ItemID.Shadewood);
    }

    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak) {
        return false;
    }
}
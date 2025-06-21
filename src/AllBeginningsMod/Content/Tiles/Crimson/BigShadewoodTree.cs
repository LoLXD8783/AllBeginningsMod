using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AllBeginningsMod.Content.Tiles.Crimson;

// public class BigShadewoodTreeTrunkBase : ModTile {
//     public override string Texture => Assets.Assets.Textures.Tiles.Crimson.KEY_BigShadewoodTrunkBase;
//
//     public override void SetStaticDefaults() {
//         Main.tileSolid[Type] = false;
//         Main.tileMergeDirt[Type] = false;
//         Main.tileBlockLight[Type] = false;
//         Main.tileFrameImportant[Type] = true;
//     }
// }

public class BigShadewoodTreeTrunk : ModTile {
    public override string Texture => Assets.Assets.Textures.Tiles.Crimson.KEY_BigShadewoodTrunk;

    public override void SetStaticDefaults() {
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = false;
        Main.tileBlockLight[Type] = false;
        Main.tileFrameImportant[Type] = true;
        
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 2, 0);
        
        TileObjectData.addTile(Type);
    }
}
using AllBeginningsMod.Common.Bases.Tiles;
using AllBeginningsMod.Content.Items.Placeables;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AllBeginningsMod.Content.Tiles;

public sealed class MysteriousTotemTile : ModTileBase
{
    public override void SetStaticDefaults() {
        Main.tileNoFail[Type] = true;
        Main.tileLighted[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.addTile(Type);

        DustType = DustID.Iron;
        HitSound = SoundID.Dig;
        MineResist = 2f;
    }

    public override void NumDust(int i, int j, bool fail, ref int num) {
        num = fail ? 1 : 3;
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY) {
        Item.NewItem(
            new EntitySource_TileBreak(i, j),
            i * 16,
            j * 16,
            16,
            32,
            ModContent.ItemType<MysteriousTotemItem>()
        );
    }
}
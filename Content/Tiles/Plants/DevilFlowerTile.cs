using AllBeginningsMod.Common.Bases.Tiles;
using AllBeginningsMod.Content.Buffs;
using AllBeginningsMod.Content.Items.Placeables.Plants;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AllBeginningsMod.Content.Tiles.Plants;

public sealed class DevilFlowerTile : ModTileBase
{
    public override void SetStaticDefaults() {
        Main.tileNoFail[Type] = true;
        Main.tileLighted[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);

        TileObjectData.newTile.Height = 4;
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16 };

        TileObjectData.addTile(Type);

        DustType = DustID.JunglePlants;
        HitSound = SoundID.Grass;
        MineResist = 1f;
    }

    public override void NearbyEffects(int i, int j, bool closer) {
        Player player = Main.LocalPlayer;

        if (!player.dead)
            player.AddBuff(ModContent.BuffType<DevilFlowerBuff>(), 90);
    }

    public override void NumDust(int i, int j, bool fail, ref int num) {
        num = 2;
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY) {
        Item.NewItem(
            new EntitySource_TileBreak(i, j),
            i * 16,
            j * 16,
            96,
            64,
            ModContent.ItemType<DevilFlowerItem>()
        );
    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;

namespace AllBeginningsMod.Content.Tiles
{
    public sealed class MysteriousTotemTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSpelunker[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
            TileObjectData.addTile(Type);

            DustType = DustID.Iron;
            HitSound = SoundID.Dig;
            MineResist = 2f;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<Items.Tiles.MysteriousTotemItem>());
        }
    }
}

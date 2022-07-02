using AllBeginningsMod.Content.Items.Placeables;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AllBeginningsMod.Content.Tiles
{
    public sealed class DevilFlowerTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileNoFail[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);

            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.CoordinateHeights = new int[]
            {
                16,
                16,
                16,
                16
            };

            TileObjectData.addTile(Type);

            DustType = DustID.JunglePlants;
            HitSound = SoundID.Grass;
            MineResist = 1f;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Player player = Main.LocalPlayer;
            if (!player.HasBuff(ModContent.BuffType<Buffs.DevilFlowerBuff>()))
            {
                player.AddBuff(ModContent.BuffType<Buffs.DevilFlowerBuff>(), 2);
            }
        }
        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
        public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 32, ModContent.ItemType<DevilFlowerItem>());
    }
}
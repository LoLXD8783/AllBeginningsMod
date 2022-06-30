using AllBeginningsMod.Common.CustomEntities.Particles;
using AllBeginningsMod.Content.CustomEntities.Particles;
using AllBeginningsMod.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AllBeginningsMod.Content.Tiles
{
    public sealed class DeathBlossomTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileCut[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Plant"]);

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);

            TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.EmptyTile, 1, 0);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 0);

            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.newTile.UsesCustomCanPlace = true;

            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.RandomStyleRange = 2;

            TileObjectData.newTile.AnchorValidTiles = new int[]
            {
                TileID.Grass
            };

            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                TileID.ClayPot,
                TileID.PlanterBox
            };

            TileObjectData.addTile(Type);

            DustType = DustID.Enchanted_Pink;
            HitSound = SoundID.Grass;
            MineResist = 1.5f;
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            for (int k = 0; k < 8; k++)
            {
                Dust.NewDust(new Vector2(i, j) * 16f, 16, 32, DustType);
            }
        }
        
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.rand.NextBool(200))
            {
                ParticleSystem.Spawn(new DeathBlossomParticle
                {
                    Position = new Vector2(i, j + 2) * 16f + new Vector2(Main.rand.NextFloat(-20f, -20f), 0f)
                }); 
            }

            Texture2D glowmaskTexture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;

            Tile tile = Framing.GetTileSafely(i, j);
            
            Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange); 
            Vector2 drawPosition = new Vector2(i, j) * 16f - Main.screenPosition + zero + new Vector2(0f, TileObjectData.GetTileData(tile).DrawYOffset);
            
            Rectangle frame = new(tile.TileFrameX, tile.TileFrameY, 18, 18);

            spriteBatch.Draw(glowmaskTexture, drawPosition, frame, Color.White, 0f, default, 1f, SpriteEffects.None, 0f);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.1f;
            b = 0.4f;
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = 2;

        public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(new EntitySource_TileBreak(i, j), new Rectangle(i * 16, j * 16, 16, 32), ModContent.ItemType<DeathBlossomItem>());
    }
}
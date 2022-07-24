using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AllBeginningsMod.Common.Bases.Tiles
{
    public abstract class RelicTileBase : ModTile
    {
        public const int FrameWidth = 18 * 3;
        public const int FrameHeight = 18 * 4;

        public const int HorizontalFrames = 1;
        public const int VerticalFrames = 1;

        public virtual string RelicTexturePath => GetType().FullName.Replace('.', '/');

        public sealed override string Texture => "AllBeginningsMod/Content/Tiles/Relics/RelicPedestalTile";

        public override void SetStaticDefaults() {
            Main.tileShine[Type] = 400;
            Main.tileFrameImportant[Type] = true;

            TileID.Sets.InteractibleByNPCs[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
            TileObjectData.newTile.StyleHorizontal = false;
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
            TileObjectData.addAlternate(1);
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(233, 207, 94), Language.GetText("MapObject.Relic"));
        }

        public override bool CreateDust(int i, int j, ref int type) {
            return false;
        }

        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY) {
            tileFrameX %= FrameWidth;
            tileFrameY %= FrameHeight * 2;
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData) {
            if (drawData.tileFrameX % FrameWidth == 0 && drawData.tileFrameY % FrameHeight == 0) {
                Main.instance.TilesRenderer.AddSpecialLegacyPoint(i, j);
            }
        }

        public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch) {
            Vector2 offScreen = Main.drawToScreen ? Vector2.Zero : new(Main.offScreenRange);
            Point point = new(i, j);
            Tile tile = Framing.GetTileSafely(point.X, point.Y);
            Texture2D texture = ModContent.Request<Texture2D>(RelicTexturePath).Value;

            int frameY = tile.TileFrameX / FrameWidth;
            Rectangle frame = texture.Frame(HorizontalFrames, VerticalFrames, 0, frameY);

            Vector2 origin = frame.Size() / 2f;
            Vector2 worldPosition = point.ToWorldCoordinates(24f, 64f);
            Color color = Lighting.GetColor(point.X, point.Y);

            bool direction = tile.TileFrameY / FrameHeight != 0;
            SpriteEffects effects = direction ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            float offset = MathF.Sin(Main.GlobalTimeWrappedHourly * MathHelper.TwoPi / 5f);
            Vector2 drawPos = worldPosition + offScreen - Main.screenPosition - new Vector2(0f, 40f) + new Vector2(0f, offset * 4f);

            Main.EntitySpriteDraw(texture, drawPos, frame, color, 0f, origin, 1f, effects, 0);

            float scale = MathF.Sin(Main.GlobalTimeWrappedHourly * MathHelper.TwoPi / 2f) * 0.3f + 0.7f;
            Color effectColor = color;
            effectColor.A = 0;
            effectColor = effectColor * 0.1f * scale;

            for (float k = 0f; k < 1f; k += 355f / (678f * MathHelper.Pi)) {
                Main.EntitySpriteDraw(texture, drawPos + (MathHelper.TwoPi * k).ToRotationVector2() * (6f + offset * 2f), frame, effectColor, 0f, origin, 1f, effects, 0);
            }
        }
    }
}
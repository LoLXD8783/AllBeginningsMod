using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AllBeginningsMod.Content.Tiles.Forest;

//okk so, if were gonna have lots of tiles that have stuff like this i should probably come up with a dummy system or something

public sealed class GiantShroom : ModTile {
    public override string Texture => "AllBeginningsMod/Content/Tiles/Forest/GiantShroomStem";

    public override void SetStaticDefaults() {
        Main.tileSolid[Type] = false;
        Main.tileMergeDirt[Type] = false;
        Main.tileBlockLight[Type] = false;
        Main.tileFrameImportant[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 2, 0);
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.addTile(Type);
    }

    //lol sorry for all the magic numbers
    public override void NearbyEffects(int i, int j, bool closer) {
        var tile = Main.tile[i, j];

        if(tile.TileFrameX == 0 && tile.TileFrameY == 0) {
            bool capExists = Main.projectile.Any(
                p =>
                    p.active &&
                    p.type == ModContent.ProjectileType<GiantShroomCapDummy>() &&
                    (p.ModProjectile as GiantShroomCapDummy)?.ParentPosition ==
                    new Point(i, j)
            );

            if (!capExists) {
                int projIndex = Projectile.NewProjectile(
                    new EntitySource_TileUpdate(i, j),
                    new Vector2(i * 16 + 15, j * 16 - 14),
                    Vector2.Zero,
                    ModContent.ProjectileType<GiantShroomCapDummy>(),
                    0,
                    0f,
                    Main.myPlayer
                );

                if (Main.projectile[projIndex].ModProjectile is GiantShroomCapDummy capDummy) {
                    capDummy.ParentPosition = new Point(i, j);
                }
            }   
        }
    }
    
    internal sealed class GiantShroomCapDummy : ModProjectile {
        public Point ParentPosition { get; set; }

        public override string Texture => "AllBeginningsMod/Content/Tiles/Forest/GiantShroomCap";
        
        private int _squishTimer = 0;

        public override void SetDefaults() {
            Projectile.width = 70;
            Projectile.height = 40;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 10;
        }

        public override void AI() {
            if (_squishTimer > 0) {
                _squishTimer--;
            }
            
            Tile parentTile = Main.tile[ParentPosition.X, ParentPosition.Y];
            if (parentTile.HasTile && parentTile.TileType == ModContent.TileType<GiantShroom>()) {
                Projectile.timeLeft = 10;
            } else {
                Projectile.Kill();
            }
            
            if (Main.LocalPlayer.velocity.Y > 0  && Main.LocalPlayer.Hitbox.Intersects(Projectile.Hitbox)) {
                if (Main.LocalPlayer.velocity.Y > -10)
                    Main.LocalPlayer.velocity.Y = -10;
                
                if (Main.LocalPlayer.velocity.X != 0) {
                    Main.LocalPlayer.velocity.X +=
                        (Main.LocalPlayer.velocity.X > 0 ? 1 : -1) *
                        3f;
                }
                
                _squishTimer = 20;
            }
        }

        public override bool PreDraw(ref Color lightColor) {
            var tex = TextureAssets.Projectile[Projectile.type].Value;
            
            Vector2 scale = Vector2.One;
            if (_squishTimer > 0) {
                // This creates a smooth "bloop" effect using a sine wave.
                // It goes from 0 -> 1 -> 0 over the course of the animation.
                float progress = 1f - (float)_squishTimer / 20;
                float sinWave = MathF.Sin(progress * MathHelper.Pi);

                // Apply the squish: Y gets smaller, X gets bigger
                scale.Y = 1f - sinWave * 0.3f;
                scale.X = 1f + sinWave * 0.3f;
            }
            
            Vector2 origin = new Vector2(tex.Width / 2f, tex.Height);
            
            Main.EntitySpriteDraw(
                tex, 
                Projectile.position + new Vector2(36, 40) - Main.screenPosition, 
                null, 
                Color.White, 
                0.0f, 
                origin, 
                scale, 
                SpriteEffects.None);
            
            return false;
        }
    }
}
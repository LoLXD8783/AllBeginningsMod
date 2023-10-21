using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.NPCs.Enemies
{
    internal class SunVampireExplosion : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_0";

        private Effect effect;
        private readonly int maxTimeLeft = 40;
        public override void SetDefaults() {
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(128, 128);
            Projectile.penetrate = -1;
            Projectile.timeLeft = maxTimeLeft;
            Projectile.aiStyle = -1;
        }

        public override bool PreDraw(ref Color lightColor) {
            effect ??= Mod.Assets.Request<Effect>("Assets/Effects/VampireExplosion", AssetRequestMode.ImmediateLoad).Value;
            effect.Parameters["progress"].SetValue(1f - (float)Projectile.timeLeft / maxTimeLeft);
            
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(default, BlendState.Additive, default, default, default, effect);

            Point position = (Projectile.position - Main.screenPosition).ToPoint();
            Main.spriteBatch.Draw(
                TextureAssets.MagicPixel.Value,
                new Rectangle(position.X, position.Y, Projectile.width, Projectile.height),
                Color.White
            );

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(default, BlendState.Opaque, default, default, default);

            return false;
        }
    }
}

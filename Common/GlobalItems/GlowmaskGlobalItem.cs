using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.GlobalItems
{
    /// <summary>
    /// Automatically draws a glowmask in the world for an item (name of glowmask image: "ItemName_Glow")
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal class ItemGlowmaskAttribute : Attribute {
        public string Path { get; set; } = default;
    }

    internal class GlowmaskGlobalItem : GlobalItem
    {
        public override void PostDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI) {
            ItemGlowmaskAttribute itemGlowmask;
            if (item.ModItem is null || (itemGlowmask = item.ModItem.GetType().GetCustomAttribute<ItemGlowmaskAttribute>()) is null) {
                return;
            }

            Texture2D texture = ModContent.Request<Texture2D>(itemGlowmask.Path ?? (item.ModItem.Texture + "_Glow"), AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}

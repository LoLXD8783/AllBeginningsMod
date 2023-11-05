using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Dusts
{
    internal class SmokeDust : ModDust
    {
        public override void OnSpawn(Dust dust) {
            dust.frame = new Rectangle(0, 20 * Main.rand.Next(3), 19, 20);
            dust.fadeIn = Main.rand.Next(120, 250);
            dust.rotation = Main.rand.NextFloatDirection() * 0.25f;
        }

        public override bool Update(Dust dust) {
            dust.position += dust.velocity;
            dust.velocity.Y -= 0.1f;
            dust.velocity *= 0.94f;

            dust.color = Color.Lerp(dust.color, Color.Black, 0.01f);
            dust.alpha += 4;
            dust.scale += 0.005f;
            if (dust.alpha >= 255) {
                dust.active = false;
            }
            return false;
        }
    }
}

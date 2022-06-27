using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Dusts
{
    public sealed class DarkBombshellSparkDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
            
            dust.frame = new Rectangle(0, Main.rand.Next(3) * 6, 6, 6);
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.velocity *= 0.9f;
            
            dust.color = Color.Lerp(new Color(127, 203, 192), Color.Black, dust.alpha / 255f);
            dust.alpha += 5;

            if (dust.alpha >= 255)
            {
                dust.active = false;
            }

            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor) => dust.color * (1f - (dust.alpha / 255f));
    }
}
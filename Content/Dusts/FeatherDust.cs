using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Dusts;

public sealed class FeatherDust : ModDust
{
    private const int FrameHeight = 14;

    public override void OnSpawn(Dust dust) {
        dust.noLight = true;
        dust.noGravity = true;

        dust.frame = new Rectangle(0, 0, 20, FrameHeight);
        dust.alpha = Main.rand.Next(50, 75);
    }

    public override bool Update(Dust dust) {
        dust.position += dust.velocity;
        dust.rotation = dust.velocity.ToRotation();

        dust.velocity.Y += 0.025f;

        if (dust.velocity.Y > 2f)
            dust.velocity.Y = 2f;

        dust.alpha += 1;
        dust.scale = 1f - dust.alpha / 255f;

        if (dust.alpha > 255 || dust.scale <= 0f)
            dust.active = false;

        AnimateDust(dust);

        return false;
    }

    private static void AnimateDust(Dust dust) {
        if (dust.customData is not TimeData data)
            dust.customData = data = new TimeData();

        data.Time++;

        if (data.Time > 5) {
            dust.frame.Y += FrameHeight;
            data.Time = 0;
        }

        if (dust.frame.Y > FrameHeight * 3)
            dust.frame.Y = 0;
    }

    private sealed class TimeData
    {
        public int Time;
    }
}
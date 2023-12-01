using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items
{
    internal class PlasmicRepeaterLaserProjectile : ModProjectile
    {
        public override string Texture => "AllBeginningsMod/Assets/Images/Sample/Noise2";
        public override void SetDefaults() {
            Projectile.width = 0;
            Projectile.height = 0;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.friendly = false;
            Projectile.hostile = false;
        }

        public override bool PreDraw(ref Color lightColor) {
            return false;
        }
    }
}

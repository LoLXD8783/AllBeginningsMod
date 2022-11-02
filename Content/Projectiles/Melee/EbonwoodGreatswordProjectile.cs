using System;
using AllBeginningsMod.Common.Bases;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Projectiles.Melee;

public sealed class EbonwoodGreatswordProjectile : GreatswordProjectileBase
{
    public override void SetDefaults() {
        Projectile.width = 32;
        Projectile.height = 32;
        
        base.SetDefaults();
    }
}
using AllBeginningsMod.Common.Bases.Items;
using AllBeginningsMod.Content.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Melee;

public sealed class ArcticClaymoreItem : ModItemBase
{
    public override void SetStaticDefaults() {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.damage = 15;
        Item.DamageType = DamageClass.Melee;

        Item.autoReuse = true;
        Item.knockBack = 2f;

        Item.width = 50;
        Item.height = 50;

        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(gold: 1, silver: 50);
    }
    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit) {
        for (int i = 0; i < 3; i++) {
            Projectile.NewProjectile(Item.GetSource_FromThis(), target.Center, new Vector2(Main.rand.Next(5, 9) * player.direction, Main.rand.Next(-3, 3)), ModContent.ProjectileType<ArcticClaymoreProjectile>(), damage, knockBack, player.whoAmI);
        }
    }
}
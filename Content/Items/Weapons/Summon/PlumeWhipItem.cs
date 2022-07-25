using AllBeginningsMod.Content.Projectiles.Summon.Whips;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Summon;

public sealed class PlumeWhipItem : ModItem
{
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("Plume Whip");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.noMelee = true;
        Item.autoReuse = false;
        Item.noUseGraphic = true;

        Item.width = 36;
        Item.height = 48;

        Item.DamageType = DamageClass.SummonMeleeSpeed;
        Item.damage = 10;
        Item.knockBack = 0.5f;

        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shoot = ModContent.ProjectileType<PlumeWhipProjectile>();
        Item.shootSpeed = 3f;

        Item.UseSound = SoundID.Item152;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(silver: 20);
    }
}
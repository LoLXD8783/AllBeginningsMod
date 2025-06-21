using AllBeginningsMod.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Items.Weapons.Magic; 

internal class MistaBombItem : ModItem {
    public override string Texture => Assets.Assets.Textures.Items.Weapons.Magic.KEY_MistaBombItem;
    
    public override void SetDefaults() {
        Item.damage = 99;
        Item.DamageType = DamageClass.Magic;
        Item.width = 0;
        Item.height = 0;
        Item.useTime = Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.shoot = ModContent.ProjectileType<MistaBombProjectile>();
        Item.shootSpeed = 15f;
        Item.knockBack = 5;
        Item.value = 10000;
        Item.autoReuse = true;
        Item.noUseGraphic = true;
        Item.noMelee = true;
    }

    public override bool CanUseItem(Player player) {
        return player.ownedProjectileCounts[Item.shoot] == 0;
    }

    public override void AddRecipes() {
        CreateRecipe()
            .AddIngredient<ExothermicSoulItem>(20)
            .AddIngredient(ItemID.Bomb, 30)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
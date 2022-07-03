using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace AllBeginningsMod.Content.Items.Weapons.Summon
{
    public sealed class TimberWhipItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 46;
            Item.damage = 8;
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.knockBack = 2f;
            Item.useTime = Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 20);
            Item.DefaultToWhip(ModContent.ProjectileType<Projectiles.Summon.TimberWhipProjectile>(), 8, 2, 3, 25);
            //Tip: Shoot speed affects the range of the whip
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 12);
            recipe.AddIngredient(ItemID.Rope, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ParryingDaggers.Items
{
	public class FlimsyDagger : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flimsy Parrying Dagger");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 0, 1, 0);
			Item.rare = ItemRarityID.Blue;
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(Mod, "ParryingDaggers", string.Format("Press {0} to parry enemies and reflect incoming projectiles\nEnemies hit by reflected projectiles drop hearts\nYou can also reflect certain friendly projectiles to boost their velocity\nWorks in your inventory as long as it is favorited\n'Good enough'", ParryingDaggers.ParryHotkey.GetAssignedKeys().Count > 0 ? ParryingDaggers.ParryHotkey.GetAssignedKeys()[0] : "[Unbounded Hotkey]"));
            tooltips.Add(line);
        }
        public override void UpdateInventory(Player player)
        {
            if(Item.favorited && player == Main.player[Main.myPlayer])
            {
                player.GetModPlayer<DaggerPlayer>().DaggerID = 1;
                player.GetModPlayer<DaggerPlayer>().Dagger = Item;
            }
        }
        public override void AddRecipes() 
        {
            CreateRecipe()
            .AddRecipeGroup(RecipeGroupID.Wood, 8)
            .AddRecipeGroup(RecipeGroupID.IronBar, 5)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ParryingDaggers.Items
{
	public class Backhander : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 24;
			Item.value = Item.sellPrice(0, 4, 0, 0);
			Item.rare = ItemRarityID.Pink;
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(Mod, "ParryingDaggers", string.Format("Press {0} to reflect friendly projectiles\nCannot parry enemies or reflect enemy projectiles\nReflected projectiles gain 20% increased damage and greatly increased velocity\nConverts reflected musket balls into explosive bullets\nWorks in your inventory as long as it is favorited", ParryingDaggers.ParryHotkey.GetAssignedKeys().Count > 0 ? ParryingDaggers.ParryHotkey.GetAssignedKeys()[0] : "[Unbounded Hotkey]"));
            tooltips.Add(line);
        }
        public override void UpdateInventory(Player player)
        {
            if(Item.favorited && player == Main.player[Main.myPlayer])
            {
                player.GetModPlayer<DaggerPlayer>().DaggerID = 10;
                player.GetModPlayer<DaggerPlayer>().Dagger = Item;
            }
        }
        public override void AddRecipes() 
        {
            CreateRecipe()
            .AddRecipeGroup(RecipeGroupID.IronBar, 5)
            .AddRecipeGroup("AdamantiteGroup", 8)
            .AddIngredient(ItemID.SoulofNight, 8)
            .AddIngredient(ItemID.SoulofMight, 20)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
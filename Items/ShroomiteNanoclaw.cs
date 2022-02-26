using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ParryingDaggers.Items
{
	public class ShroomiteNanoclaw : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 22;
			Item.value = Item.sellPrice(0, 7, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(Mod, "ParryingDaggers", string.Format("Press {0} to parry enemies and reflect incoming projectiles\nEnemies hit by reflected projectiles drop hearts\nYou can also reflect certain friendly projectiles to boost their velocity\nWorks in your inventory as long as it is favorited\nReflected projectiles are launched back at a tremendous velocity and gain 10% increased damage", ParryingDaggers.ParryHotkey.GetAssignedKeys().Count > 0 ? ParryingDaggers.ParryHotkey.GetAssignedKeys()[0] : "[Unbounded Hotkey]"));
            tooltips.Add(line);
        }
        public override void UpdateInventory(Player player)
        {
            if(Item.favorited && player == Main.player[Main.myPlayer])
            {
                player.GetModPlayer<DaggerPlayer>().DaggerID = 3;
                player.GetModPlayer<DaggerPlayer>().Dagger = Item;
            }
        }
        public override void AddRecipes() 
        {
            CreateRecipe()
            .AddIngredient(ItemID.ShroomiteBar, 9)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ParryingDaggers.Items
{
	public class ParryingRuler : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 26;
			Item.value = Item.sellPrice(0, 0, 2, 0);
			Item.rare = ItemRarityID.Blue;
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(Mod, "ParryingDaggers", string.Format("Press {0} to parry enemies and reflect incoming projectiles\nEnemies hit by reflected projectiles drop hearts\nYou can also reflect certain friendly projectiles to boost their velocity\nWorks in your inventory as long as it is favorited\nReflected enemy projectiles gain bonus damage depending on how far they travel\n'A ruler sharpened like a shiv; somewhow it works just as well as any other parrying device'", ParryingDaggers.ParryHotkey.GetAssignedKeys().Count > 0 ? ParryingDaggers.ParryHotkey.GetAssignedKeys()[0] : "[Unbounded Hotkey]"));
            tooltips.Add(line);
        }
        public override void UpdateInventory(Player player)
        {
            if(Item.favorited && player == Main.player[Main.myPlayer])
            {
                player.GetModPlayer<DaggerPlayer>().DaggerID = 2;
                player.GetModPlayer<DaggerPlayer>().Dagger = Item;
            }
        }
        public override void AddRecipes() 
        {
            CreateRecipe()
            .AddIngredient(ItemID.Ruler)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
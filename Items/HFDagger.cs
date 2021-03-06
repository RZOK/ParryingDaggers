using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ParryingDaggers.Items
{
	public class HFDagger : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("HF Dagger");
        }
        public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.value = Item.buyPrice(0, 20, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(Mod, "ParryingDaggers", string.Format("Press {0} to parry enemies and reflect incoming projectiles\nYou can also reflect certain friendly projectiles to boost their velocity\nWorks in your inventory as long as it is favorited\nAttempting to reflect a hostile projectile will instead absorb it, causing it to disappear and heal you based on its damage\n'Strange tech containing traces of nanomachines'", ParryingDaggers.ParryHotkey.GetAssignedKeys().Count > 0 ? ParryingDaggers.ParryHotkey.GetAssignedKeys()[0] : "[Unbounded Hotkey]"));
            tooltips.Add(line);
        }
        public override void UpdateInventory(Player player)
        {
            if(Item.favorited && player == Main.player[Main.myPlayer])
            {
                player.GetModPlayer<DaggerPlayer>().DaggerID = 5;
                player.GetModPlayer<DaggerPlayer>().Dagger = Item;
            }
        }
    }
}
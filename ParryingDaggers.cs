using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ParryingDaggers
{
	public class ParryingDaggers : Mod
	{
		public static ModKeybind ParryHotkey;
        public override void Load()
        {
            ParryHotkey = KeybindLoader.RegisterKeybind(this, "Parry", "F");
        }
        public override void Unload()
        {
            ParryHotkey = null;
        }
        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => "Any Adamantite Bar", new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar,
            });
            RecipeGroup.RegisterGroup("AdamantiteGroup", group);
        }
    }
}
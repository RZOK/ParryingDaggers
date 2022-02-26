using Terraria.ID;
using Terraria;
using Terraria.ModLoader;


namespace ParryingDaggers
{
    public class Cooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Parry Cooldown");
            Description.SetDefault("You cannot swing your parrying dagger");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }
    }
}
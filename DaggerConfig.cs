using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ParryingDaggers
{
    public class DaggerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(1)]
        [Range(0f, 5f)]
        [Increment(0.05f)]
        [Label("Screenshake Duration Multiplier")]
        [Tooltip("Changes the multiplier of how long projectile-reflecting screenshake lasts is. 1 is the standard amount, 0.5 is half as long, 2 is twice as long, and 0 disables screenshake entirely.")]
        public float ScreenshakeDurationMult;

        [DefaultValue(1)]
        [Range(0f, 5f)]
        [Increment(0.05f)]
        [Label("Screenshake Intensity Multiplier")]
        [Tooltip("Changes the multiplier of how intense the projectile-reflecting screenshake is. 1 is the standard amount, 0.5 is half as intense, 2 is twice as intense, and 0 disables screenshake entirely.")]
        public float ScreenshakeIntensityMult;
    }
}
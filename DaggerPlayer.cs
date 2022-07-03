using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameInput;
using Microsoft.Xna.Framework;
using ParryingDaggers.Projectiles;
using Terraria.Audio;
using Terraria.DataStructures;
using ParryingDaggers;

namespace ParryingDaggers
{
    public class DaggerPlayer : ModPlayer 
    {
        public int DaggerID = 0;
        public float screenShake = 0;
        public Item Dagger;
        public override void UpdateDead()
        {
            DaggerID = 0;
        }
        public override void ResetEffects()
        {
            DaggerID = 0;
        }
        public override void ModifyScreenPosition()
        {
            if (screenShake < 0) screenShake = 0;
            if (ModContent.GetInstance<DaggerConfig>().ScreenshakeDurationMult != 0 && ModContent.GetInstance<DaggerConfig>().ScreenshakeIntensityMult != 0)
            {
                if (screenShake > 0)
                {
                    Main.screenPosition.X += Main.rand.Next((int)-screenShake, (int)screenShake) * ModContent.GetInstance<DaggerConfig>().ScreenshakeIntensityMult;
                    Main.screenPosition.Y += Main.rand.Next((int)-screenShake, (int)screenShake) * ModContent.GetInstance<DaggerConfig>().ScreenshakeIntensityMult;
                    screenShake -= 1 / ModContent.GetInstance<DaggerConfig>().ScreenshakeDurationMult;
                }
            }
            else screenShake = 0;
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if(DaggerID > 0)
            {
                Player.AddBuff(ModContent.BuffType<Cooldown>(), 119);
            }
            return true;
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (ParryingDaggers.ParryHotkey.JustPressed)
            {
                if (DaggerID > 0 && Player == Main.player[Main.myPlayer] && !Player.HasBuff<Cooldown>())
                {
                    Player.AddBuff(ModContent.BuffType<Cooldown>(), 599);
                    SoundEngine.PlaySound(new SoundStyle($"{nameof(ParryingDaggers)}/Sounds/Item/Swing"), Player.Center);
                    Vector2 speed = Main.MouseWorld - Player.position;
                    speed.Normalize();
                    speed *= 15;
                    Vector2 vector = Player.RotatedRelativePoint(Player.MountedCenter);
                    Vector2 vector28 = vector + Utils.RandomVector2(Main.rand, -20f, 20f);
                    Vector2 vector27 = Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 14 * (0.6f + Main.rand.NextFloat() * 0.8f);
                    Projectile.NewProjectile(Player.GetSource_Misc("ParryingDagger"), Player.position, vector27, ModContent.ProjectileType<BaseDagger>(), 8, 0.5f, Player.whoAmI, 0, DaggerID);
                }
            }
        }
    }
}

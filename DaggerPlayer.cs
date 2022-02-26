using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameInput;
using Microsoft.Xna.Framework;
using ParryingDaggers.Projectiles;
using Terraria.Audio;
using Terraria.DataStructures;

namespace ParryingDaggers
{
    public class DaggerPlayer : ModPlayer 
    {
        public int DaggerID = 0;
        public Item Dagger;
        public override void UpdateDead()
        {
            DaggerID = 0;
        }
        public override void ResetEffects()
        {
            DaggerID = 0;
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
                    SoundEngine.PlaySound(SoundID.Item1, Player.position);
                    Vector2 speed = Main.MouseWorld - Player.position;
                    speed.Normalize();
                    speed *= 15;
                    Vector2 vector = Player.RotatedRelativePoint(Player.MountedCenter);
                    Vector2 vector28 = vector + Utils.RandomVector2(Main.rand, -20f, 20f);
                    Vector2 vector27 = Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 14 * (0.6f + Main.rand.NextFloat() * 0.8f);
                    Projectile.NewProjectile(Player.GetProjectileSource_Item(Dagger), Player.position, vector27, ModContent.ProjectileType<BaseDagger>(), 8, 0.5f, Player.whoAmI, 0, DaggerID);
                }
            }
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ParryingDaggers
{
    public class DaggerProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public bool DropHeart = false;
        public bool parried = false;
        public bool reflected = false;
        public bool reflectedFriendly = false;

        public bool Ruler = false;

        public override void AI(Projectile projectile)
        {
            if (reflected || reflectedFriendly)
            {
                if (Main.rand.NextBool(2))
                {
                    int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.MartianSaucerSpark, -projectile.velocity.X * Main.rand.NextFloat(1), -projectile.velocity.Y * Main.rand.NextFloat(1), 255);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].fadeIn = 1.2f;
                }
            }
            if (Ruler)
            {
                projectile.damage += (int)((Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) / 10);
            }
        }

        public override void Kill(Projectile projectile, int timeLeft)
        {
            if (reflected || reflectedFriendly)
            {
                SoundEngine.PlaySound(SoundID.Item89, projectile.Center);
                int num4;
                for (int num796 = 4; num796 < 20; num796 = num4 + 1)
                {
                    float num797 = projectile.oldVelocity.X * (5f / (float)num796);
                    float num798 = projectile.oldVelocity.Y * (5f / (float)num796);
                    int num799 = Dust.NewDust(new Vector2(projectile.position.X - num797, projectile.position.Y - num798), projectile.width, projectile.height, DustID.MartianSaucerSpark, projectile.oldVelocity.X / 2, projectile.oldVelocity.Y / 2, 255, default(Color), 1.8f);
                    Main.dust[num799].noGravity = true;
                    Dust dust = Main.dust[num799];
                    dust.velocity *= Main.rand.NextFloat(0.5f);
                    num4 = num796;
                }
                int num326 = 28;
                int num5;
                for (int num327 = 0; num327 < num326; num327 = num5 + 1)
                {
                    Vector2 spinningpoint2 = new Vector2(8, 8);
                    spinningpoint2 = spinningpoint2.RotatedBy((num327 - (num326 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num326) + projectile.Center;
                    Vector2 vector13 = spinningpoint2 - projectile.Center;
                    int num328 = Dust.NewDust(spinningpoint2 + vector13, 0, 0, DustID.MartianSaucerSpark, vector13.X, vector13.Y, 100, default(Color), 1.4f);
                    Main.dust[num328].noGravity = true;
                    Main.dust[num328].velocity = vector13 / 2;
                    num5 = num327;
                }
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (DropHeart)
            {
                Item.NewItem((int)target.position.X, (int)target.position.Y, target.width, target.height, ItemID.Heart, 1);
            }
        }
    }
}

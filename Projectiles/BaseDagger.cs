using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ParryingDaggers.Projectiles
{
    public class BaseDagger : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Parrying Dagger");
            Main.projFrames[Projectile.type] = 10;
        }

        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.aiStyle = 161;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 9;
        }

        public bool Cooldown = false;
        public int dustType = 222;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            float num = 0f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (Projectile.spriteDirection == -1)
            {
                num = 3.14159274f;
            }
            if (++Projectile.frame >= Main.projFrames[Projectile.type])
            {
                Projectile.frame = 0;
            }
            if (Main.myPlayer == Projectile.owner)
            {
                float scaleFactor6 = 23 * Projectile.scale;
                Vector2 vector13 = Main.MouseWorld - vector;
                vector13.Normalize();
                if (vector13.HasNaNs())
                {
                    vector13 = Vector2.UnitX * (float)player.direction;
                }
                vector13 *= scaleFactor6;
                if (vector13.X != Projectile.velocity.X || vector13.Y != Projectile.velocity.Y)
                {
                    Projectile.netUpdate = true;
                }
                Projectile.velocity = vector13;
            }
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + num;
            Projectile.spriteDirection = Projectile.direction;
            if (Projectile.owner == Main.myPlayer)
            {
                Parry((int)Projectile.ai[1]);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.ai[1] != 10)
            {
                Main.player[Projectile.owner].ClearBuff(ModContent.BuffType<Cooldown>());
                Main.player[Projectile.owner].AddBuff(ModContent.BuffType<Cooldown>(), 59);
                for (int f = 0; f < 30; f++)
                {
                    int dust = Dust.NewDust(target.position, target.width, target.height, dustType, 0, 0, 0);
                    Main.dust[dust].scale = 0.5f;
                    Main.dust[dust].noGravity = true;
                }
                PlaySounds(Projectile.Center);
                Cooldown = true;
            }
        }

        public List<int> AiList = new List<int>
        {
            1,
            2,
            3,
            8,
            18,
            16,
            21,
            23,
            24,
            25,
            27,
            28,
            29,
            32,
            33,
            34,
            36,
            40,
            41,
            43,
            44,
            45,
            46,
            47,
            48,
            49,
            50,
            51,
            55,
            56,
            57,
            58,
            60,
            65,
            68,
            70,
            71,
            72,
            74,
            75,
            80,
            81,
            82,
            83,
            88,
            91,
            92,
            93,
            95,
            96,
            102,
            105,
            106,
            107,
            108,
            109,
            112,
            113,
            115,
            116,
            118,
            119,
            126,
            129,
            131,
            143,
            144,
            147,
            151,
            179,
            181
        };

        public void Parry(int id)
        {
            Rectangle hitbox = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
            switch(id)
            {
                case 3: //Shroomite Nanoclaw
                    dustType = DustID.FireworkFountain_Blue;
                    break;
                case 10: //Backhander
                    dustType = DustID.FireworkFountain_Red;
                    break;
            }
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile reflProjectile = Main.projectile[i];
                if (hitbox.Intersects(reflProjectile.getRect()))
                {
                    if (reflProjectile.active && reflProjectile.velocity != Vector2.Zero && (reflProjectile.hostile || (reflProjectile.aiStyle == 1 || reflProjectile.aiStyle == 16)) && !reflProjectile.GetGlobalProjectile<DaggerProjectile>().parried)
                    {
                        Player player = Main.player[Projectile.owner];
                        if (AiList.Contains(reflProjectile.aiStyle))
                        {
                            float damage = reflProjectile.damage;
                            int penetrate = reflProjectile.penetrate;
                            int extraUpdates = reflProjectile.extraUpdates;
                            float knockback = reflProjectile.knockBack;

                            Vector2 dir = Main.MouseWorld - reflProjectile.position;
                            dir.Normalize();
                            dir *= (Math.Abs(reflProjectile.velocity.X) + Math.Abs(reflProjectile.velocity.Y));
                            Vector2 velocity = dir;
                            if (reflProjectile.hostile && id != 10)
                            {
                                player.GetModPlayer<DaggerPlayer>().screenShake = 10;
                                PlaySounds(Projectile.Center);
                                reflProjectile.hostile = false;
                                reflProjectile.friendly = true;
                                switch (id)
                                {
                                    case 2: //Parrying Ruler
                                        damage *= 5;
                                        if (penetrate != -1) penetrate += 5;
                                        velocity *= 1.5f;
                                        extraUpdates += 1;
                                        knockback *= 2;
                                        reflProjectile.GetGlobalProjectile<DaggerProjectile>().DropHeart = true;
                                        reflProjectile.GetGlobalProjectile<DaggerProjectile>().Ruler = true;
                                        break;

                                    case 3: //Shroomite Nanoclaw
                                        damage *= 5.5f;
                                        velocity *= 1.5f;
                                        knockback *= 2;
                                        int p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), reflProjectile.position, velocity, ModContent.ProjectileType<NanoclawBeam>(), (int)damage, knockback, Main.myPlayer);
                                        Main.projectile[p].GetGlobalProjectile<DaggerProjectile>().DropHeart = true;
                                        player.GetModPlayer<DaggerPlayer>().screenShake = 15;
                                        reflProjectile.active = false;
                                        break;

                                    case 5: //HF Parrying Dagger
                                        player.statLife += reflProjectile.damage / 2;
                                        player.HealEffect(reflProjectile.damage / 2);
                                        for (int d = 0; d < 10; d++)
                                        {
                                            float num797 = reflProjectile.oldVelocity.X * (5f / (d / 2));
                                            float num798 = reflProjectile.oldVelocity.Y * (5f / (d / 2));
                                            int num799 = Dust.NewDust(new Vector2(reflProjectile.Center.X - num797, reflProjectile.Center.Y - num798), 8, 8, 92, reflProjectile.oldVelocity.X / 2, reflProjectile.oldVelocity.Y / 2, 255, default(Color), 1.8f);
                                            Main.dust[num799].noGravity = true;
                                            Dust dust = Main.dust[num799];
                                            dust.velocity *= 0.5f;
                                            num799 = Dust.NewDust(new Vector2(reflProjectile.Center.X - num797, reflProjectile.Center.Y - num798), 8, 8, 92, reflProjectile.oldVelocity.X / 2, reflProjectile.oldVelocity.Y / 2, 255, default(Color), 1.4f);
                                            dust = Main.dust[num799];
                                            dust.velocity *= 0.05f;
                                            Main.dust[num799].noGravity = true;
                                            for (int f = 0; f < 2; f++)
                                            {
                                                int num5 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 92, 0f, 0f, 200, default(Color), 0.5f);
                                                Main.dust[num5].noGravity = true;
                                                Main.dust[num5].scale = 1.3f;
                                                Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                                                vector.Normalize();
                                                vector *= (float)Main.rand.Next(50, 100) * 0.08f;
                                                Main.dust[num5].velocity = vector;
                                                vector.Normalize();
                                                vector *= 50f;
                                                Main.dust[num5].position = Projectile.Center - vector;
                                            }
                                        }
                                        reflProjectile.active = false;
                                        break;

                                    default:
                                        damage *= 5;
                                        if (penetrate != -1) penetrate += 5;
                                        velocity *= 1.5f;
                                        extraUpdates += 1;
                                        knockback *= 2;
                                        reflProjectile.GetGlobalProjectile<DaggerProjectile>().DropHeart = true;
                                        break;
                                }
                                reflProjectile.GetGlobalProjectile<DaggerProjectile>().parried = true;
                                player.ClearBuff(ModContent.BuffType<Cooldown>());
                                player.AddBuff(ModContent.BuffType<Cooldown>(), 59);
                                reflProjectile.GetGlobalProjectile<DaggerProjectile>().reflected = true;
                                reflProjectile.damage = (int)damage;
                                reflProjectile.penetrate = penetrate;
                                reflProjectile.velocity = velocity;
                                reflProjectile.extraUpdates = extraUpdates;
                                reflProjectile.knockBack = knockback;
                                reflProjectile.usesLocalNPCImmunity = true;
                                reflProjectile.localNPCHitCooldown = -1;
                                if (id != 5)
                                {
                                    for (int f = 0; f < 30; f++)
                                    {
                                        float scale = 0.5f;
                                        float vel = Main.rand.NextFloat(0.05f, 2f);
                                        if (f % 5 == 0)
                                        {
                                            scale = 1.2f;
                                            vel *= 1.2f;
                                        }
                                        Vector2 normalizedVelocity = Vector2.Normalize(reflProjectile.velocity) * 12;
                                        int dust = Dust.NewDust(Projectile.Center + Projectile.velocity, 1, 1, dustType, normalizedVelocity.X, normalizedVelocity.Y, 0);
                                        Main.dust[dust].scale = scale;
                                        Main.dust[dust].noGravity = true;
                                        Main.dust[dust].velocity *= vel;
                                    }
                                }
                                Cooldown = true;
                                player.immune = true;
                                player.immuneTime = 60;
                            }
                            else if (reflProjectile.friendly && (reflProjectile.aiStyle == 1 || reflProjectile.aiStyle == 16) && Projectile.type != ModContent.ProjectileType<NanoclawBeam>())
                            {
                                PlaySounds(Projectile.Center);
                                extraUpdates += 3;
                                velocity *= 1.25f;
                                penetrate += 2;
                                switch (id)
                                {
                                    case 3:
                                        extraUpdates += 10;
                                        damage *= 1.1f;
                                        break;

                                    case 10:
                                        damage *= 1.2f;
                                        extraUpdates += 3;
                                        if (reflProjectile.type == ProjectileID.Bullet)
                                        {
                                            int explosiveProjectile = Projectile.NewProjectile(Projectile.GetSource_Misc("ParryingDagger"), reflProjectile.Center, velocity, ProjectileID.ExplosiveBullet, (int)damage, knockback, Projectile.owner);
                                            Main.projectile[explosiveProjectile].GetGlobalProjectile<DaggerProjectile>().reflectedFriendly = true;
                                            Main.projectile[explosiveProjectile].GetGlobalProjectile<DaggerProjectile>().parried = true;
                                            Main.projectile[explosiveProjectile].extraUpdates = extraUpdates;
                                            Main.projectile[explosiveProjectile].penetrate = penetrate;
                                            reflProjectile.active = false;
                                        }
                                        break;
                                }
                                
                                player.ClearBuff(ModContent.BuffType<Cooldown>());
                                player.AddBuff(ModContent.BuffType<Cooldown>(), 59);
                                reflProjectile.GetGlobalProjectile<DaggerProjectile>().parried = true;
                                reflProjectile.GetGlobalProjectile<DaggerProjectile>().reflectedFriendly = true;
                                reflProjectile.damage = (int)damage;
                                reflProjectile.penetrate = penetrate;
                                reflProjectile.velocity = velocity;
                                reflProjectile.extraUpdates = extraUpdates;
                                reflProjectile.knockBack = knockback;
                                player.GetModPlayer<DaggerPlayer>().screenShake = 10;
                                for (int f = 0; f < 30; f++)
                                {
                                    float scale = 0.5f;
                                    float vel = Main.rand.NextFloat(0.05f, 2f);
                                    if (f % 5 == 0)
                                    {
                                        scale = 1.2f;
                                        vel *= 1.2f;
                                    }
                                    Vector2 normalizedVelocity = Vector2.Normalize(reflProjectile.velocity) * 12;
                                    int dust = Dust.NewDust(Projectile.Center + Projectile.velocity, 1, 1, dustType, normalizedVelocity.X, normalizedVelocity.Y, 0);
                                    Main.dust[dust].scale = scale;
                                    Main.dust[dust].noGravity = true;
                                    Main.dust[dust].velocity *= vel;
                                }
                                Cooldown = true;
                            }
                        }
                        if (reflProjectile.type == 385) //sharknado bolt
                        {
                            int num388 = 36;
                            int num9;
                            for (int num389 = 0; num389 < num388; num389 = num9 + 1)
                            {
                                Vector2 vector25 = Vector2.Normalize(reflProjectile.velocity) * new Vector2((float)reflProjectile.width / 2f, (float)reflProjectile.height) * 0.75f;
                                vector25 = vector25.RotatedBy((double)((float)(num389 - (num388 / 2 - 1)) * 6.2831855f / (float)num388), default(Vector2)) + reflProjectile.Center;
                                Vector2 vector26 = vector25 - reflProjectile.Center;
                                int num390 = Dust.NewDust(vector25 + vector26, 0, 0, 172, vector26.X * 2f, vector26.Y * 2f, 100, default(Color), 1.4f);
                                Main.dust[num390].noGravity = true;
                                Main.dust[num390].noLight = true;
                                Main.dust[num390].velocity = vector26;
                                num9 = num389;
                            }
                            reflProjectile.active = false;

                        }
                        else if (reflProjectile.hostile)
                        {
                            player.immune = true;
                            player.immuneTime = 60;
                        }
                    }
                }
            }
        }
        public void PlaySounds(Vector2 pos)
        {
            SoundEngine.PlaySound(new SoundStyle($"{nameof(ParryingDaggers)}/Sounds/Item/Parry"), Projectile.Center);
        }
    }
}
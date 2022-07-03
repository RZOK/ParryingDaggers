using Microsoft.Xna.Framework;
using ParryingDaggers.Items;
using ParryingDaggers.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ParryingDaggers
{
    public class DaggerNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool Swordbroken = false;

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            var source = projectile.GetSource_Misc("ParryingDagger");
            if (projectile.GetGlobalProjectile<DaggerProjectile>().reflected)
            {
                crit = true;
            }
            if (projectile.type == ModContent.ProjectileType<BaseDagger>() && npc.type != NPCID.TargetDummy && projectile.ai[1] != 10)
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(ParryingDaggers)}/Sounds/Item/Parry"), projectile.Center);
                switch (projectile.ai[1])
                {
                    case 4:
                        if (!Swordbroken)
                        {
                            Swordbroken = true;
                            if (!npc.boss) npc.damage /= 2;
                            else npc.damage /= 3 * 2;
                        }
                        break;
                }

                switch (npc.type)
                {
                    case NPCID.SolarCrawltipedeTail:
                        damage = Main.rand.Next(500000, 5000000);
                        crit = true;
                        SoundEngine.PlaySound(SoundID.NPCDeath60, projectile.Center);
                        break;

                    case NPCID.Creeper or NPCID.ServantofCthulhu:
                        damage = Main.rand.Next(140, 200);
                        crit = true;
                        break;

                    case NPCID.MothronEgg:
                        damage = Main.rand.Next(140, 300);
                        crit = true;
                        Item.NewItem(source, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Heart, 1);
                        break;

                    case NPCID.TheHungry:
                        damage = Main.rand.Next(600, 900);
                        crit = true;
                        Item.NewItem(source, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Heart, 1);
                        break;

                    case NPCID.Snatcher or NPCID.ManEater or NPCID.AngryTrapper or NPCID.FungiBulb or NPCID.GiantFungiBulb or NPCID.Clinger:
                        damage = Main.rand.Next(80, 100);
                        crit = true;
                        break;
                }

                Main.player[projectile.owner].immune = true;
                Main.player[projectile.owner].immuneTime = 20;
            }
            if (((projectile.type == ProjectileID.PinkLaser && projectile.GetGlobalProjectile<DaggerProjectile>().reflected) || projectile.type == ModContent.ProjectileType<BaseDagger>()) && npc.type == NPCID.Probe)
            {
                damage = Main.rand.Next(350, 500);
                crit = true;
                Projectile.NewProjectile(npc.GetSource_Death(), npc.Center, Vector2.Zero, ModContent.ProjectileType<ProbeExplosion>(), 200, 11, projectile.owner);
                SoundEngine.PlaySound(SoundID.Item62, npc.position);
                ref float y = ref npc.position.X;
                y += npc.width / 2;
                y = ref npc.position.Y;
                y += npc.height / 2;
                npc.width = 22;
                npc.height = 22;
                y = ref npc.position.X;
                y -= npc.width / 2;
                y = ref npc.position.Y;
                y -= npc.height / 2;
                int num4;
                for (int num725 = 0; num725 < 40; num725 = num4 + 1)
                {
                    int num726 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 127, Main.rand.NextFloat(-40, 41), Main.rand.NextFloat(-40, 41), 100, default(Color), 2.5f);
                    Dust dust210 = Main.dust[num726];
                    Dust dust2 = dust210;
                    dust2.noGravity = true;
                    dust2.velocity *= 2.4f;
                    num4 = num725;
                }
                for (int num727 = 0; num727 < 40; num727 = num4 + 1)
                {
                    int num728 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 127, Main.rand.NextFloat(-40, 41), Main.rand.NextFloat(-40, 41), 100, default(Color), 3.5f);
                    Main.dust[num728].noGravity = true;
                    Dust dust211 = Main.dust[num728];
                    Dust dust2 = dust211;
                    num728 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 127, 0f, 0f, 100, default(Color), 1.5f);
                    dust211 = Main.dust[num728];
                    dust2 = dust211;
                    dust2.noGravity = true;
                    num4 = num727;
                }
                for (int num729 = 0; num729 < 8; num729 = num4 + 1)
                {
                    float num730 = 0.4f;
                    if (num729 == 1)
                    {
                        num730 = 0.8f;
                    }
                    int num731 = Gore.NewGore(npc.GetSource_Death(), new Vector2(npc.position.X, npc.position.Y), new Vector2(Main.rand.NextFloat(-15, 16), Main.rand.NextFloat(-15, 16)), Main.rand.Next(61, 64));
                    Gore gore32 = Main.gore[num731];
                    Gore gore2 = gore32;
                    gore2.velocity *= num730;
                    y = ref Main.gore[num731].velocity.X;
                    y += 1f;
                    y = ref Main.gore[num731].velocity.Y;
                    y += 1f;
                    num731 = Gore.NewGore(npc.GetSource_Death(), new Vector2(npc.position.X, npc.position.Y), new Vector2(Main.rand.NextFloat(-15, 16), Main.rand.NextFloat(-20, 21)), Main.rand.Next(61, 64), 1.5f);
                    gore32 = Main.gore[num731];
                    gore2 = gore32;
                    gore2.velocity *= num730;
                    y = ref Main.gore[num731].velocity.X;
                    y -= 1f;
                    y = ref Main.gore[num731].velocity.Y;
                    y += 1f;
                    num731 = Gore.NewGore(npc.GetSource_Death(), new Vector2(npc.position.X, npc.position.Y), new Vector2(Main.rand.NextFloat(-15, 16), Main.rand.NextFloat(-15, 16)), Main.rand.Next(61, 64), 1.25f);
                    gore32 = Main.gore[num731];
                    gore2 = gore32;
                    gore2.velocity *= num730;
                    y = ref Main.gore[num731].velocity.X;
                    y += 1f;
                    y = ref Main.gore[num731].velocity.Y;
                    y -= 1f;
                    num731 = Gore.NewGore(npc.GetSource_Death(), new Vector2(npc.position.X, npc.position.Y), new Vector2(Main.rand.NextFloat(-15, 16), Main.rand.NextFloat(-15, 16)), Main.rand.Next(61, 64));
                    gore32 = Main.gore[num731];
                    gore2 = gore32;
                    gore2.velocity *= num730;
                    y = ref Main.gore[num731].velocity.X;
                    y -= 1f;
                    y = ref Main.gore[num731].velocity.Y;
                    y -= 1f;
                    num4 = num729;
                }
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (Swordbroken)
            {
                drawColor = Color.Maroon;
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.AngryBones)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Swordbreaker>(), 100, 50));
            }
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Cyborg)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<HFDagger>());
                nextSlot++;
            }
        }
    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ParryingDaggers.Projectiles
{
    public class NanoclawBeam : ModProjectile
    {
        public override string Texture => "ParryingDaggers/Projectiles/Blank";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nanoclaw Burst");
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;      
            Projectile.height = 8; 
            Projectile.friendly = true;      
            Projectile.tileCollide = true;  
            Projectile.penetrate = -1;     
            Projectile.timeLeft = 600;   
            Projectile.extraUpdates = 20;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            float scale = 0.6f;
            if (Main.rand.NextBool(5)) scale *= 2;
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.FireworkFountain_Blue, -Projectile.velocity.X * Main.rand.NextFloat(0.5f), -Projectile.velocity.Y * Main.rand.NextFloat(0.5f), 255);
            Main.dust[d].noGravity = true;
            Main.dust[d].scale = scale;
        }
    }
}
using Terraria.ModLoader;


namespace ParryingDaggers.Projectiles
{
    public class ProbeExplosion : ModProjectile
    {
        public override string Texture => "ParryingDaggers/Projectiles/Blank";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Probe Explosion");
        }

        public override void SetDefaults()
        {
            Projectile.width = 300;      
            Projectile.height = 300; 
            Projectile.friendly = true;    
            Projectile.tileCollide = false; 
            Projectile.penetrate = -1;     
            Projectile.timeLeft = 6;   
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
    }
}
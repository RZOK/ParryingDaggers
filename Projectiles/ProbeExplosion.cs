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
            Projectile.width = 300;      //Projectile width
            Projectile.height = 300;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you         //
            Projectile.tileCollide = false;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many npc will penetrate
            Projectile.timeLeft = 6;   //how many time this Projectile has before disepire   // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
    }
}
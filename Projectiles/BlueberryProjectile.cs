using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ArknightsWeapons.Projectiles
{
    public class BlueberryProjectile : ModProjectile
    {
        //public int Timer;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 10;
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 150;
            Projectile.height = 150;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            Projectile.ArmorPenetration = 20;

        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner]; 
            float num = 1.57079637f; 
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true); 




            Projectile.velocity *= 0.98f;

            if (++Projectile.frameCounter >= 2)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
            }
            
         
            if(Projectile.frame >= 9)
            {
                Projectile.Kill();
            }


            if (Projectile.frame <= 0)
            {
                Vector2 posVector = Main.MouseWorld - vector;
                posVector.Normalize();
                Vector2 offset = new Vector2(40, 40);
                posVector = Vector2.Multiply(posVector, offset);
                Vector2 projectileCenter = Vector2.Add(player.MountedCenter, posVector);

                Projectile.position = player.RotatedRelativePoint(projectileCenter, true) - Projectile.Size / 2f;
            }

            Projectile.direction = (Projectile.spriteDirection = ((Projectile.velocity.X > 0f) ? 1 : -1));
            Projectile.rotation = Projectile.velocity.ToRotation();



            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }


            if (Projectile.spriteDirection == -1)
                Projectile.rotation += MathHelper.Pi;




            if (Main.myPlayer == Projectile.owner) // Check if the local player is the owner of this projectile, if it is we update the rest.
            {
                                                                                                          // Otherwise we KILL it (mwuahahaha). So basically destroy this projectile if the item is not being used.
                    float scaleFactor6 = 1f; //
                    if (player.inventory[player.selectedItem].shoot == Projectile.type) // Check if the item that is currently selected in the players' inventory is the one that is
                    {                                                                                                  // shooting this Projectile.
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale; // Set the 'scaleFactor6' value
                    }
                    Vector2 vector13 = Main.MouseWorld - vector; // Get the direction vector between the mouse position and the vector (vector was declared earlier, remember?)
                    vector13.Normalize(); // Normalize this vector since we're not in need of any large values, we just need to get the direction out of this.
                    if (vector13.HasNaNs()) // This check is basically used to check if the X value of the direction is 'Not a Number' (or a negative value in this case).
                    {
                        vector13 = Vector2.UnitX * (float)player.direction; // If it is, we
                    }
                    vector13 *= scaleFactor6;
                    if (vector13.X != Projectile.velocity.X || vector13.Y != Projectile.velocity.Y) // If the vector13 value is actually changing something
                    {                                                                                                        // (so if the mouse or the player have been moved).
                        Projectile.netUpdate = true; // Make sure it gets updated for everyone if you're in multiplayer.
                    }
            }


            Lighting.AddLight(Projectile.Center, 0.5f, 0.5f, 1f);
            

            Vector2 vector14 = Projectile.Center + Projectile.velocity * 0.1f; 

            int bbDust = Dust.NewDust(vector14 - Projectile.Size / 2f, Projectile.width, Projectile.height, 206, 0f, 0f, 150, default(Color), 0.9f);
               
            Main.dust[bbDust].noGravity = true;



        }



        // Some advanced drawing because the texture image isn't centered or symetrical.
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            origin.X = (float)((Projectile.spriteDirection == 1) ? (sourceRectangle.Width - 90) : 90);

            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.spriteBatch.Draw(texture,
            Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
            sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);

            return false;
        }



    }
}




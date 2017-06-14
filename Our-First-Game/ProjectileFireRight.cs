using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Our_First_Game
{
    public class ProjectileFireRight
    {
        private Texture2D rocketShot;
        public static Rectangle rocketbox1;
        public static float rocketStartX, rocketPos, rocketStartY;

        public ProjectileFireRight(Texture2D projectile, float startX, float startY)
        {
            rocketShot = projectile;
            rocketStartX = startX;
            rocketStartY = startY;
            rocketPos = 0;
        }

        private bool RocketEnd()
        {
            if (Game1.isGameActive)
            {
                rocketPos += ProjectileFireLeft.rocketSpeed;
            }
            else
            {
                rocketPos += 0;
            }
            
            if (rocketStartX + rocketPos >= 800 || !Game1.isScoAlive)
            {
                Game1.shot1 = false;
                rocketbox1 = new Rectangle();
                Game1.rocketSoundInstanceRight.Stop();
                return false;
            }
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (RocketEnd())
            {
                spriteBatch.Draw(rocketShot, new Vector2(rocketStartX + rocketPos, rocketStartY), Color.White);
                rocketbox1 = new Rectangle((int) (rocketStartX + rocketPos), (int)rocketStartY, rocketShot.Width, rocketShot.Height);
            }
        }
    }
}

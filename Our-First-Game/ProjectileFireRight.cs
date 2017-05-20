using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Our_First_Game
{
    class ProjectileFireRight
    {
        private Texture2D rocketShot;
        public static Rectangle rocketbox1;
        private float rocketStartX, rocketStartY, rocketSpeed;

        public ProjectileFireRight(Texture2D projectile, float startX, float startY)
        {
            rocketShot = projectile;
            rocketStartX = startX;
            rocketStartY = startY;
        }

        private bool rocketEnd()
        {
            if (Game1.isGameActive)
            {
                rocketSpeed += 15;
            }
            else
            {
                rocketSpeed += 0;
            }
            
            if (rocketStartX + rocketSpeed >= 800 || !Game1.isScoAlive)
            {
                Game1.shot1 = false;
                return false;
            }
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (rocketEnd())
            {
                spriteBatch.Draw(rocketShot, new Vector2(rocketStartX + rocketSpeed, rocketStartY), Color.White);
                rocketbox1 = new Rectangle((int) (rocketStartX + rocketSpeed), (int)rocketStartY, rocketShot.Width, rocketShot.Height);
            }
            else
            {
                rocketbox1 = new Rectangle();
            }
        }
    }
}

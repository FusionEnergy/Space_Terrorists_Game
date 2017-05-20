using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Our_First_Game
{
    class ProjectileFireLeft
    {
        private Texture2D rocketShot;
        public static Rectangle rocketBox2;
        private float rocketStartX, rocketStartY, rocketSpeed;

        public ProjectileFireLeft(Texture2D projectile, float startX, float startY)
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

            if (rocketStartX - rocketSpeed <= 0 - rocketShot.Width || !Game1.isCruAlive)
            {
                Game1.shot2 = false;
                return false;
            }
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (rocketEnd())
            {
                spriteBatch.Draw(rocketShot, new Vector2(rocketStartX - rocketSpeed, rocketStartY), Color.White);
                rocketBox2 = new Rectangle((int)(rocketStartX - rocketSpeed), (int)rocketStartY, rocketShot.Width, rocketShot.Height);
            }
            else
            {
                rocketBox2 = new Rectangle();
            }
        }
    }
}
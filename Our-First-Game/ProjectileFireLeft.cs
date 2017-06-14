using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Our_First_Game
{
    public class ProjectileFireLeft
    {
        private Texture2D rocketShot;
        public static Rectangle rocketBox2;
        public static float rocketStartX, rocketStartY, rocketPos;
        public const float rocketSpeed = 18.75f;

        public ProjectileFireLeft(Texture2D projectile, float startX, float startY)
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
                rocketPos += rocketSpeed;
            }
            else
            {
                rocketPos += 0;
            }

            if (rocketStartX - rocketPos <= 0 - rocketShot.Width || !Game1.isCruAlive)
            {
                Game1.shot2 = false;
                rocketBox2 = new Rectangle();
                Game1.rocketSoundInstanceLeft.Stop();
                return false;
            }
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (RocketEnd())
            {
                spriteBatch.Draw(rocketShot, new Vector2(rocketStartX - rocketPos, rocketStartY), Color.White);
                rocketBox2 = new Rectangle((int)(rocketStartX - rocketPos), (int)rocketStartY, rocketShot.Width, rocketShot.Height);
            }
        }
    }
}
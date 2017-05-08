using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Our_First_Game
{
    class ProjectileFireRight
    {
        private Texture2D rocketShot1;
        private float rocketStartX1, rocketStartY1, rocketSpeed1;

        public ProjectileFireRight(Texture2D projectile, float startX, float startY)
        {
            rocketShot1 = projectile;
            rocketStartX1 = startX;
            rocketStartY1 = startY;
        }

        private bool rocketEnd()
        {
            rocketSpeed1 += 15;
            if (rocketStartX1 + rocketSpeed1 >= 1000)
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
                spriteBatch.Draw(rocketShot1, new Vector2(rocketStartX1 + rocketSpeed1, rocketStartY1), Color.White);
            }
        }
    }
}

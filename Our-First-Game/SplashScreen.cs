using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Our_First_Game
{
    public class SplashScreen
    {
        protected Texture2D image;
        protected bool endScreen = false;

        public SplashScreen(Texture2D SplashImage)
        {
            image = SplashImage;
        }

        public void Update()
        {
            if (!endScreen)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && Game1.displayMenu.endScreen)
                {
                    endScreen = true;
                    Game1.isGameActive = true;
                    Game1.reload1 = 0;
                    Game1.reload2 = 0;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!endScreen)
            {
                Game1.isGameActive = false;
                spriteBatch.Draw(image, new Rectangle(0 ,0 , 800, 480), Color.White);
            }
        }
    }
}

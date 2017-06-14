using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Our_First_Game
{
    public class SplashScreen
    {
        private KeyboardState keyOldState, keyNewState;
        protected Texture2D image;
        public bool endScreen = false; //internal access modifier only allows access to programs in this namespace

        public SplashScreen(Texture2D SplashImage)
        {
            image = SplashImage;
        }

        public void Update()
        {
            if (!endScreen)
            {
                keyNewState = Keyboard.GetState();

                if (keyNewState.IsKeyDown(Keys.Space) && Game1.displayMenu.endScreen)
                {
                    endScreen = true;
                    Game1.isGameActive = true;
                    Game1.reload1 = Game1.reloadTimeMilliseconds - Game1.firstTimeReloadReduction;
                    Game1.reload2 = Game1.reloadTimeMilliseconds - Game1.firstTimeReloadReduction;
                }

                if (keyNewState.IsKeyDown(Keys.N) && keyOldState.IsKeyUp(Keys.N) && Game1.displayMenu.endScreen && !Game1.scoAIEnabled)
                {
                    Game1.scoAIEnabled = true;
                }
                else if (keyNewState.IsKeyDown(Keys.N) && keyOldState.IsKeyUp(Keys.N) && Game1.displayMenu.endScreen && Game1.scoAIEnabled)
                    Game1.scoAIEnabled = false;

                keyOldState = keyNewState;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (!endScreen)
            {
                Game1.isGameActive = false;
                spriteBatch.Draw(image, new Rectangle(0, 0, 800, 480), Color.White);
                
                if (Game1.scoAIEnabled)
                {
                    spriteBatch.DrawString(font, "Scorpion bot enabled!", new Vector2(473, 430), Color.Red);
                }
                else
                {
                    spriteBatch.DrawString(font, "Scorpion bot disabled.", new Vector2(473, 430), Color.MidnightBlue);
                }
            }
        }
    }
}

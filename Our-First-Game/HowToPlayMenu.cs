using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Our_First_Game
{
    public class HowToPlayMenu : SplashScreen
    {
        private static KeyboardState keyOldState1, keyNewState1, keyOldState2, keyNewState2;
        private double clickGracePeriod;
        private const double clickGraceTime = 85;

        public HowToPlayMenu(Texture2D Instructions) : base(Instructions)
        {
            image = Instructions;
            endScreen = true;
            clickGracePeriod = clickGraceTime;
        }

        public void Update(GameTime gameTime)
        {
            clickGracePeriod += gameTime.ElapsedGameTime.TotalMilliseconds;

            keyNewState1 = Keyboard.GetState();
            keyNewState2 = Keyboard.GetState();

            if (endScreen && keyNewState1.IsKeyDown(Keys.M) && keyOldState1.IsKeyUp(Keys.M) && clickGracePeriod > clickGraceTime)
            {
                endScreen = false;
                clickGracePeriod = 0;
            }
            else if (!endScreen && keyNewState2.IsKeyDown(Keys.M) && keyOldState2.IsKeyUp(Keys.M) && clickGracePeriod > clickGraceTime)
            {
                endScreen = true;
                clickGracePeriod = 0;
            }

            keyOldState1 = keyNewState1;
            keyOldState2 = keyNewState2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}

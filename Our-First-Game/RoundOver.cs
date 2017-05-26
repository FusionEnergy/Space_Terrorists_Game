using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Our_First_Game
{
    class RoundOver
    {
        public RoundOver()
        {
        }

        public async void awardPoints(int winner)
        {
            Game1.isGameActive = false;
            await Task.Delay(530);

            if (winner == 0)
            {
                Game1.score1++;
            }
            else
            {
                Game1.score2++;
            }

            if (Game1.score1 != 8 && Game1.score2 != 8)
            {
                Game1.cruXPos = 50; Game1.cruYPos = 380; Game1.scoXPos = 700; Game1.scoYPos = 80; Game1.reload1 = 0; Game1.reload2 = 0;
                Game1.isCruAlive = true; Game1.isScoAlive = true; Game1.shot1 = false; Game1.shot2 = false; Game1.cruGracePeriod = true; Game1.scoGracePeriod = true;
                Game1.isGameActive = true;
            }

            await Task.Delay(55);
        }

        public void gameOver(SpriteBatch spriteBatch, Texture2D winnerScreen)
        {
            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                spriteBatch.Draw(winnerScreen, new Rectangle(0, 0, 800, 480), Color.White);
            
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Game1.cruXPos = 50; Game1.cruYPos = 380; Game1.scoXPos = 700; Game1.scoYPos = 80; Game1.reload1 = 0; Game1.reload2 = 0;
                Game1.isCruAlive = true; Game1.isScoAlive = true; Game1.shot1 = false; Game1.shot2 = false; Game1.cruGracePeriod = true; Game1.scoGracePeriod = true;
                Game1.isGameActive = true;
                Game1.score1 = 0; Game1.score2 = 0;
            }
        }
    }
}

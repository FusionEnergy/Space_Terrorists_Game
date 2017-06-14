using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Our_First_Game
{
    class RoundOver
    {
        bool runOnceWinScreenSound = false;

        public RoundOver()
        {
        }

        public async void AwardPoints(int winner)
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

            if (Game1.score1 != Game1.scoreMax && Game1.score2 != Game1.scoreMax)
            {
                Game1.cruXPos = 50; Game1.cruYPos = 380; Game1.scoXPos = 700; Game1.scoYPos = 80; Game1.reload1 = Game1.reloadTimeMilliseconds - Game1.firstTimeReloadReduction; Game1.reload2 = Game1.reloadTimeMilliseconds - Game1.firstTimeReloadReduction;
                Game1.isCruAlive = true; Game1.isScoAlive = true; Game1.shot1 = false; Game1.shot2 = false; Game1.cruGracePeriod = true; Game1.scoGracePeriod = true;
                Game1.isGameActive = true;
                Game1.drawBackground.GetRandom();
            }

            await Task.Delay(55);
        }

        public void GameOver(SpriteBatch spriteBatch, int winner, Texture2D winnerScreen)
        {
            MediaPlayer.Pause();
            Game1.rocketSoundInstanceLeft.Stop();
            Game1.rocketSoundInstanceRight.Stop();
            Game1.explosionSoundInstance.Stop();

            if (winner == 0)
            {
                Game1.winScreenSoundInstance.Pan = -0.1f;
            }
            else
            {
                Game1.winScreenSoundInstance.Pan = 0.1f;
            }
            if (runOnceWinScreenSound == false)
            {
                Game1.winScreenSoundInstance.Play();
                runOnceWinScreenSound = true;
            }

            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                spriteBatch.Draw(winnerScreen, new Rectangle(0, 0, 800, 480), Color.White);
            
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Game1.cruXPos = 50; Game1.cruYPos = 380; Game1.scoXPos = 700; Game1.scoYPos = 80; Game1.reload1 = Game1.reloadTimeMilliseconds - Game1.firstTimeReloadReduction; Game1.reload2 = Game1.reloadTimeMilliseconds - Game1.firstTimeReloadReduction;
                Game1.isCruAlive = true; Game1.isScoAlive = true; Game1.shot1 = false; Game1.shot2 = false; Game1.cruGracePeriod = true; Game1.scoGracePeriod = true;
                Game1.isGameActive = true;
                Game1.score1 = 0; Game1.score2 = 0;
                Game1.drawBackground.GetRandom();
                Game1.winScreenSoundInstance.Stop();
            }
        }
    }
}

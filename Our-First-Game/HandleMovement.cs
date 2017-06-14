using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Our_First_Game;

namespace Space_Terrorists
{
    class HandleMovement
    {
        private static KeyboardState keyNewStateCru, keyOldStateCru, keyNewStateSco, keyOldStateSco;
        public const float speedLeftRight = 3.7f, speedForward = 3.8f, speedBackward = 3.5f;

        public HandleMovement()
        {

        }

        public void UpdateCruiserMovement(Texture2D cruiser)
        {
            keyNewStateCru = Keyboard.GetState();

            if (keyNewStateCru.IsKeyDown(Keys.W) && Game1.isCruAlive && Game1.isGameActive)
            {
                if (Game1.cruYPos >= 70)
                    Game1.cruYPos -= speedLeftRight;
            }
            if (keyNewStateCru.IsKeyDown(Keys.S) && Game1.isCruAlive && Game1.isGameActive)
            {
                if (Game1.cruYPos <= 480 - cruiser.Height)
                    Game1.cruYPos += speedLeftRight;
            }
            if (keyNewStateCru.IsKeyDown(Keys.D) && Game1.isCruAlive && Game1.isGameActive)
            {
                if (Game1.cruXPos <= 395 - cruiser.Width)
                    Game1.cruXPos += speedForward;
            }
            if (keyNewStateCru.IsKeyDown(Keys.A) && Game1.isCruAlive && Game1.isGameActive)
            {
                if (Game1.cruXPos >= 0)
                    Game1.cruXPos -= speedBackward;
            }

            keyOldStateCru = keyNewStateCru;
        }

        public void UpdateScorpionMovement(Texture2D scorpion)
        {
            if (!Game1.scoAIEnabled)
            {
                keyNewStateSco = Keyboard.GetState();

                if (keyNewStateSco.IsKeyDown(Keys.I) && Game1.isScoAlive && Game1.isGameActive)
                {
                    if (Game1.scoYPos >= 70)
                        Game1.scoYPos -= speedLeftRight;
                }
                if (keyNewStateSco.IsKeyDown(Keys.K) && Game1.isScoAlive && Game1.isGameActive)
                {
                    if (Game1.scoYPos <= 480 - scorpion.Height)
                        Game1.scoYPos += speedLeftRight;
                }
                if (keyNewStateSco.IsKeyDown(Keys.J) && Game1.isScoAlive && Game1.isGameActive)
                {
                    if (Game1.scoXPos >= 405)
                        Game1.scoXPos -= speedForward;
                }
                if (keyNewStateSco.IsKeyDown(Keys.L) && Game1.isScoAlive && Game1.isGameActive)
                {
                    if (Game1.scoXPos <= 800 - scorpion.Width)
                        Game1.scoXPos += speedBackward;
                }

                keyOldStateSco = keyNewStateSco;
            }
        }
    }
}

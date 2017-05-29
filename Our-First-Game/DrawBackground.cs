using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Our_First_Game
{
    public class DrawBackground
    {
        private Texture2D[] backgroundArray;
        private Random randBackgroundListNumber = new Random();
        private int backgroundListNumber, checkIfSame;

        public DrawBackground(Texture2D[] backgroundarray)
        {
            backgroundArray = backgroundarray;
            backgroundListNumber = randBackgroundListNumber.Next(backgroundArray.Length - 1);
        }

        public void GetRandom()
        {
            while (true)
            {
                checkIfSame = randBackgroundListNumber.Next(backgroundArray.Length - 1);
                if (backgroundListNumber != checkIfSame)
                {
                    backgroundListNumber = checkIfSame;
                    break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundArray[backgroundListNumber], new Rectangle(0, 0, 800, 480), Color.White);
        }
    }
}

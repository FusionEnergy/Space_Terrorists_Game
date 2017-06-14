using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Our_First_Game;

namespace Space_Terrorists
{
    class ScorpionAI
    {
        private Texture2D scorpion, cruiser, ownRocket, enemyRocket;
        private SoundEffectInstance rocketSoundSco;
        private GameTime gameTime;
        private Random binaryRandom;
        private bool handlingEnemyShot, initializeTimers;
        private bool[] moveOtherWay;
        private int binaryRanNum;
        private int[] timers;
        private const int millisecondRepeatTime = 2800;

        public ScorpionAI(Texture2D Scorpion, Texture2D Cruiser,Texture2D OwnRocket, Texture2D EnemyRocket, SoundEffect rocketSound)
        {
            scorpion = Scorpion;
            cruiser = Cruiser;
            ownRocket = OwnRocket;
            enemyRocket = EnemyRocket;
            rocketSoundSco = rocketSound.CreateInstance();
            rocketSoundSco.Volume = 0.1f;
            gameTime = new GameTime();
            initializeTimers = true;

            moveOtherWay = new bool[4];
            timers = new int[5];
            binaryRandom = new Random();
        }

        private bool IsGoingInsideScoTop()
        {
            if ((ProjectileFireRight.rocketStartY <= (Game1.scoYPos + (scorpion.Height / 2)) && ProjectileFireRight.rocketStartY >= Game1.scoYPos) || 
                ((ProjectileFireRight.rocketStartY + enemyRocket.Height) <= (Game1.scoYPos + (scorpion.Height / 2)) && (ProjectileFireRight.rocketStartY + enemyRocket.Height) >= Game1.scoYPos) || 
                ((ProjectileFireRight.rocketStartY + (enemyRocket.Height / 2)) <= (Game1.scoYPos + (scorpion.Height / 2)) && (ProjectileFireRight.rocketStartY + (enemyRocket.Height / 2)) >= Game1.scoYPos))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsGoingInsideScoBot()
        {
            if ((ProjectileFireRight.rocketStartY <= (Game1.scoYPos + scorpion.Height) && ProjectileFireRight.rocketStartY >= (Game1.scoYPos + (scorpion.Height / 2))) ||
                ((ProjectileFireRight.rocketStartY + enemyRocket.Height) <= (Game1.scoYPos + scorpion.Height) && (ProjectileFireRight.rocketStartY + enemyRocket.Height) >= (Game1.scoYPos + (scorpion.Height / 2))) ||
                ((ProjectileFireRight.rocketStartY + (enemyRocket.Height / 2)) <= (Game1.scoYPos + scorpion.Height / 2) && (ProjectileFireRight.rocketStartY + (enemyRocket.Height / 2)) >= (Game1.scoYPos + (scorpion.Height / 2))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        private bool IsEnemyShotBehindSco()
        {
            if (ProjectileFireRight.rocketStartX + ProjectileFireRight.rocketPos > Game1.scoXPos + scorpion.Width)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool MoveUpInstead()
        {
            if ((Game1.scoYPos + scorpion.Height * 1.25f) >= 480)
            {
                return true;
            }
            else
                return false;
        }

        private bool MoveDownInstead()
        {
            if ((Game1.scoYPos - scorpion.Height / 4) <= 70)
                return true;
            else
                return false;
        }

        private bool IsGoingInsideCru()
        {
            if (((Game1.cruYPos + (cruiser.Height / 4 * 3)) <= (Game1.scoYPos + (scorpion.Height / 4 * 3)) && (Game1.cruYPos + (cruiser.Height / 4 * 3)) >= (Game1.scoYPos + (scorpion.Height / 4))) ||
                ((Game1.cruYPos + (cruiser.Height / 4)) <= (Game1.scoYPos + (scorpion.Height / 4 * 3)) && (Game1.cruYPos + (cruiser.Height / 4 * 3)) >= (Game1.scoYPos + (scorpion.Height / 4))))
            {
                return true;
            }
            else
                return false;
        }

        private void HandleEnemyShot()
        {
            /*if (IsGoingInsideScoTop() && IsGoingInsideScoBot())
            {
                if (timers[4] <= 0)
                    binaryRanNum = binaryRandom.Next(2);

                if (binaryRanNum == 0)
                {
                    if (!MoveUpInstead() && timers[0] <= 0)
                    {
                        moveOtherWay[0] = true;
                    }
                    else if (MoveUpInstead() && timers[0] <= 0)
                    {
                        moveOtherWay[0] = false;
                    }

                    if (moveOtherWay[0])
                        Game1.scoYPos += HandleMovement.speedLeftRight;
                    else if (!moveOtherWay[0])
                        Game1.scoYPos -= HandleMovement.speedLeftRight;
                }
                else if (binaryRanNum == 1)
                {
                    if (!MoveDownInstead() && timers[1] <= 0)
                    {
                        moveOtherWay[1] = true;
                    }
                    else if (MoveDownInstead() && timers[1] <= 0)
                    {
                        moveOtherWay[1] = false;
                    }

                    if (moveOtherWay[1])
                        Game1.scoYPos -= HandleMovement.speedLeftRight;
                    else if (!moveOtherWay[1])
                        Game1.scoYPos += HandleMovement.speedLeftRight;
                }
            }
            else*/ if (IsGoingInsideScoTop())
            {
                if (!MoveUpInstead() && timers[2] <= 0)
                {
                    moveOtherWay[2] = true;
                }
                else if (MoveUpInstead() && timers[2] <= 0)
                {
                    moveOtherWay[2] = false;
                }

                if (moveOtherWay[2])
                    Game1.scoYPos += HandleMovement.speedLeftRight;
                else if (!moveOtherWay[2])
                    Game1.scoYPos -= HandleMovement.speedLeftRight;
            }
            else if (IsGoingInsideScoBot())
            {
                if (!MoveDownInstead() && timers[3] <= 0)
                {
                    moveOtherWay[3] = true;
                }
                else if (MoveDownInstead() && timers[3] <= 0)
                {
                    moveOtherWay[3] = false;
                }

                if (moveOtherWay[3])
                    Game1.scoYPos -= HandleMovement.speedLeftRight;
                else if (!moveOtherWay[3])
                    Game1.scoYPos += HandleMovement.speedLeftRight;
            }
        }

        private void ChaseAndAttackCruiser()
        {
            if ((Game1.reload1 >= 260 && !Game1.shot1) || IsEnemyShotBehindSco())
            {
                if (Game1.scoXPos >= 405)
                    Game1.scoXPos -= HandleMovement.speedForward;
            }
            else
            {
                if (Game1.scoXPos >= 405)
                    Game1.scoXPos -= HandleMovement.speedForward / 2;
            }

            if (Game1.cruYPos + (cruiser.Height / 2) < Game1.scoYPos + (scorpion.Height / 2))
            {
                if (!handlingEnemyShot)
                    if (Game1.scoYPos >= 70)
                        Game1.scoYPos -= HandleMovement.speedLeftRight;
            }
            else if (Game1.cruYPos + (cruiser.Height / 2) > Game1.scoYPos + (scorpion.Height / 2))
            {
                if (!handlingEnemyShot)
                    if (Game1.scoYPos + scorpion.Height <= 480)
                        Game1.scoYPos += HandleMovement.speedLeftRight;
            }

            if (IsGoingInsideCru())
            {
                FireProjectile();
            }
        }

        public void ChaseAndAttackCruiser2()
        {
            if (IsEnemyShotBehindSco())
            {
                if (Game1.scoXPos >= 405)
                    Game1.scoXPos -= HandleMovement.speedForward * 0.7f;
            }
            else if (Game1.reload1 > 900)
            {
                if (Game1.scoXPos + scorpion.Width <= 800)
                    Game1.scoXPos += HandleMovement.speedBackward * 0.9f;
            }
            else
            {
                if (Game1.scoXPos >= 405)
                    Game1.scoXPos -= HandleMovement.speedForward * 0.35f;
            }

            if (Game1.cruYPos + (cruiser.Height / 2) < Game1.scoYPos + (scorpion.Height / 2))
            {
                if (!handlingEnemyShot)
                    if (Game1.scoYPos >= 70)
                        Game1.scoYPos -= HandleMovement.speedLeftRight;
            }
            else if (Game1.cruYPos + (cruiser.Height / 2) > Game1.scoYPos + (scorpion.Height / 2))
            {
                if (!handlingEnemyShot)
                    if (Game1.scoYPos + scorpion.Height <= 480)
                        Game1.scoYPos += HandleMovement.speedLeftRight;
            }

            if (IsGoingInsideCru() && Game1.reload2 <= 0)
            {
                FireProjectile();
            }
        }

        private void EvadeAndRun()
        {
            if (Game1.reload2 > 260)
            {
                if (Game1.scoXPos + scorpion.Width <= 800)
                    Game1.scoXPos += HandleMovement.speedBackward;
            }

            if (Game1.cruYPos + (cruiser.Height / 2) < Game1.scoYPos + (scorpion.Height / 2))
            {
                if (!handlingEnemyShot)
                    if (!MoveDownInstead() && Game1.scoYPos + scorpion.Height <= 480)
                        Game1.scoYPos -= HandleMovement.speedLeftRight / (float)Math.PI;
            }
            else if (Game1.cruYPos + (cruiser.Height / 2) >= Game1.scoYPos + (scorpion.Height / 2))
            {
                if (!handlingEnemyShot)
                    if (!MoveUpInstead() && Game1.scoYPos >= 70)
                        Game1.scoYPos += HandleMovement.speedLeftRight / (float)Math.PI;
            }
        }

        private void FireProjectile()
        {
            Game1.shot2 = true;
            Game1.scoFireLeft = new ProjectileFireLeft(ownRocket, Game1.scoXPos - 19, Game1.scoYPos + 26);
            Game1.reload2 = Game1.reloadTimeMilliseconds;
            Game1.cruGracePeriod = false;
            rocketSoundSco.Play();
        }

        public void HandleTimers()
        {
            if (initializeTimers)
            {
                for (int i = 0; i < timers.Length - 1; i++)
                {
                    timers[i] = 0;
                }
                initializeTimers = false;
            }

            for (int i = 0; i < timers.Length - 1; i++)
            {
                timers[i] -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public void HandleScorpionAI()
        {
            if (Game1.scoAIEnabled && Game1.splash.endScreen && Game1.displayMenu.endScreen && Game1.isGameActive)
            {
                if (Game1.shot1 && !IsEnemyShotBehindSco())
                {
                    handlingEnemyShot = true;
                    HandleEnemyShot();
                }
                else
                {
                    handlingEnemyShot = false;
                }

                if (Game1.reload1 > 0 && Game1.reload2 <= 0)
                {
                    ChaseAndAttackCruiser();
                }
                else if (Game1.reload1 <= 0 && Game1.reload2 > 0)
                {
                    EvadeAndRun();
                }
                else
                {
                    ChaseAndAttackCruiser2();
                }
            }
        }
    }
}

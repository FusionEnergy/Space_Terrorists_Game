using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Our_First_Game
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D space, cruiser, scorpion, rocketShot1, rocketShot2;
        private Rectangle cruRect, scoRect;
        private ProjectileFireRight cruFireRight;
        private ProjectileFireLeft scoFireLeft;
        private SoundEffect rocketSound;
        private KeyboardState keyOldState;
        private SpriteFont font, tuganoFont;
        private int score1 = 0, score2 = 0;
        private float cruXPos = 50, cruYPos = 380, scoXPos = 700, scoYPos = 60, reload1 = 0, reload2 = 0;
        private bool cruGracePeriod = true, scoGracePeriod = true;
        public static bool shot1 = false, shot2 = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            IsFixedTimeStep = false;

            this.Window.Title = "The Adventure of The Future: Space Terrorists";
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            cruiser = Content.Load<Texture2D>("Pictures/cruiser");
            scorpion = Content.Load<Texture2D>("Pictures/scorpion");
            rocketShot1 = Content.Load<Texture2D>("Pictures/rocket_shot");
            rocketShot2 = Content.Load<Texture2D>("Pictures/rocket_shot2");

            space = Content.Load<Texture2D>("Pictures/Backgrounds/space");

            Song spaceTheme = Content.Load<Song>("Sounds/Music/upbeatTheme");
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(spaceTheme);
            MediaPlayer.IsRepeating = true;

            rocketSound = Content.Load<SoundEffect>("Sounds/SoundFX/rocket_sound");

            font = Content.Load<SpriteFont>("Fonts/Score");
            tuganoFont = Content.Load<SpriteFont>("Fonts/TuganoFont");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            Console.WriteLine("$");

            reload1 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            reload2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            cruRect = new Rectangle((int)cruXPos, (int)cruYPos, cruiser.Width, cruiser.Height);
            scoRect = new Rectangle((int)scoXPos, (int)scoYPos, scorpion.Width, scorpion.Height);

            if (ProjectileFireRight.rocketbox1.Intersects(scoRect) && !scoGracePeriod)
            {
                score1++;
                scoGracePeriod = true;
                Console.WriteLine("$: rocket hit! BLUE");
            }

            if (ProjectileFireLeft.rocketBox2.Intersects(cruRect) && !cruGracePeriod)
            {
                score2++;
                cruGracePeriod = true;
                Console.WriteLine("$: rocket hit! RED");
            }

            KeyboardState keyNewState = Keyboard.GetState();

            if (keyNewState.IsKeyDown(Keys.W))
            {
                cruYPos -= 3;
            }
            if (keyNewState.IsKeyDown(Keys.S))
            {
                cruYPos += 3;
            }
            if (keyNewState.IsKeyDown(Keys.D))
            {
                cruXPos += 2.3f;
            }
            if (keyNewState.IsKeyDown(Keys.A))
            {
                cruXPos -= 1.8f;
            }
            if (keyOldState.IsKeyUp(Keys.Q) && keyNewState.IsKeyDown(Keys.Q) && reload1 >= 1200)
            {
                shot1 = true;
                cruFireRight = new ProjectileFireRight(rocketShot1, cruXPos + 19, cruYPos + 26);
                reload1 = 0;
                scoGracePeriod = false;
                rocketSound.Play(0.1f, 0, 0);
            }

            if (keyNewState.IsKeyDown(Keys.I))
            {
                scoYPos -= 3;
            }
            if (keyNewState.IsKeyDown(Keys.K))
            {
                scoYPos += 3;
            }
            if (keyNewState.IsKeyDown(Keys.J))
            {
                scoXPos -= 2.3f;
            }
            if (keyNewState.IsKeyDown(Keys.L))
            {
                scoXPos += 1.8f;
            }
            if (keyOldState.IsKeyUp(Keys.U) && keyNewState.IsKeyDown(Keys.U) && reload2 >= 1200)
            {
                shot2 = true;
                scoFireLeft = new ProjectileFireLeft(rocketShot2, scoXPos - 19, scoYPos + 26);
                reload2 = 0;
                cruGracePeriod = false;
                rocketSound.Play(0.1f, 0, 0);
            }

            keyOldState = keyNewState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            float frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            string frameRateOutput = Math.Round(frameRate).ToString();

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(space, new Rectangle(0, 0, 800, 480), Color.White);

            spriteBatch.Draw(cruiser, new Vector2(cruXPos, cruYPos), Color.White);
            spriteBatch.Draw(scorpion, new Vector2(scoXPos, scoYPos), Color.White);

            if (shot1 == true)
            {
                cruFireRight.Draw(spriteBatch);
            }

            if (shot2 == true)
            {
                scoFireLeft.Draw(spriteBatch);
            }

            spriteBatch.DrawString(font, frameRateOutput, new Vector2(750, 0), Color.Yellow);
            spriteBatch.DrawString(font, ":", new Vector2(400, 27), Color.White);
            string tempScore1 = Convert.ToString(score1), tempScore2 = Convert.ToString(score2);
            spriteBatch.DrawString(font, tempScore1, new Vector2(375, 27), Color.Blue);
            spriteBatch.DrawString(font, tempScore2, new Vector2(425, 27), Color.Red);
            if (reload1 < 1200)
            {
                spriteBatch.DrawString(tuganoFont, "Reloading...", new Vector2(105, 32), Color.Blue);
            }
            if (reload2 < 1200)
            {
                spriteBatch.DrawString(tuganoFont, "Reloading...", new Vector2(615, 32), Color.Red);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

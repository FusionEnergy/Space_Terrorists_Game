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

        private Texture2D space, cruiser, scorpion, rocketShot1, rocketShot2, borderPixel;
        private Rectangle cruRect, scoRect;
        private ProjectileFireRight cruFireRight;
        private ProjectileFireLeft scoFireLeft;
        private SoundEffect rocketSound;
        private KeyboardState keyOldState;
        private SpriteFont font, tuganoFont;
        private int score1 = 0, score2 = 0;
        private float cruXPos = 50, cruYPos = 380, scoXPos = 700, scoYPos = 80, reload1 = 0, reload2 = 0;
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

            borderPixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            borderPixel.SetData(new[] { Color.White });

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

            reload1 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            reload2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            cruRect = new Rectangle((int)cruXPos, (int)cruYPos, cruiser.Width, cruiser.Height);
            scoRect = new Rectangle((int)scoXPos, (int)scoYPos, scorpion.Width, scorpion.Height);

            if (ProjectileFireRight.rocketbox1.Intersects(scoRect) && !scoGracePeriod)
            {
                score1++;
                scoGracePeriod = true;
                Console.WriteLine(gameTime.TotalGameTime.TotalSeconds + ": rocket hit! BLUE");
            }

            if (ProjectileFireLeft.rocketBox2.Intersects(cruRect) && !cruGracePeriod)
            {
                score2++;
                cruGracePeriod = true;
                Console.WriteLine(gameTime.TotalGameTime.TotalSeconds + ": rocket hit! RED");
            }

            KeyboardState keyNewState = Keyboard.GetState();

            if (keyNewState.IsKeyDown(Keys.W))
            {
                if (cruYPos >= 70)
                cruYPos -= 3;
            }
            if (keyNewState.IsKeyDown(Keys.S))
            {
                if (cruYPos <= 480 - cruiser.Height)
                cruYPos += 3;
            }
            if (keyNewState.IsKeyDown(Keys.D))
            {
                if (cruXPos <= 395 - cruiser.Width)
                cruXPos += 2.3f;
            }
            if (keyNewState.IsKeyDown(Keys.A))
            {
                if (cruXPos >= 0)
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
                if (scoYPos >= 70)
                scoYPos -= 3;
            }
            if (keyNewState.IsKeyDown(Keys.K))
            {
                if (scoYPos <= 480 - scorpion.Height)
                scoYPos += 3;
            }
            if (keyNewState.IsKeyDown(Keys.J))
            {
                if (scoXPos >= 405)
                scoXPos -= 2.3f;
            }
            if (keyNewState.IsKeyDown(Keys.L))
            {
                if (scoXPos <= 800 - scorpion.Width)
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
            string reload = "Reloading...";

            float frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            string frameRateOutput = Math.Round(frameRate).ToString();

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(space, new Rectangle(0, 0, 800, 480), Color.White);

            spriteBatch.Draw(borderPixel, new Rectangle(0, 68, 800, 3), Color.White);
            spriteBatch.Draw(borderPixel, new Rectangle(400, 68, 3, 412), Color.White);

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
            spriteBatch.DrawString(font, ":", new Vector2(398.5f, 27), Color.White);
            string tempScore1 = Convert.ToString(score1), tempScore2 = Convert.ToString(score2);
            spriteBatch.DrawString(font, tempScore1, new Vector2(355, 27), Color.Blue);
            spriteBatch.DrawString(font, tempScore2, new Vector2(435, 27), Color.Red);
            if (reload1 < 1200)
            {
                spriteBatch.DrawString(tuganoFont, reload, new Vector2(55, 32), Color.Blue);
            }
            if (reload2 < 1200)
            {
                spriteBatch.DrawString(tuganoFont, reload, new Vector2(745 - tuganoFont.MeasureString(reload).X, 32), Color.Red);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

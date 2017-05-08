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

        private Texture2D stars, cruiser, scorpion, rocketShot1, rocketShot2;
        private ProjectileFireRight cruFireRight;
        private SoundEffect rocketSound;
        private KeyboardState keyOldState;
        private SpriteFont font;
        private int score = 0;
        private float cruXPos = 50, cruYPos = 380, scoXPos = 700, scoYPos = 60;
        public static bool shot1 = false;

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

            stars = Content.Load<Texture2D>("Pictures/stars");

            Song spaceTheme = Content.Load<Song>("Sounds/Music/upbeatTheme");
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(spaceTheme);
            MediaPlayer.IsRepeating = true;

            rocketSound = Content.Load<SoundEffect>("Sounds/SoundFX/rocket_sound");

            font = Content.Load<SpriteFont>("Fonts/Score");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

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
            if (keyOldState.IsKeyUp(Keys.F) && keyNewState.IsKeyDown(Keys.F))
            {
                shot1 = true;
                cruFireRight = new ProjectileFireRight(rocketShot1, cruXPos + 19, cruYPos + 26);
                rocketSound.Play(0.1f, 0, 0);
            }

            if (keyNewState.IsKeyDown(Keys.Up))
            {
                scoYPos -= 3;
            }
            if (keyNewState.IsKeyDown(Keys.Down))
            {
                scoYPos += 3;
            }
            if (keyNewState.IsKeyDown(Keys.Left))
            {
                scoXPos -= 2.3f;
            }
            if (keyNewState.IsKeyDown(Keys.Right))
            {
                scoXPos += 1.8f;
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

            spriteBatch.Draw(stars, new Rectangle(0, 0, 800, 480), Color.White);

            spriteBatch.Draw(cruiser, new Vector2(cruXPos, cruYPos), Color.White);
            spriteBatch.Draw(scorpion, new Vector2(scoXPos, scoYPos), Color.White);

            if (shot1 == true)
            {
                cruFireRight.Draw(spriteBatch);
            }

            spriteBatch.DrawString(font, "Score: " + score, new Vector2(50, 30), Color.White);
            spriteBatch.DrawString(font, frameRateOutput, new Vector2(750, 0), Color.Yellow);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

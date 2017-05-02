using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Our_First_Game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D stars;
        private SoundEffect laserSound;
        private MouseState mouseOldState;
        private SpriteFont font;
        private int score = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            this.Window.Title = "The Adventure of The Future: Space Terrorists";

            IsFixedTimeStep = false;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            stars = Content.Load<Texture2D>("Pictures/stars");

            Song spaceTheme = Content.Load<Song>("Sounds/Music/upbeatTheme");
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(spaceTheme);
            MediaPlayer.IsRepeating = true;

            laserSound = Content.Load<SoundEffect>("Sounds/SoundFX/laserShot");

            font = Content.Load<SpriteFont>("Fonts/Score");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            MouseState mouseNewState = Mouse.GetState();

            if (mouseOldState.LeftButton == ButtonState.Released && mouseNewState.LeftButton == ButtonState.Pressed)
            {
                score++;
                laserSound.Play(0.3f, 0, 0);
            }
            mouseOldState = mouseNewState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            float frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            string frameRateOutput = Math.Round(frameRate).ToString();

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(stars, new Rectangle(0, 0, 800, 480), Color.White);

            spriteBatch.DrawString(font, "Score: " + score, new Vector2(50, 30), Color.White);
            spriteBatch.DrawString(font, frameRateOutput, new Vector2(750, 0), Color.Yellow);

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

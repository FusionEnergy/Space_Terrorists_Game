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

        private Texture2D stars, cruiser, scorpion, rocketShot1, rocketShot2;
        private SoundEffect rocketSound;
        private KeyboardState keyOldState;
        private SpriteFont font;
        private bool shot1 = false, shot2 = false;
        private int score = 0;
        private float cruXPos = 50, cruYPos = 380, scoXPos = 700, scoYPos = 60, rocketStartX1, rocketStartY1, rocketStartX2, rocketStartY2, rocketSpeed1 = 0, rocketSpeed2 = 0;

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
            if (keyNewState.IsKeyDown(Keys.F)) // if (keyOldState.IsKeyUp(Keys.F) && keyNewState.IsKeyDown(Keys.F))
            {//thinking of creating a function for this rocket shot, maybe in another class even
                shot1 = true;
                rocketStartX1 = cruXPos + 19; //don't know why rocket doesn't start shooting at cruiser sprite
                rocketStartY1 = cruYPos + 26;
            }
            if (rocketStartX1 + rocketSpeed1 >= 800)
            {
                shot1 = false;
            }
            rocketSpeed1 += 6;

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

            spriteBatch.Draw(cruiser, new Vector2(cruXPos, cruYPos), Color.White);
            spriteBatch.Draw(scorpion, new Vector2(scoXPos, scoYPos), Color.White);

            if (shot1 == true)
            {
                spriteBatch.Draw(rocketShot1, new Vector2(rocketStartX1 + rocketSpeed1, rocketStartY1), Color.White);
            }

            spriteBatch.DrawString(font, "Score: " + score, new Vector2(50, 30), Color.White);
            spriteBatch.DrawString(font, frameRateOutput, new Vector2(750, 0), Color.Yellow);

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

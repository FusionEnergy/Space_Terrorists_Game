using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Space_Terrorists;

namespace Our_First_Game
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D cruiser, scorpion, rocketShot1, rocketShot2, explosion, borderPixel, blackTranslucentPixel, blueWinScreen, redWinScreen, splashScreen, howToPlayScreen, blueRed, galaxy, nebula, pink, purple;
        private Texture2D[] backgroundList;
        private Rectangle cruRect, scoRect;
        private AnimatedSprite animatedExplosionCru, animatedExplosionSco;
        public static ProjectileFireRight cruFireRight;
        public static ProjectileFireLeft scoFireLeft;
        private SpriteFont font, neonAdventureFont, fightingForceFont;
        private ScorpionAI scoAI;
        private RoundOver roundOver;
        private static KeyboardState keyOldState, keyNewState;
        private static HandleMovement handleMovement;
        private static Song boss, map, Mars, Mercury, Venus;
        private static Song[] songList;
        private static SoundEffect rocketSound, explosionSound, winScreenSound;
        public static DrawBackground drawBackground;
        public static SplashScreen splash;
        public static HowToPlayMenu displayMenu;
        public static SoundEffectInstance rocketSoundInstanceLeft, rocketSoundInstanceRight, explosionSoundInstance, winScreenSoundInstance;
        public static int score1 = 0, score2 = 0;
        public const int scoreMax = 5, reloadTimeMilliseconds = 2350, firstTimeReloadReduction = 1675; //can change values here for debugging purposes
        public static float cruXPos = 50, cruYPos = 380, scoXPos = 700, scoYPos = 80, reload1, reload2;
        public static bool shot1 = false, shot2 = false, isCruAlive = true, isScoAlive = true, isGameActive = true, cruGracePeriod = true, scoGracePeriod = true, scoAIEnabled;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            IsFixedTimeStep = false;

            this.Window.Title = "The Adventure Of The Future: Space Terrorists";
            Content.RootDirectory = "Content";

            handleMovement = new HandleMovement();
            scoAIEnabled = false;
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
            blackTranslucentPixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blackTranslucentPixel.SetData(new[] { Color.Black * 0.9f });

            cruiser = Content.Load<Texture2D>("Pictures/cruiser");
            scorpion = Content.Load<Texture2D>("Pictures/scorpion");
            rocketShot1 = Content.Load<Texture2D>("Pictures/rocket_shot");
            rocketShot2 = Content.Load<Texture2D>("Pictures/rocket_shot2");

            explosion = Content.Load<Texture2D>("Pictures/Animations/explosion");
            animatedExplosionCru = new AnimatedSprite(explosion, 4, 4);
            animatedExplosionSco = new AnimatedSprite(explosion, 4, 4);

            blueRed = Content.Load<Texture2D>("Pictures/Backgrounds/blue&red");
            galaxy = Content.Load<Texture2D>("Pictures/Backgrounds/galaxy");
            nebula = Content.Load<Texture2D>("Pictures/Backgrounds/nebula");
            pink = Content.Load<Texture2D>("Pictures/Backgrounds/pink");
            purple = Content.Load<Texture2D>("Pictures/Backgrounds/purple");
            backgroundList = new Texture2D[] { blueRed, galaxy, nebula, pink, purple };
            drawBackground = new DrawBackground(backgroundList);

            splashScreen = Content.Load<Texture2D>("Pictures/Screens/SplashScreen");
            splash = new SplashScreen(splashScreen);
            howToPlayScreen = Content.Load<Texture2D>("Pictures/Screens/HowToPlayScreen");
            displayMenu = new HowToPlayMenu(howToPlayScreen);
            blueWinScreen = Content.Load<Texture2D>("Pictures/Screens/BlueWins");
            redWinScreen = Content.Load<Texture2D>("Pictures/Screens/RedWins");

            boss = Content.Load<Song>("Sounds/Music/boss");
            map = Content.Load<Song>("Sounds/Music/map");
            Mars = Content.Load<Song>("Sounds/Music/mars");
            Mercury = Content.Load<Song>("Sounds/Music/mercury");
            Venus = Content.Load<Song>("Sounds/Music/venus");
            songList = new Song[] { boss, map, Mars, Mercury, Venus };
            MediaPlayer.Volume = 0.1f;

            rocketSound = Content.Load<SoundEffect>("Sounds/SoundFX/rocket_sound");
            rocketSoundInstanceLeft = rocketSound.CreateInstance();
            rocketSoundInstanceRight = rocketSound.CreateInstance();
            rocketSoundInstanceLeft.Volume = 0.1f;
            rocketSoundInstanceRight.Volume = 0.1f;
            explosionSound = Content.Load<SoundEffect>("Sounds/SoundFX/atari_death_sound");
            explosionSoundInstance = explosionSound.CreateInstance();
            explosionSoundInstance.Volume = 0.4f;
            winScreenSound = Content.Load<SoundEffect>("Sounds/SoundFX/win_screen_sound");
            winScreenSoundInstance = winScreenSound.CreateInstance();
            winScreenSoundInstance.Volume = 0.45f;
            scoAI = new ScorpionAI(scorpion, cruiser, rocketShot2, rocketShot1, rocketSound);

            font = Content.Load<SpriteFont>("Fonts/Score");
            neonAdventureFont = Content.Load<SpriteFont>("Fonts/NeonAdventureFont");
            fightingForceFont = Content.Load<SpriteFont>("Fonts/FightingForceFont");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (isGameActive)
            {
                reload1 -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                reload2 -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                scoAI.HandleTimers();
            }

            cruRect = new Rectangle((int)cruXPos, (int)cruYPos, cruiser.Width, cruiser.Height);
            scoRect = new Rectangle((int)scoXPos, (int)scoYPos, scorpion.Width, scorpion.Height);

            Random randSongListNumber = new Random();

            if (ProjectileFireRight.rocketbox1.Intersects(scoRect) && !scoGracePeriod)
            {
                scoGracePeriod = true;
                isScoAlive = false;
                explosionSoundInstance.Pan = 0.08f;
                explosionSoundInstance.Play();
                roundOver = new RoundOver();
                roundOver.AwardPoints(0);
            }

            if (ProjectileFireLeft.rocketBox2.Intersects(cruRect) && !cruGracePeriod)
            {
                cruGracePeriod = true;
                isCruAlive = false;
                explosionSoundInstance.Pan = -0.08f;
                explosionSoundInstance.Play();
                roundOver = new RoundOver();
                roundOver.AwardPoints(1);
            }

            keyNewState = Keyboard.GetState();
            
            if (keyOldState.IsKeyUp(Keys.Q) && keyNewState.IsKeyDown(Keys.Q) && reload1 <= 0 && isCruAlive && isGameActive)
            {
                shot1 = true;
                cruFireRight = new ProjectileFireRight(rocketShot1, cruXPos + 19, cruYPos + 26);
                reload1 = reloadTimeMilliseconds;
                scoGracePeriod = false;
                rocketSoundInstanceRight.Play();
            }

            if (keyOldState.IsKeyUp(Keys.U) && keyNewState.IsKeyDown(Keys.U) && reload2 <= 0 && isScoAlive && isGameActive && !scoAIEnabled)
            {
                shot2 = true;
                scoFireLeft = new ProjectileFireLeft(rocketShot2, scoXPos - 19, scoYPos + 26);
                reload2 = reloadTimeMilliseconds;
                cruGracePeriod = false;
                rocketSoundInstanceLeft.Play();
            }

            keyOldState = keyNewState;

            handleMovement.UpdateCruiserMovement(cruiser);
            handleMovement.UpdateScorpionMovement(scorpion);
            scoAI.HandleScorpionAI();

            if (MediaPlayer.State != MediaState.Playing && MediaPlayer.PlayPosition.TotalSeconds == 0.0f)
            {
                MediaPlayer.Play(songList[randSongListNumber.Next(songList.Length - 1)]);
            }

            splash.Update();
            displayMenu.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            drawBackground.Draw(spriteBatch);

            spriteBatch.Draw(blackTranslucentPixel, new Rectangle(0, 0, 800, 68), Color.White);

            spriteBatch.Draw(borderPixel, new Rectangle(0, 68, 800, 3), Color.White);
            spriteBatch.Draw(borderPixel, new Rectangle(400, 68, 3, 412), Color.White);

            if (isCruAlive)
                spriteBatch.Draw(cruiser, new Vector2(cruXPos, cruYPos), Color.White);
            if (isScoAlive)
                spriteBatch.Draw(scorpion, new Vector2(scoXPos, scoYPos), Color.White);

            if (shot1 == true)
            {
                cruFireRight.Draw(spriteBatch);
            }

            if (shot2 == true)
            {
                scoFireLeft.Draw(spriteBatch);
            }

            if (!isScoAlive)
                animatedExplosionSco.Draw(spriteBatch, new Vector2((scoXPos + scorpion.Width / 2) - ((explosion.Width * 2.6f) / 8), (scoYPos + scorpion.Height / 2) - ((explosion.Height * 2.6f) / 8)), 2.6f);

            if (!isCruAlive)
                animatedExplosionCru.Draw(spriteBatch, new Vector2((cruXPos + cruiser.Width / 2) - ((explosion.Width * 2.6f) / 8), (cruYPos + cruiser.Height / 2) - ((explosion.Height * 2.6f) / 8)), 2.6f);

            spriteBatch.DrawString(font, ":", new Vector2(398.5f, 27), Color.White);
            string tempScore1 = Convert.ToString(score1), tempScore2 = Convert.ToString(score2);
            spriteBatch.DrawString(font, tempScore1, new Vector2(355, 27), Color.Blue);
            spriteBatch.DrawString(font, tempScore2, new Vector2(435, 27), Color.Red);

            string reloadCruStr = Convert.ToString(Math.Round((reload1/1000), 1)), reloadScoStr = Convert.ToString(Math.Round((reload2/1000), 1));
            if (reload1 >= 0)
            {
                spriteBatch.DrawString(neonAdventureFont, "Reloading...   " + reloadCruStr, new Vector2(95, 24), Color.Blue);
            }
            if (reload2 >= 0)
            {
                spriteBatch.DrawString(neonAdventureFont, "Reloading...   " + reloadScoStr, new Vector2(705 - neonAdventureFont.MeasureString("Reloading...   0.0").X, 24), Color.Red);
            }

            if (score1 == scoreMax)
            {
                roundOver.GameOver(spriteBatch, 0, blueWinScreen);
            }

            if (score2 == scoreMax)
            {
                roundOver.GameOver(spriteBatch, 1, redWinScreen);
            }

            splash.Draw(spriteBatch, fightingForceFont);
            displayMenu.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

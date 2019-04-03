using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Block_s_Quest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Dwayne dwayne;
        Texture2D dwaynet, bulletT, diamondt, shopt, dpadt;
        KeyboardState kb;
        Level level;
        int levelIndex, maxLevel;
        KeyboardState oldkb;
        GameState gameState;
        SpriteFont font, font1;
        Rectangle selectionRectangle;
        Color background = Color.CornflowerBlue;
        Color op1 = Color.White;
        Color op2 = Color.White;
        Color op3 = Color.White;
        enum GameState
        {
            MainMenu, Normal, Hardcore, Insane, GameOver
        };

        enum bullType
        {
            fire,
            chi,
            earth,
            water
        };
        UI gui;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1800;
            graphics.ApplyChanges();
            IsMouseVisible = true;
            levelIndex = 1;
            maxLevel = 2;
            gameState = GameState.MainMenu;
            selectionRectangle = new Rectangle(750, 500, 0, 0);
            oldkb = Keyboard.GetState();

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

            // TODO: use this.Content to load your game content here
            dwaynet = this.Content.Load<Texture2D>("Dwayne Angry Face");
            bulletT = this.Content.Load<Texture2D>("Bullet");
            diamondt = this.Content.Load<Texture2D>("Diamonds");
            shopt = this.Content.Load<Texture2D>("shop");
            dpadt = this.Content.Load<Texture2D>("dpad");
            font = this.Content.Load<SpriteFont>("SpriteFont1");
            gui = new UI(font, bulletT, shopt, diamondt, dpadt);
            gui.show();
            dwayne = new Dwayne(dwaynet, bulletT);
            font = Content.Load<SpriteFont>("SpriteFont1");
            font1 = Content.Load<SpriteFont>("SpriteFont2");

            LoadLevel();
        }

        private void LoadLevel()
        {
            level = new Level(Services, @"Content/Levels/Level"+levelIndex+".txt");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            KeyboardState kb = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();
            if (gameState == GameState.MainMenu)
            {
                if (selectionRectangle.Y < 500)
                {
                    selectionRectangle.Y = 700;
                }
                if (selectionRectangle.Y > 700)
                {
                    selectionRectangle.Y = 500;
                }
                if (kb.IsKeyDown(Keys.Up) && !oldkb.IsKeyDown(Keys.Up))
                {
                    selectionRectangle.Y -= 100;
                }
                if (kb.IsKeyDown(Keys.Down) && !oldkb.IsKeyDown(Keys.Down))
                {
                    selectionRectangle.Y += 100;
                }
            }
            else       
            {
                dwayne.Update(kb);
                if (level.LevelEnd())
                {
                    levelIndex++;

                    if (levelIndex <= maxLevel)
                        LoadLevel();
                }
            }
            if (selectionRectangle.Y == 500)
            {
                op1 = Color.Blue;
                if (kb.IsKeyDown(Keys.Enter) && !oldkb.IsKeyDown(Keys.Enter))
                {
                    gameState = GameState.Normal;
                }
            }
            else
            {
                op1 = Color.White;
            }
            if (selectionRectangle.Y == 600)
            {
                op2 = Color.Blue;
                if (kb.IsKeyDown(Keys.Enter) && !oldkb.IsKeyDown(Keys.Enter))
                {
                    gameState = GameState.Hardcore;
                }
            }
            else
            {
                op2 = Color.White;
            }
            if (selectionRectangle.Y == 700)
            {
                op3 = Color.Blue;
                if (kb.IsKeyDown(Keys.Enter) && !oldkb.IsKeyDown(Keys.Enter))
                {
                    gameState = GameState.Insane;
                }
            }
            else
            {
                op3 = Color.White;
            }
            oldkb = kb;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(background);
            spriteBatch.Begin();
            if (gameState == GameState.MainMenu)
            {
                background = Color.SaddleBrown;
                spriteBatch.DrawString(font1, "The Block's Quest", new Vector2(550, 0), Color.White);
                spriteBatch.DrawString(font, "Normal Mode", new Vector2(750, 500), op1);
                spriteBatch.DrawString(font, "Hardcore Mode", new Vector2(750, 600), op2);
                spriteBatch.DrawString(font, "Insane Mode", new Vector2(750, 700), op3);
            }
            else
            {
                background = Color.CornflowerBlue;
                dwayne.Draw(spriteBatch, gameTime);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

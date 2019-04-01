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
        SpriteFont font;

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
            kb = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if(level.LevelEnd())
            {
                levelIndex++;

                if(levelIndex<=maxLevel)
                    LoadLevel();
            }

            dwayne.Update(kb);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            dwayne.Draw(spriteBatch, gameTime);
            gui.Draw(spriteBatch, gameTime);
            base.Draw(gameTime);
        }
    }
}

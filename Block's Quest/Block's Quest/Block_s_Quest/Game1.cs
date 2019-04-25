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
        Level level,owBuild;
        int levelIndex, maxLevel;
        KeyboardState oldkb;
        GameState gameState;
        SpriteFont font, font1;
        Rectangle selectionRectangle;
        Color background = Color.CornflowerBlue;
        Color op1 = Color.White;
        Color op2 = Color.White;
        Color op3 = Color.White;
        SoundEffect shootEffect, gameMusic;
        SoundEffectInstance musicInstance;
        int winTimer = 0, gameOverTimer = 0;
        int spawnTimer = 0;
        UI gui;
        List<Bullet> bullets;
        List<Enemy> enemy;
        Boolean bug;
        Overworld ow;
        Boolean soundEffectPlayed;

        enum GameState
        {
            MainMenu, Normal, Hardcore, Insane, GameOver, Win, Overworld
        };

        enum bullType
        {
            fire,
            chi,
            earth,
            water
        };

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
            bug = true;
            levelIndex = 1;
            maxLevel = 4;
            bullets = new List<Bullet>();
            enemy = new List<Enemy>();
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
            font = Content.Load<SpriteFont>("SpriteFont1");
            font1 = Content.Load<SpriteFont>("SpriteFont2");
            shootEffect = Content.Load<SoundEffect>("pm_ag_1_2_abstract_guns_281");
            gameMusic = Content.Load<SoundEffect>("Bakugan - Aquos Arena");
            musicInstance = gameMusic.CreateInstance();
            dwayne = new Dwayne(dwaynet, bulletT, shootEffect, this.Content);
            LoadLevel();
            LoadOverWorld();
        }

        private void LoadLevel()
        {
            level = new Level(Services, @"Content/Levels/Level"+levelIndex+".txt", bulletT);
        }

        private void LoadOverWorld()
        {
            owBuild = new Level(Services, @"Content/Overworlds/Overworld1.txt");
            ow = new Overworld(bulletT, dwaynet, owBuild.getPath(), Services);
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
            if (musicInstance.State != SoundState.Playing)
                musicInstance.Play();

            if (bug)
            {
                dwayne.Shoot();
                bullets = dwayne.getBullets();
                bullets.Remove(bullets[0]);
                bug = false;
            }

            //Tester for Fire Rate Upgrade
            if (kb.IsKeyDown(Keys.I) && !oldkb.IsKeyDown(Keys.I))
                dwayne.UpgradeFireRate();

            //Tester for # Bullets Upgrade
            if (kb.IsKeyDown(Keys.O) && !oldkb.IsKeyDown(Keys.O))
                dwayne.UpgradeNumBullets();

            if (gameState == GameState.MainMenu)
            {
                dwayne.setPos(-900, -900);
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
                if (selectionRectangle.Y == 500)
                {
                    op1 = Color.Blue;
                    if (kb.IsKeyDown(Keys.Enter) && oldkb.IsKeyUp(Keys.Enter))
                    {
                        gameState = GameState.Overworld;
                    }
                }
                else
                {
                    op1 = Color.White;
                }
            }
            else if(gameState == GameState.Overworld)
            {
                ow.Update(gameTime, kb, oldkb);
                if(kb.IsKeyDown(Keys.Enter) && oldkb.IsKeyUp(Keys.Enter))
                {
                    gameState = GameState.Normal;
                }
                dwayne.setPos(950, 875);
            }
            else       
            {
                //Checks for collisoin of enemies with bullets
                for (int i = dwayne.getBullets().Count - 1; i >= 0; i--)
                {
                    bullets = dwayne.getBullets();
                    enemy = level.getEnemies();

                    //Removes Bullet if off Screen
                    if (bullets[i].getRect().Y + 20 < 0)
                        bullets.Remove(bullets[i]);
                    else
                    {
                        for (int j = enemy.Count - 1; j >= 0; j--)
                        {
                            if (bullets[i].getRect().Intersects(enemy[j].getRect()))
                            {
                                gui.score++;
                                enemy[j].decreaseHitPoints(bullets[i].getBulletDamage());
                            }
                        }
                    }      
                }
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i].getRect().Y <= 200)
                    {
                        bullets.Remove(bullets[i]);
                    }
                }
                if (levelIndex == 4)
                {
                    spawnTimer++;
                    if (spawnTimer % 180 == 0)
                    {
                        level.spawnEnemy(level.bossRect);
                    }
                }
                level.Update();

                if (dwayne.isDead(enemy))
                {
                    gameState = GameState.GameOver;
                }
                dwayne.Update(kb, gui);

                //Changes to next Level
                if (level.LevelEnd())
                {
                    //gameState = GameState.Overworld;
                    levelIndex++;
                    gui.score = 0;

                    if (levelIndex <= maxLevel)
                        LoadLevel();
                    else
                        gameState = GameState.Win;

                    winTimer++;
                }
            }
            if (gameState == GameState.GameOver)
            {
                gameOverTimer++;
            }
            if (winTimer >= 180)
            {
                gameState = GameState.MainMenu;
                levelIndex = 0;
                winTimer = 0;
            }
            if (gameOverTimer >= 180)
            {
                gameState = GameState.MainMenu;
                levelIndex = 0;
                gameOverTimer = 0;
            }
            if (selectionRectangle.Y == 500)
            {
                op1 = Color.Blue;
                if (kb.IsKeyDown(Keys.Enter) && !oldkb.IsKeyDown(Keys.Enter) && gameState==GameState.MainMenu)
                {
                    gameState = GameState.Normal;
                    dwayne.setPos(950, 875);
                }
            }
            else
            {
                op1 = Color.White;
            }
            if (selectionRectangle.Y == 600)
            {
                op2 = Color.Blue;
                if (kb.IsKeyDown(Keys.Enter) && !oldkb.IsKeyDown(Keys.Enter) && gameState == GameState.MainMenu)
                {
                    gameState = GameState.Hardcore;
                    dwayne.setPos(950, 875);
                }
            }
            else
            {
                op2 = Color.White;
            }
            if (selectionRectangle.Y == 700)
            {
                op3 = Color.Blue;
                if (kb.IsKeyDown(Keys.Enter) && !oldkb.IsKeyDown(Keys.Enter) && gameState == GameState.MainMenu)
                {
                    gameState = GameState.Insane;
                    dwayne.setPos(950, 875);
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
            switch(gameState)
            {
                case GameState.MainMenu:
                    background = Color.DarkSalmon;
                    spriteBatch.DrawString(font1, "The Block's Quest", new Vector2(550, 0), Color.White);
                    spriteBatch.DrawString(font, "Normal Mode", new Vector2(750, 500), op1);
                    spriteBatch.DrawString(font, "Hardcore Mode", new Vector2(750, 600), op2);
                    spriteBatch.DrawString(font, "Insane Mode", new Vector2(750, 700), op3);
                    gui.hide();
                    break;
                case GameState.Overworld:
                    owBuild.Draw(gameTime, spriteBatch);
                    ow.Draw(gameTime, spriteBatch);
                    break;
                case GameState.GameOver:
                    background = Color.Crimson;
                    spriteBatch.DrawString(font1, "GAME OVER", new Vector2(650, 500), Color.White);
                    break;
                case GameState.Win:
                    background = Color.LightSeaGreen;
                    spriteBatch.DrawString(font1, "YOU WIN", new Vector2(650, 500), Color.White);
                    break;
                default:
                    background = Color.DarkSalmon;
                    level.Draw(gameTime, spriteBatch);
                    dwayne.Draw(spriteBatch, gameTime);
                    gui.show();
                    gui.Draw(spriteBatch, gameTime);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

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
        Texture2D dwaynet, bulletT, diamondt, shopt, dpadt, escp,URateT;
        Level level,owBuild;
        int levelIndex, maxLevel;
        KeyboardState oldkb;
        GameState gameState,prevState;
        SpriteFont font, font1;
        List<Diamond> collectables = new List<Diamond>();
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
        Color[] pause = new Color[3];
        Color[] shop = new Color[2];
        Rectangle[] items = new Rectangle[2];
        string[] itemName = new string[2];
        int current = 0;
        Wallet wallet = new Wallet();
        int[] cost = new int[2];
        bool[] upgradeable = new bool[2];
        String levelclear = "Level Clear!";
        Vector2 clearloc;
        int timer = 300;
        SpriteFont largefont;
        Texture2D[] UB = new Texture2D[3];
        int curretnUB = 0;
        int finalscore = 0;
        //Boolean soundEffectPlayed;

        enum GameState
        {
            MainMenu, Normal, Hardcore, Insane, GameOver, Win, Overworld, Pause, Shop, LevelClear
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

        public Diamond.type getDiamondType()
        {
            Random rand = new Random();
            int r = rand.Next(0,100);

            if (r > 100-(2*levelIndex) && r <= 100)
                return Diamond.type.orange;
            else if (r > 95-(2*levelIndex) && r <= 95)
                return Diamond.type.purple;
            else if (r > 90-(2*levelIndex) && r <= 90)
                return Diamond.type.red;
            else if (r > 65 && r <= 80)
                return Diamond.type.yellow;
            else if (r > 50 && r <= 65)
                return Diamond.type.blue;
            else
                return Diamond.type.green;
        }

        public void ResetGame()
        {
            dwayne = new Dwayne(dwaynet, bulletT, shootEffect, this.Content); ;
            levelIndex = 1;
            LoadOverWorld();
            gui.score = 0;
            curretnUB = 0;
            for (int x = 0; x < 2; x++)
            {
                upgradeable[x] = true;
                cost[x] = 10;
            }
            wallet.Reset();

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

            for (int x = 0; x < 3; x++)
                pause[x] = Color.White;

            for (int x = 0; x < 2; x++)
            {
                upgradeable[x] = true;
                cost[x] = 10;
                items[x] = new Rectangle(500 + (x * 200), 600, 100, 100);
                shop[x] = Color.DarkSalmon;
            }

            itemName[0] = "Upgrade \n Fire Rate";
            itemName[1] = "Upgrade \n # of Bullets";
            clearloc = new Vector2(750, 450);
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
            escp = this.Content.Load<Texture2D>("Escp");
            shopt = this.Content.Load<Texture2D>("shop");
            dpadt = this.Content.Load<Texture2D>("dpad");
            font = this.Content.Load<SpriteFont>("SpriteFont1");
            gui = new UI(font, bulletT, shopt, diamondt, dpadt, wallet);
            gui.show();
            font = Content.Load<SpriteFont>("SpriteFont1");
            font1 = Content.Load<SpriteFont>("SpriteFont2");
            shootEffect = Content.Load<SoundEffect>("pm_ag_1_2_abstract_guns_281");
            gameMusic = Content.Load<SoundEffect>("Bakugan - Aquos Arena");
            musicInstance = gameMusic.CreateInstance();
            dwayne = new Dwayne(dwaynet, bulletT, shootEffect, this.Content);
            largefont = Content.Load<SpriteFont>("LARGEFONT");
            for(int x=0;x<3;x++)
                UB[x]= this.Content.Load<Texture2D>("UB "+(x+1));
            URateT= this.Content.Load<Texture2D>("URate");
            //LoadLevel();
            LoadOverWorld();
        }
        

        private void LoadLevel()
        {
            level = new Level(Services, @"Content/Levels/Level"+levelIndex+".txt", bulletT);
        }

        //private void LoadLevel()
        //{
        //    level = new Level(Services, @"Content/Levels/Level/1.txt", Content.Load<Texture2D>("Tiles/Node"));
        //    level.setTexture(diamondt);
        //}

        private void LoadOverWorld()
        {
            owBuild = new Level(Services, @"Content/Overworlds/Overworld1.txt");
            ow = new Overworld(bulletT, dwaynet, new Path(owBuild.getPath()), Services);
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
            GamePadState game1 = GamePad.GetState(PlayerIndex.One);
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit(); 
            if (musicInstance.State != SoundState.Playing)
                musicInstance.Play();

            if (bug)
            {
                dwayne.Shoot();
                bullets = dwayne.getBullets();
                bullets[0].deactivate();
                bug = false;
            }

            //Tester for Fire Rate Upgrade
            if (kb.IsKeyDown(Keys.I) && !oldkb.IsKeyDown(Keys.I) || game1.Buttons.X == ButtonState.Pressed)
                dwayne.UpgradeFireRate();

            //Tester for # Bullets Upgrade
            if (kb.IsKeyDown(Keys.O) && !oldkb.IsKeyDown(Keys.O) || game1.Buttons.Y == ButtonState.Pressed)
                dwayne.UpgradeNumBullets();

            //Tester for Wallet Class/Shop
            //Gives Diamonds
            if (kb.IsKeyDown(Keys.P) && !oldkb.IsKeyDown(Keys.P) || game1.Buttons.LeftShoulder == ButtonState.Pressed)
                wallet.deposit(10);

            //Tester for Reset
            if (kb.IsKeyDown(Keys.R) && !oldkb.IsKeyDown(Keys.R) || game1.Buttons.RightShoulder == ButtonState.Pressed)
                ResetGame();

            //Pause
            if (kb.IsKeyDown(Keys.Escape) && !oldkb.IsKeyDown(Keys.Escape) && gameState != GameState.GameOver && gameState != GameState.Win || game1.Buttons.Start == ButtonState.Pressed && gameState != GameState.GameOver && gameState != GameState.Win)
            {
                if (gameState != GameState.Shop && gameState != GameState.Pause)
                    prevState = gameState;

                gameState = GameState.Pause;
            }

            switch(gameState)
            {
                case GameState.MainMenu:
                        dwayne.setPos(-900, -900);
                        if (selectionRectangle.Y < 500)
                            selectionRectangle.Y = 700;
                        if (selectionRectangle.Y > 700)
                            selectionRectangle.Y = 500;
                        if (kb.IsKeyDown(Keys.Up) && !oldkb.IsKeyDown(Keys.Up))
                            selectionRectangle.Y -= 100;
                        if (kb.IsKeyDown(Keys.Down) && !oldkb.IsKeyDown(Keys.Down))
                            selectionRectangle.Y += 100;
                        if (selectionRectangle.Y == 500)
                        {
                            op1 = Color.Blue;
                            if (kb.IsKeyDown(Keys.Enter) && oldkb.IsKeyUp(Keys.Enter) || game1.Buttons.A == ButtonState.Pressed)
                                gameState = GameState.Overworld;
                        }
                        else
                            op1 = Color.White;
                    break;

                case GameState.Overworld:
                    ow.Update(gameTime, kb, oldkb);
                    if (kb.IsKeyDown(Keys.Enter) && oldkb.IsKeyUp(Keys.Enter) && ow.isActive() || game1.Buttons.A == ButtonState.Pressed && ow.isActive()) 
                    {
                        if (ow.isLevel())
                        {
                            level = ow.returnLevel();
                            gameState = GameState.Normal;
                        }
                    }
                    dwayne.setPos(950, 875);
                    break;

                case GameState.GameOver:
                    gameOverTimer++;
                    break;

                case GameState.Win:
                    winTimer++;
                    break;

                case GameState.Pause:
                    if ((kb.IsKeyDown(Keys.Up) && !oldkb.IsKeyDown(Keys.Up)) || (kb.IsKeyDown(Keys.W) && !oldkb.IsKeyDown(Keys.W)) || game1.DPad.Up == ButtonState.Pressed)
                    {
                        if (current - 1 < 0)
                            current = 2;
                        else
                            current--;
                    }
                    else if ((kb.IsKeyDown(Keys.Down) && !oldkb.IsKeyDown(Keys.Down)) || (kb.IsKeyDown(Keys.S) && !oldkb.IsKeyDown(Keys.S)) || game1.DPad.Down == ButtonState.Pressed)
                    {
                        if (current + 1 > 2)
                            current = 0;
                        else
                            current++;
                    }

                    if (kb.IsKeyDown(Keys.Enter) && !oldkb.IsKeyDown(Keys.Enter) || game1.Buttons.A == ButtonState.Pressed)
                    {
                        switch (current)
                        {
                            case 0:
                                gameState = GameState.Shop;
                                break;
                            case 1:
                                gameState = prevState;
                                break;
                            case 2:
                                this.Exit();
                                break;
                        }
                        current = 0;

                    }

                    for (int x = 0; x < 3; x++)
                    {
                        if (x == current)
                            pause[x] = Color.Blue;
                        else
                            pause[x] = Color.White;
                    }
                    break;

                case GameState.Shop:
                    if ((kb.IsKeyDown(Keys.Right) && !oldkb.IsKeyDown(Keys.Right)) || (kb.IsKeyDown(Keys.D) && !oldkb.IsKeyDown(Keys.D)) || game1.DPad.Right == ButtonState.Pressed)
                    {
                        if (current - 1 < 0)
                            current = 1;
                        else
                            current--;
                    }
                    else if ((kb.IsKeyDown(Keys.Left) && !oldkb.IsKeyDown(Keys.Left)) || (kb.IsKeyDown(Keys.A) && !oldkb.IsKeyDown(Keys.A)) || game1.DPad.Left == ButtonState.Pressed)
                    {
                        if (current + 1 > 1)
                            current = 0;
                        else
                            current++;
                    }

                    if (kb.IsKeyDown(Keys.Enter) && !oldkb.IsKeyDown(Keys.Enter) || game1.Buttons.A == ButtonState.Pressed)
                    {
                        switch (current)
                        {
                            case 0:
                                if (wallet.afford(cost[0]) && upgradeable[0])
                                {
                                    dwayne.UpgradeFireRate();
                                    wallet.withdraw(cost[0]);
                                    cost[0] += 20;

                                    if (dwayne.getFireRate() == 5)
                                        upgradeable[0] = false;
                                }
                                break;
                            case 1:
                                if (wallet.afford(cost[1]) && upgradeable[1])
                                {
                                    dwayne.UpgradeNumBullets();
                                    wallet.withdraw(cost[1]);
                                    cost[1] += 40;

                                    if (dwayne.getNumBullets() == 4)
                                        upgradeable[1] = false;
                                    else
                                        curretnUB++;
                                }
                                break;
                        }
                    }

                    for (int x = 0; x < 2; x++)
                    {
                        if (x == current)
                            shop[x] = Color.Yellow;
                        else
                            shop[x] = Color.DarkSalmon;
                    }

                    break;
                case GameState.LevelClear:
                    //Diamonds
                    gui.updateWallet(wallet);
                    foreach (Diamond d in collectables)
                        d.Update();

                    for (int i = collectables.Count - 1; i >= 0; i--)
                    {
                        if (dwayne.getRect().Intersects(collectables[i].getRect()))
                        {
                            wallet.addDiamond(collectables[i]);
                            collectables.Remove(collectables[i]);
                        }
                    }

                    //Bullets
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        if (bullets[i].getRect().Y <= 0)
                        {
                            bullets.Remove(bullets[i]);
                        }
                    }

                    //Level
                    level.Update();
                    dwayne.Update(kb, gui);

                    timer--;
                    //Ends after either 5 seconds or when player skips
                    if(timer <= 0 || (kb.IsKeyUp(Keys.Enter) && oldkb.IsKeyDown(Keys.Enter)) || game1.Buttons.A == ButtonState.Pressed)
                    {
                        timer = 300;
                        gameState = GameState.Overworld;
                    }
                    break;
                //GamePlay
                default:
                    gui.updateWallet(wallet);
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
                                //Kill enemies
                                if (bullets[i].getRect().Intersects(enemy[j].getRect()) && bullets[i].isActive())
                                {
                                    gui.score++;
                                    if (enemy[j].living())
                                    {
                                        if (enemy[j].getType() == bullets[i].getType())
                                        {
                                            enemy[j].decreaseHitPoints(bullets[i].getBulletDamage() * 2);

                                            if(enemy[j].living()==false)
                                            {
                                                collectables.Add(new Diamond(enemy[j].getRect().X, enemy[j].getRect().Y, diamondt, getDiamondType()));
                                                collectables.Add(new Diamond(enemy[j].getRect().X, enemy[j].getRect().Y - 10, diamondt, getDiamondType()));
                                            }
                                        }
                                        else
                                        {
                                            enemy[j].decreaseHitPoints(bullets[i].getBulletDamage());

                                            if(enemy[j].living() == false)
                                                collectables.Add(new Diamond(enemy[j].getRect().X, enemy[j].getRect().Y, diamondt, getDiamondType()));
                                        }
                                        bullets[i].deactivate();
                                    }
                                }
                            }
                        }
                    }

                    foreach (Diamond d in collectables)
                        d.Update();

                    for (int i = collectables.Count - 1; i >= 0; i--)
                    {
                        if (dwayne.getRect().Intersects(collectables[i].getRect()))
                        {
                            wallet.addDiamond(collectables[i]);
                            collectables.Remove(collectables[i]);
                        }
                    }

                    enemy = level.getEnemies();

                    //Removes Bullets
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        if (bullets[i].getRect().Y <= 0)
                        {
                            bullets.Remove(bullets[i]);
                        }
                    }

                    //Spawn Enemies from Boss
                    if (level.BossEnemy() && enemy[0].isAlive==true)
                    {
                        spawnTimer++;
                        if (spawnTimer % 120 == 0)
                        {
                            level.spawnEnemy(level.bossRect);
                        }
                    }

                    level.Update();

                    if (dwayne.isDead(enemy))
                    {
                        gameState = GameState.GameOver;
                        ResetGame();
                    }
                    dwayne.Update(kb, gui);

                    //Changes to next Level
                    if (level.LevelEnd())
                    {
                        ow.deactivate();

                        if (level.BossEnemy())
                        {
                            gameState = GameState.Win;
                            finalscore = gui.score;
                            ResetGame();
                        }
                        else
                            gameState = GameState.LevelClear;
                    }
                    break;
            }


            //Conditions
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
                if (kb.IsKeyDown(Keys.Enter) && !oldkb.IsKeyDown(Keys.Enter) && gameState==GameState.MainMenu || game1.Buttons.A == ButtonState.Pressed && gameState == GameState.MainMenu)
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
                if (kb.IsKeyDown(Keys.Enter) && !oldkb.IsKeyDown(Keys.Enter) && gameState == GameState.MainMenu || game1.Buttons.A == ButtonState.Pressed && gameState == GameState.MainMenu)
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
                if (kb.IsKeyDown(Keys.Enter) && !oldkb.IsKeyDown(Keys.Enter) && gameState == GameState.MainMenu || game1.Buttons.A == ButtonState.Pressed && gameState == GameState.MainMenu)
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
                    spriteBatch.DrawString(font1, "YOU WIN", new Vector2(700, 500), Color.White);
                    spriteBatch.DrawString(font, "Score: "+finalscore, new Vector2(700, 600), Color.White);
                    break;
                case GameState.Pause:
                    spriteBatch.DrawString(font1, "PAUSE", new Vector2(800, 300), Color.White);
                    spriteBatch.DrawString(font, "Shop", new Vector2(750, 500), pause[0]);
                    spriteBatch.DrawString(font, "Resume", new Vector2(750, 600), pause[1]);
                    spriteBatch.DrawString(font, "Exit", new Vector2(750, 700), pause[2]);
                    break;
                case GameState.Shop:
                    spriteBatch.DrawString(font1, "SHOP", new Vector2(800, 300), Color.White);
                    spriteBatch.Draw(escp, new Rectangle(800, 50, 100, 100), Color.White);
                    for (int x=0;x<2;x++)
                    {
                        spriteBatch.Draw(bulletT, new Rectangle(items[x].X-20,items[x].Y-20,140,140), shop[x]);
                        spriteBatch.DrawString(font, itemName[x], new Vector2(items[x].X, items[x].Y + 150),Color.White);

                        if(upgradeable[x])
                            spriteBatch.DrawString(font, "Price: $" + cost[x], new Vector2(items[x].X, items[x].Y + 250), Color.White);
                        else
                            spriteBatch.DrawString(font, "Max Upgrade Reached", new Vector2(items[x].X, items[x].Y + 250), Color.White);
                    }
                    spriteBatch.Draw(URateT, items[0], Color.White);
                    spriteBatch.Draw(UB[curretnUB], items[1], Color.White);
                    spriteBatch.DrawString(font, "$" + wallet.getBalance(), new Vector2(800, 500), Color.White);
                    break;
                case GameState.LevelClear:
                    background = Color.DarkSalmon;
                    spriteBatch.DrawString(largefont, levelclear, clearloc, Color.White);
                    level.Draw(gameTime, spriteBatch);
                    dwayne.Draw(spriteBatch, gameTime);
                    gui.show();
                    gui.Draw(spriteBatch, gameTime);
                    foreach (Diamond d in collectables)
                        spriteBatch.Draw(diamondt, d.getRect(), d.getColor());
                    break;
                default:
                    background = Color.DarkSalmon;
                    level.Draw(gameTime, spriteBatch);
                    dwayne.Draw(spriteBatch, gameTime);
                    gui.show();
                    gui.Draw(spriteBatch, gameTime);
                    foreach (Diamond d in collectables)
                        spriteBatch.Draw(diamondt, d.getRect(), d.getColor());
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

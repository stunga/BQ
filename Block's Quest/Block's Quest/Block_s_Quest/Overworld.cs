﻿using System;
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
    class Overworld
    {
        Texture2D background, playert;
        Rectangle player;
        Road[,] path;
        Rectangle[,] grid = new Rectangle[20, 10];
        Rectangle[,] back = new Rectangle[20, 10];
        List<Level> levels = new List<Level>();
        //Level currlevel;

        public Overworld()
        {

        }

        public Overworld(Texture2D t, Texture2D p)
        {
            background = t;
            playert = p;
        }

        public Overworld(Texture2D t, Texture2D p, Road[,] pa, IServiceProvider Services)
        {
            background = t;
            playert = p;
            path = pa;
            int levelcounter = 1;
            for (int y = 0; y  < 20; y++)
            {
                for(int x = 0; x < 10; x++)
                {
                    if (path[y, x] != null)
                    {
                        if (path[y, x].getType() == 2)
                        {
                            Level l = new Level(Services, @"Content/Levels/Level" + levelcounter + ".txt", t);
                        }
                        if (path[y, x].getType() == 0)
                        {
                            player = new Rectangle(y * 100, x * 100, 100, 100);
                            grid[y, x] = player;
                            back[y, x] = new Rectangle(y * 100, x * 100, 100, 100);
                        }
                        else
                        {
                            grid[y, x] = new Rectangle(y * 100, x * 100, 100, 100);
                            back[y, x] = new Rectangle(y * 100, x * 100, 100, 100);
                        }
                    }
                }
            }
        }

        public void Update(GameTime gt, KeyboardState kb, KeyboardState oldkb)
        {
            int x = player.X / 100;
            int y = player.Y / 100;
            if ((kb.IsKeyUp(Keys.A) && oldkb.IsKeyDown(Keys.A)) && grid[x - 1, y] != null)
            {
                player = grid[x - 1, y];
            }
            if ((kb.IsKeyUp(Keys.S) && oldkb.IsKeyDown(Keys.S)) && grid[x, y + 1] != null)
            {
                player = grid[x, y + 1];
            }
            if ((kb.IsKeyUp(Keys.D) && oldkb.IsKeyDown(Keys.D)) && grid[x + 1, y] != null)
            {
                player = grid[x + 1, y];
            }
            if ((kb.IsKeyUp(Keys.W) && oldkb.IsKeyDown(Keys.W)) && grid[x, y - 1] != null)
            {
                player = grid[x, y - 1];
            }
        }

        public void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(playert, player, Color.White);
        }

        public void addLevel(LevelNode l)
        {

        }

        public void addRoad(Road r)
        {

        }

    }
}

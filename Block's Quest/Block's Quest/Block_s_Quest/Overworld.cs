using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Block_s_Quest
{
    class Overworld
    {
        Texture2D background, playert;
        Rectangle player;
        Road[,] path;
        Rectangle[,] grid = new Rectangle[20, 10];
        Rectangle[,] back = new Rectangle[20, 10];

        public Overworld()
        {

        }

        public Overworld(Texture2D t, Texture2D p)
        {
            background = t;
            playert = p;
        }

        public Overworld(Texture2D t, Texture2D p, Road[,] pa)
        {
            background = t;
            playert = p;
            path = pa;
            for(int x = 0; x  < 20; x++)
            {
                for(int y = 0; y < 10; y++)
                {
                    if (path[x, y] != null)
                    {
                        if (path[x, y].getType() == 0)
                        {
                            player = new Rectangle(x * 100, y * 100, 100, 100);
                            grid[x, y] = player;
                            back[x, y] = new Rectangle(x * 100, y * 100, 100, 100);
                        }
                        else
                        {
                            grid[x, y] = new Rectangle(x * 100, y * 100, 100, 100);
                            back[x, y] = new Rectangle(x * 100, y * 100, 100, 100);
                        }
                    }
                }
            }
        }

        public void Update(GameTime gt, KeyboardState kb, KeyboardState oldkb)
        {
            int x = player.X / 100;
            int y = player.Y / 100;
            if (kb.IsKeyUp(Keys.A) && oldkb.IsKeyDown(Keys.A) && grid[x - 1, y] != null)
            {
                player = grid[x - 1, y];
            }
            if (kb.IsKeyUp(Keys.S) && oldkb.IsKeyDown(Keys.S) && grid[x, y + 1] != null)
            {
                player = grid[x, y + 1];
            }
            if (kb.IsKeyUp(Keys.D) && oldkb.IsKeyDown(Keys.D) && grid[x + 1, y] != null)
            {
                player = grid[x + 1, y];
            }
            if (kb.IsKeyUp(Keys.W) && oldkb.IsKeyDown(Keys.W) && grid[x, y - 1] != null)
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

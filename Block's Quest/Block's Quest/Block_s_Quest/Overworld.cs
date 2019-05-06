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
    class Overworld
    {
        Texture2D background, playert;
        Rectangle player, prev;
        Path path;
        List<Level> levels = new List<Level>();
        Level currlevel;
        int x, y, bosslevelindex;
        int currlevelindex = 0;

        //Needed for level class
        public Overworld()
        {
        }

        //Basic Constructor
        public Overworld(Texture2D t, Texture2D p, Path pa, IServiceProvider Services)
        {
            x = 0;
            y = 0;
            background = t;
            playert = p;
            path = pa;
            player = pa.getStart();
            for(int i = 0; i < 20; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    if (path.isLevel(i, j))
                    {
                        levels.Add(path.load(i, j));
                    }
                }
            }
            bosslevelindex = levels.Count - 1;
        }

        public void Update(GameTime gt, KeyboardState kb, KeyboardState oldkb)
        {
            //Find location in grid of Dwayne icon
            x = player.X / 100;
            y = player.Y / 100;

            //When Dwayne moves, uses methods from path class to make sure that area has a road and then move dwayne to that spot
            if ((kb.IsKeyUp(Keys.A) && oldkb.IsKeyDown(Keys.A)) && path.check(x - 1, y))
            {
                player = path.move(x - 1, y);
            }
            else if ((kb.IsKeyUp(Keys.S) && oldkb.IsKeyDown(Keys.S)) && path.check(x, y + 1))
            {
                player = path.move(x, y + 1);
            }
            else if ((kb.IsKeyUp(Keys.D) && oldkb.IsKeyDown(Keys.D)) && path.check(x + 1, y))
            {
                player = path.move(x + 1, y);
            }
            else if ((kb.IsKeyUp(Keys.W) && oldkb.IsKeyDown(Keys.W)) && path.check(x, y - 1))
            {
                player = path.move(x, y - 1);
            }

            //Tracks current level index
            //if(isLevel())
            //{
            //    foreach(Level l in levels)
            //    {
            //        if (returnLevel() == l)
            //            currlevelindex = levels.IndexOf(l);
            //    }
            //}

            //Makes sure that dwaynes rectangle is never null because of old glitch where he would be able to move to an empty space and it would crash the game
                if (player == null)
                player = prev;
            prev = player;
        }

        public Level returnLevel()
        {
            return path.load(x, y);
        }

        public bool isLevel()
        {
            if (path.isLevel(x, y))
                return true;
            else
                return false;
        }
        
        public void deactivate()
        {
            path.deactivate(x, y);
        }

        public bool isActive()
        {
            return path.isActive(x, y);
        }

        public bool isBoss()
        {
            if (currlevelindex == bosslevelindex)
                return true;
            else
                return false;
        }

        public void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(playert, player, Color.White);
        }

    }
}

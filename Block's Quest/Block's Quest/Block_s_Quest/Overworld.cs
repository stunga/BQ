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
        int x, y;

        //Needed for level class
        public Overworld()
        {
        }

        //Basic Constructor
        public Overworld(Texture2D t, Texture2D p, Path pa, IServiceProvider Services)
        {
            background = t;
            playert = p;
            path = pa;
            int levelcounter = 1;
            player = pa.getStart();
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

        public void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(playert, player, Color.White);
        }

    }
}

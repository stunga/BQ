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
    class Path
    {
        Road[,] path;
        Rectangle[,] grid = new Rectangle[20, 10];
        Rectangle startPoint;

        public Path(Road[,] p)
        {
            path = p;

            //checks each element in path, if it's not null it adds rectangle data to a parallel array used for visual images and position of dwayne on the map
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (path[y, x] != null)
                    {
                        grid[y, x] = new Rectangle(y * 100, x * 100, 100, 100);
                        if (path[y, x].getType() == 0)
                        {
                            startPoint = grid[y, x];
                        }
                    }
                }
            }
        }

        //Used to find where dwayne icon needs to begin in overworld
        public Rectangle getStart()
        {
            return startPoint;
        }

        //Used to see if dwayne is able to move to a specified point on the overworld grid
        public Boolean check(int x, int y)
        {
            if (path[x, y] == null)
                return false;
            else
                return true;
        }

        //Used to see if specified road is a level node before attempting to load a level which may not exist
        public Boolean isLevel(int x, int y)
        {
            if (path[x, y].getType() == 2)
                return true;
            else
                return false;
        }

        public Level load(int x, int y)
        {
            return path[x, y].enterLevel();
        }

        //Used to access a rectangle value in the grid to set dwaynes position to when he moves
        public Rectangle move(int x, int y)
        {
            return grid[x, y];
        }

    }
}

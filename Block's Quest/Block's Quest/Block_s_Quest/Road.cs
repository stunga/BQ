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
    class Road
    {
        //GENERAL DATA
        enum TileType
        {
            startnode,
            path,
            levelnode
        };
        TileType tileType;
        Rectangle rec;

        //LEVEL NODE SPECIFIC DATA
        Level level;
        bool active = true;
        int index;

        //GENERAL METHODS
        //Default constructor
        public Road(int x, int y, int type)
        {
            rec = new Rectangle(x, y, 64, 64);
            if (type == 0)
                tileType = TileType.startnode;
            else if (type == 1)
                tileType = TileType.path;
            else
                tileType = TileType.levelnode;
        }

        public Rectangle getRec()
        {
            return rec;
        }

        //Returns what type of road it is
        public int getType()
        {
            if (tileType == TileType.startnode)
                return 0;
            else if (tileType == TileType.path)
                return 1;
            else
                return 2;
        }

        //LEVEL NODE SPECIFIC METHODS
        //Level node constructor
        public Road(int x, int y, int levelindex, IServiceProvider Services, Texture2D tex)
        {
            rec = new Rectangle(x, y, 64, 64);
            tileType = TileType.levelnode;
            ContentManager Content = new ContentManager(Services, "Content");
            index = levelindex;
            level = new Level(Services, @"Content/Levels/Level" + index + ".txt", Content.Load<Texture2D>("Tiles/Node"));
        }

        public void loadLevel(Level l)
        {
            level = l;
        }

        public void deactivate()
        {
            active = false;
        }

        public bool isActive()
        {
            return active;
        }

        public Level enterLevel()
        {
            return level;
        }

        public int returnIndex()
        {
            return index;
        }
    }
}

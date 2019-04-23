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
        enum TileType
        {
            startnode,
            path,
            levelnode
        };
        TileType tileType;
        Rectangle rec;

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

        public int getType()
        {
            if (tileType == TileType.startnode)
                return 0;
            else if (tileType == TileType.path)
                return 1;
            else
                return 2;
        }
    }
}

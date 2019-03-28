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
    class Diamond
    {
        public enum type
        {
            green,
            blue,
            yellow,
            red,
            purple,
            orange
        };
        type col;
        Rectangle rec;
        Texture tex;

        //Constructor
        public Diamond(int x, int y, Texture t, type c)
        {
            rec = new Rectangle(x - 10, y - 10, 20, 20);
            tex = t;
            col = c;
        }

        //Get the value of this diamond
        public int getValue()
        {
            if (col == type.green)
                return 1;
            if (col == type.blue)
                return 5;
            if (col == type.yellow)
                return 10;
            if (col == type.red)
                return 20;
            if (col == type.purple)
                return 50;
            if (col == type.orange)
                return 100;
            return 0;
        }
    }
}

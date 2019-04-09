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
            green=1,
            blue=5,
            yellow=10,
            red=20,
            purple=50,
            orange=100
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
            return (int)col;
        }
    }
}

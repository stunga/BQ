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
    public class Diamond
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
        Color color;
        int velocityX = 5, velocityY = 0;

        //Constructor
        public Diamond(int x, int y, Texture t, type c)
        {
            rec = new Rectangle(x - 10, y - 10, 20, 20);
            tex = t;
            col = c;
        }

        public void Update()
        {
            if (this.rec.Y < 960)
            {
                Double acceleration = 1;
                rec.Y += (velocityY / 9);
                rec.X += velocityX;
                velocityY += (int)acceleration;
                if (rec.X <= 0 || rec.X + rec.Width >= 1800)
                {
                    velocityX *= -1;
                }
            }
            else
            {
                velocityX = 0;
                velocityY = 0;    
            }
        }

        public Color getColor()
        {
            switch(col)
            {
                case type.green:
                    color = Color.Green;
                    break;
                case type.blue:
                    color = Color.Blue;
                    break;
                case type.yellow:
                    color = Color.Yellow;
                    break;
                case type.red:
                    color = Color.Red;
                    break;
                case type.purple:
                    color = Color.Purple;
                    break;
                case type.orange:
                    color = Color.Orange;
                    break;
            }
            return color;
        }

        public Rectangle getRect()
        {
            return rec;
        }

        //Get the value of this diamond
        public int getValue()
        {
            return (int)col;
        }
    }
}

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
        bool showing = true;
        Texture2D background;

        public Overworld(Texture2D t)
        {
            background = t;
        }

        public void Display()
        {
            showing = true;
        }

        public void Hide()
        {
            showing = false;
        }

        public void Draw(GameTime gt, SpriteBatch sb)
        {
            if(showing)
            {
                sb.Draw(background, new Rectangle(0, 0, 1800, 1000), Color.Gold);
            }
            else
            {

            }
        }
    }
}

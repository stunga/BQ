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
        Vector2 vel;

        public Overworld()
        {

        }

        public Overworld(Texture2D t, Texture2D p)
        {
            background = t;
            playert = p;
        }

        public void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(background, new Rectangle(0, 0, 1800, 1000), Color.Gold);
        }

        public void addLevel(LevelNode l)
        {

        }

    }
}

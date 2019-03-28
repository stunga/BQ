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
    class Bullet
    {
        Rectangle bulletRec;
        Texture2D bulletT;

        public Bullet(int x, int y, Texture2D t)
        {
            bulletRec = new Rectangle(x, y, 50, 50);
            bulletT = t;
        }

        public void Update()
        {
            bulletRec.Y -= 10;
        }

        public void Draw(SpriteBatch sb, GameTime gt)
        {
            sb.Draw(bulletT, bulletRec, Color.White);
        }
    }
}

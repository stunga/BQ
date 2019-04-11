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
        enum bullType
        {
            fire,
            chi,
            earth,
            water
        };

        bullType type = bullType.chi;
        Color col;

        public Bullet(int x, int y, Texture2D t)
        {
            bulletRec = new Rectangle(x, y, 20, 20);
            bulletT = t;
            type = bullType.chi;
            col = Color.LightBlue;
        }
        public Rectangle getRect()
        {
            return bulletRec;
        }
        public Bullet(int x, int y, Texture2D t, int i)
        {
            bulletRec = new Rectangle(x, y, 20, 20);
            bulletT = t;
            switch (i)
            {
                case 1:
                    type = bullType.chi;
                    col = Color.LightBlue;
                    break;
                case 2:
                    type = bullType.earth;
                    col = Color.Gray;
                    break;
                case 3:
                    type = bullType.fire;
                    col = Color.OrangeRed;
                    break;
                case 4:
                    type = bullType.water;
                    col = Color.DarkBlue;
                    break;
                default:
                    type = bullType.chi;
                    col = Color.LightBlue;
                    break;
            }

        }

        public void Update()
        {
            bulletRec.Y -= 10;
        }

        public void Draw(SpriteBatch sb, GameTime gt)
        {
            sb.Draw(bulletT, bulletRec, col);
        }
    }
}

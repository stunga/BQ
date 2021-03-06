﻿using System;
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
        int bulletDamage;
        enum bullType
        {
            fire=3,
            chi=1,
            earth=2,
            water=4
        };
        bool active = true;

        bullType type = bullType.chi;
        Color col;

        public Bullet(int x, int y, Texture2D t)
        {
            bulletRec = new Rectangle(x, y, 20, 20);
            bulletT = t;
            type = bullType.chi;
            col = Color.LightBlue;
        }

        public int getType()
        {
            return (int)type;
        }

        public Rectangle getRect()
        {
            return bulletRec;
        }
        public int getBulletDamage()
        {
            return this.bulletDamage;
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
            bulletDamage = 1;

        }

        //***USED TO PREVENT INFINITE DAMAGE WHILE BULLET IS INTERSECTING AN ENEMY***
        public bool isActive()
        {
            return active;
        }

        public void deactivate()
        {
            active = false;
        }
        //***************************************************************************

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

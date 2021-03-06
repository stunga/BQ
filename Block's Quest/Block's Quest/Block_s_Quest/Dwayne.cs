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
    class Dwayne
    {
        Rectangle rec;
        Texture2D tex, bulletT;
        KeyboardState oldKb;
        SoundEffect sound;
        List<Bullet> bullet = new List<Bullet>();
        int numBullets;
        //float vel;
        int bulletTimer = 0;
        int rate = 30;
        enum bullType
        {
            fire,
            chi,
            earth,
            water
        };
        bullType curType = bullType.chi;

        public Dwayne(Texture2D t, Texture2D bT, SoundEffect s, ContentManager content)
        {
            rec = new Rectangle(950, 875, 100, 100);
            tex = t;
            bulletT = bT;
            sound = s;
            numBullets = 1;
        }
        public List<Bullet> getBullets()
        {
            return bullet;
        }

        public bool isBehindDpad()
        {
            if (rec.X < 140)
                return true;
            return false;
        }

        //Checks for keypresses
        private void checkAction(KeyboardState kb, UI ui)
        {
            if (kb.IsKeyDown(Keys.A) && rec.X > 0)
                rec.X -= 20;
            if (kb.IsKeyDown(Keys.D) && rec.X + rec.Width < 1800)
                rec.X += 20;
            if (kb.IsKeyDown(Keys.Space) && bulletTimer == 0)
            {
                Shoot();
                sound.Play();
                bulletTimer = rate;
            }

            if (kb.IsKeyDown(Keys.Left))
            {
                curType = bullType.chi;
                ui.setCol(Color.Magenta, Color.Gray, Color.OrangeRed, Color.DarkBlue);
            }
            if (kb.IsKeyDown(Keys.Up))
            {
                curType = bullType.earth;
                ui.setCol(Color.LightBlue, Color.Magenta, Color.OrangeRed, Color.DarkBlue);
            }
            if (kb.IsKeyDown(Keys.Right))
            {
                curType = bullType.fire;
                ui.setCol(Color.LightBlue, Color.Gray, Color.Magenta, Color.DarkBlue);
            }
            if (kb.IsKeyDown(Keys.Down))
            {
                curType = bullType.water;
                ui.setCol(Color.LightBlue, Color.Gray, Color.OrangeRed, Color.Magenta);
            }

            if (isBehindDpad())
                ui.halfColor();
            else
                ui.fullColor();

            oldKb = kb;
        }

        public bool isDead(List<Enemy> enemies)
        {
            foreach(Enemy e in enemies)
            {
                if (e.isAlive)
                {
                    Rectangle r = e.getRect();
                    if (r.Intersects(rec))
                        return true;
                }
            }
            return false;
        }

        public void setPos(int x, int y)
        {
            rec.X = x;
            rec.Y = y;
        }

        public void Shoot()
        {
            int center = 40;
            int holder;

            if (numBullets == 4)
                holder = 10;
            else
                holder = center  / numBullets;

            for (int x = 0; x < numBullets; x++)
            {
                switch (curType)
                {
                    case bullType.chi:
                        bullet.Add(new Bullet(rec.X + holder, rec.Y - 10, bulletT, 1));
                        break;
                    case bullType.earth:
                        bullet.Add(new Bullet(rec.X + holder, rec.Y - 10, bulletT, 2));
                        break;
                    case bullType.fire:
                        bullet.Add(new Bullet(rec.X + holder, rec.Y - 10, bulletT, 3));
                        break;
                    case bullType.water:
                        bullet.Add(new Bullet(rec.X + holder, rec.Y - 10, bulletT, 4));
                        break;
                    default:
                        bullet.Add(new Bullet(rec.X + holder, rec.Y - 10, bulletT));
                        break;
                }
                if (numBullets == 4)
                {
                    holder += 30;
                }
                else
                {
                    if (numBullets % 2 == 0)
                    {
                        if (x % 2 == 0)
                            holder += center;
                        else
                            holder -= center;
                    }
                    else
                    {
                        holder += 30;
                    }
                }
            }
        }

        //Increases fire rate
        public void UpgradeFireRate()
        {
            if(rate-5!=0)
                rate--;
        }

        public int getFireRate()
        {
            return rate;
        }

        //Increase # of Bullets
        public void UpgradeNumBullets()
        {
            if (numBullets != 4)
                numBullets++;
        }

        public int getNumBullets()
        {
            return numBullets;
        }

        //Update
        public void Update(KeyboardState kb, UI ui)
        {
            checkAction(kb, ui);

            if(bulletTimer > 0)
                bulletTimer--;

            for (int x = 0; x < bullet.Count; x++)
                bullet[x].Update();
        }

        //Draw
        public void Draw(SpriteBatch sb, GameTime gt)
        {
            sb.Draw(tex, rec, Color.White);
            for (int x = 0; x < bullet.Count; x++)
            {
                if(bullet[x].isActive())
                    bullet[x].Draw(sb, gt);
            }
        }

        public Rectangle getRect()
        {
            return rec;
        }

    }
}

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
    class Dwayne
    {
        Rectangle rec;
        Texture2D tex, bulletT;
        KeyboardState oldKb;
        List<Bullet> bullet = new List<Bullet>();
        float vel;
        int bulletTimer = 0;
        enum bullType
        {
            fire,
            chi,
            earth,
            water
        };
        bullType curType = bullType.chi;

        public Dwayne(Texture2D t, Texture2D bT)
        {
            rec = new Rectangle(950, 875, 100, 100);
            tex = t;
            bulletT = bT;
        }
        public List<Bullet> getBullets()
        {
            return bullet;
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
                bulletTimer = 30;
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

            oldKb = kb;
        }

        private void Shoot()
        { 
            switch (curType)
            {
                case bullType.chi:
                    bullet.Add(new Bullet(rec.X + 40, rec.Y - 10, bulletT, 1));
                    break;
                case bullType.earth:
                    bullet.Add(new Bullet(rec.X + 40, rec.Y - 10, bulletT, 2));
                    break;
                case bullType.fire:
                    bullet.Add(new Bullet(rec.X + 40, rec.Y - 10, bulletT, 3));
                    break;
                case bullType.water:
                    bullet.Add(new Bullet(rec.X + 40, rec.Y - 10, bulletT, 4));
                    break;
                default:
                    bullet.Add(new Bullet(rec.X + 40, rec.Y - 10, bulletT));
                    break;
            }
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
                bullet[x].Draw(sb, gt);
        }

        public Rectangle getRect()
        {
            return rec;
        }

    }
}

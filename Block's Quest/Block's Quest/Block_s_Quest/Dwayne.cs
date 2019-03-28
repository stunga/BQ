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

        public Dwayne(Texture2D t, Texture2D bT)
        {
            rec = new Rectangle(950, 875, 100, 100);
            tex = t;
            bulletT = bT;
        }

        //Checks for keypresses
        private void checkAction(KeyboardState kb)
        {
            if ((kb.IsKeyDown(Keys.A) || kb.IsKeyDown(Keys.Left)) && rec.X > 0)
                rec.X -= 20;
            if ((kb.IsKeyDown(Keys.D) || kb.IsKeyDown(Keys.Right)) && rec.X + rec.Width < 1800)
                rec.X += 20;
            if (kb.IsKeyDown(Keys.Space) && !oldKb.IsKeyDown(Keys.Space))
                Shoot();

            oldKb = kb;
        }

        private void Shoot()
        {
            bullet.Add(new Bullet(rec.X+45, rec.Y, bulletT));
        }

        //Update
        public void Update(KeyboardState kb)
        {
            checkAction(kb);

            for (int x = 0; x < bullet.Count; x++)
                bullet[x].Update();
        }

        //Draw
        public void Draw(SpriteBatch sb, GameTime gt)
        {
            sb.Begin();
            sb.Draw(tex, rec, Color.White);
            for (int x = 0; x < bullet.Count; x++)
                bullet[x].Draw(sb, gt);
            sb.End();
        }

    }
}

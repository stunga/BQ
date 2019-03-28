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
        Texture2D tex;
        List<Bullet> bullet = new List<Bullet>();
        float vel;

        public Dwayne(Texture2D t)
        {
            rec = new Rectangle(950, 875, 100, 100);
            tex = t;
        }

        private void checkAction(KeyboardState kb)
        {
            if ((kb.IsKeyDown(Keys.A) || kb.IsKeyDown(Keys.Left)) && rec.X > 0)
                rec.X -= 20;
            if ((kb.IsKeyDown(Keys.D) || kb.IsKeyDown(Keys.Right)) && rec.X + rec.Width < 1800)
                rec.X += 20;
            if (kb.IsKeyDown(Keys.Space))
                Shoot();
        }

        private void Shoot()
        {
            Bullet shot = new Bullet(rec.X, rec.Y);
        }

        public void Update(KeyboardState kb)
        {
            checkAction(kb);
        }

        public void Draw(SpriteBatch sb, GameTime gt)
        {
            sb.Begin();
            sb.Draw(tex, rec, Color.White);
            sb.End();
        }

    }
}

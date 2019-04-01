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
    class UI
    {
        Rectangle diamonds, dpad, chi, fire, earth, water, shop, shopbutton, current, levelprogress, levelback;
        String diamondamount, progresstext, score, scoretext, currtext;
        Vector2 diamondamountloc, progresstextloc, scoreloc, scoretextloc, currtextloc;
        SpriteFont font;
        Texture2D element, shopt, diamondt, dpadt;
        bool isShowing = false;

        public UI(SpriteFont f, Texture2D e, Texture2D s, Texture2D d, Texture2D dp)
        {
            font = f;
            element = e;
            shopt = s;
            diamondt = d;
            diamonds = new Rectangle(0, 0, 50, 50);
            dpadt = dp;
            dpad = new Rectangle(30, 860, 120, 120);
            earth = new Rectangle(75, 880, 25, 20);
            water = new Rectangle(75, 940, 25, 20);
        }

        public void show()
        {
            isShowing = true;
        }

        public void hide()
        {
            isShowing = false;
        }

        public bool isDisplayed()
        {
            return isShowing;
        }

        public void Update(int s, int d)
        {
            score = s.ToString();
            diamondamount = d.ToString();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(diamondt, diamonds, Color.LightBlue);
            spriteBatch.Draw(element, earth, Color.Gray);
            spriteBatch.Draw(element, water, Color.DarkBlue);
            spriteBatch.Draw(dpadt, dpad, Color.White);
            spriteBatch.End();
        }

    }
}

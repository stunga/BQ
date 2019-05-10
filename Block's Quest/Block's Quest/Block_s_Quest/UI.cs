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
        Rectangle diamonds, dpad, chi, fire, earth, water, shop;
        public int diamondamount;
        public int score;
        Vector2 diamondamountloc, scoreloc;
        SpriteFont font;
        Texture2D element, shopt, diamondt, dpadt;
        Color firec, chic, waterc, earthc;
        bool isShowing = false;
        bool transparent = false;
        Wallet wallet;

        public UI(SpriteFont f, Texture2D e, Texture2D s, Texture2D d, Texture2D dp, Wallet w)
        {
            font = f;
            element = e;
            shopt = s;
            diamondt = d;
            diamonds = new Rectangle(0, 0, 50, 50);
            dpadt = dp;
            firec = Color.OrangeRed;
            chic = Color.Magenta;
            waterc = Color.DarkBlue;
            earthc = Color.Gray;
            diamondamountloc = new Vector2(50, 12);
            dpad = new Rectangle(40, 870, 100, 100);
            earth = new Rectangle(75, 880, 25, 20);
            water = new Rectangle(75, 940, 25, 20);
            chi = new Rectangle(50, 910, 25, 20);
            fire = new Rectangle(100, 910, 28, 20);
            shop = new Rectangle(800, 10, 100, 50);
            score = 0;
            scoreloc = new Vector2(1700, 20);
            wallet = w;
            diamondamount = wallet.getBalance();
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

        public void fullColor()
        {
            transparent = false;
        }

        public void halfColor()
        {
            transparent = true;
        }

        public void updateWallet(Wallet w)
        {
            wallet = w;
            diamondamount = wallet.getBalance();
        }

        public void Update(int s)
        {
            score = s;
        }

        public void setCol(Color c, Color e, Color f, Color w)
        {
            chic = c;
            earthc = e;
            firec = f;
            waterc = w;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (isShowing)
            {
                spriteBatch.Draw(diamondt, diamonds, Color.LightBlue);
                if(transparent)
                {
                    spriteBatch.Draw(element, earth, earthc * 0.25f);
                    spriteBatch.Draw(element, water, waterc * 0.25f);
                    spriteBatch.Draw(element, chi, chic * 0.25f);
                    spriteBatch.Draw(element, fire, firec * 0.25f);
                    spriteBatch.Draw(dpadt, dpad, Color.White * 0.25f);
                }
                else
                {
                    spriteBatch.Draw(element, earth, earthc);
                    spriteBatch.Draw(element, water, waterc);
                    spriteBatch.Draw(element, chi, chic);
                    spriteBatch.Draw(element, fire, firec);
                    spriteBatch.Draw(dpadt, dpad, Color.White);
                }
                spriteBatch.Draw(shopt, shop, Color.White);
                spriteBatch.DrawString(font, diamondamount.ToString(), diamondamountloc, Color.LightBlue);
                spriteBatch.DrawString(font, "Score: " + score.ToString(), scoreloc, Color.White);
            }
        }

    }
}

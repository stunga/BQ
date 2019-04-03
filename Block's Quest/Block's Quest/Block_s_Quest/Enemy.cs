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
    class Enemy
    {
        private Texture2D img;
        private int velocityX;
        private int velocityY;
        private Color color;
        private int hitpoints;
        private Rectangle rect;
        public Boolean isAlive = true;
        Boolean spawnCoins;
        public SpriteFont font;
        public Enemy(Texture2D i, int vX, Color col, int hitP, Rectangle r)
        {
            img = i;
            velocityX = vX;
            color = col;
            hitpoints = hitP;
            rect = r;
        }
        public int getHitpoints()
        {
            if (this.hitpoints <= 0)
            {
                isAlive = false;
            }
            else
            {
                isAlive = true;
            }
            return hitpoints;
        }
        public void enemyKilled()
        {
           if (!isAlive)
            {
                spawnCoins = true;
            }
        }
        private void ApplyPhysics()
        {
            Double acceleration = -9.8;
            velocityY = 0;
            rect.X += velocityX;
            rect.Y += velocityY;
            velocityY += (int)acceleration;
            if (rect.Y+rect.Height >= 1000 || rect.Y <= 0)
            {
                velocityY *= -1;
            }
            if (rect.X <= 0 || rect.X + rect.Width >= 1800)
            {
                velocityX *= -1;
            }
        }
        public void Update()
        {
            ApplyPhysics();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isAlive)
            {
                spriteBatch.Draw(img, rect, color);
            }
        }
    }
}

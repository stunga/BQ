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
        enum enemyType
        {
            fire=3,
            chi=1,
            earth=2,
            water=4,
            boss=5
        };
        enemyType type;
        private Texture2D img;
        private int velocityX;
        private int velocityY;
        private Color color;
        private int hitpoints;
        private Rectangle rect;
        public Boolean isAlive = true;
        Boolean spawnCoins;

        public Enemy(Texture2D i, int vX, int hitP, Rectangle r)
        {
            img = i;
            velocityX = vX;
            color = Color.LightBlue;
            hitpoints = hitP;
            rect = r;
            velocityY = 0;
            type = enemyType.chi;
        }
        public Enemy(Texture2D i, int vX, int hitP, Rectangle r, int t)
        {
            img = i;
            velocityX = vX;
            hitpoints = hitP;
            rect = r;
            velocityY = 0;

            switch(t)
            {
                case 1:
                    type = enemyType.chi;
                    color = Color.LightBlue;
                    break;
                case 2:
                    type = enemyType.earth;
                    color = Color.Gray;
                    break;
                case 3:
                    type = enemyType.fire;
                    color = Color.OrangeRed;
                    break;
                case 4:
                    type = enemyType.water;
                    color = Color.DarkBlue;
                    break;
            }
        }

        public void setType(int i)
        {
            switch (i)
            {
                case 1:
                    type = enemyType.chi;
                    color = Color.LightBlue;
                    break;
                case 2:
                    type = enemyType.earth;
                    color = Color.Gray;
                    break;
                case 3:
                    type = enemyType.fire;
                    color = Color.OrangeRed;
                    break;
                case 4:
                    type = enemyType.water;
                    color = Color.DarkBlue;
                    break;
                case 5:
                    type = enemyType.boss;
                    color = Color.White;
                    break;
            }
        }

        public int getType()
        {
            return (int)type;
        }

        public void decreaseHitPoints(int bulletDamage)
        {
            
            this.hitpoints -= bulletDamage;
            if (this.hitpoints <= 0)
            {
                isAlive = false;
            }
            else
            {
                isAlive = true;
            }
        }
        public Rectangle getRect()
        {
            return rect;
        }

        public bool living()
        {
            return isAlive;
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
            Double acceleration = 1;
            rect.X += velocityX;
            rect.Y += (velocityY/9);
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
            if (isAlive)
            {
                ApplyPhysics();
            }
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

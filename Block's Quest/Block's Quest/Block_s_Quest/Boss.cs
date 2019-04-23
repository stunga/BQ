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
    class Boss: Enemy
    {
        private int hitpoints;
        private Rectangle rectangle;
        private int velocityX;
        private int velocityY;
        private Texture2D image;
        public new bool isAlive = true;
        Boolean spawnCoins;
        int timer = 0;
        int counter = 0;
        Random rand = new Random();
        int randInt;
        Boolean move;
        public Boss(Texture2D i, int vX, Color col, int hitP, Rectangle r): base(i, vX, col, hitP, r)
        {
            image = i;
            hitpoints = hitP;
            rectangle = r;
            velocityX = vX;
            col = Color.White;
            velocityY = 10;
            rectangle.X = 850;
            rectangle.Y = 0;
        }
        public new void decreaseHitPoints(int bulletDamage)
        {
            this.hitpoints -= bulletDamage;
        }
        public new Rectangle getRect()
        {
            return rectangle;
        }
        public new int getHitpoints()
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

        public new void enemyKilled()
        {
            if (!isAlive)
            {
                spawnCoins = true;
            }
        }
        public new void Update()
        {
            randInt = rand.Next(0, 4);
            if (isAlive)
            {
                if (counter != randInt)
                {
                    if (move)
                    {
                        switch (counter)
                        {
                            case 0:
                                rectangle.X += velocityX;
                                rectangle.Y += velocityY;
                                if (rectangle.Y >= 450)
                                {
                                    counter = 1;
                                }
                                break;
                            case 1:
                                rectangle.X -= velocityX;
                                rectangle.Y += velocityY;
                                if (rectangle.Y >= 950)
                                {
                                    counter = 2;
                                }
                                break;
                            case 2:
                                rectangle.X -= velocityX;
                                rectangle.Y -= velocityY;
                                if (rectangle.Y <= 450)
                                {
                                    counter = 3;
                                }
                                break;
                            case 3:
                                rectangle.X += velocityX;
                                rectangle.Y -= velocityY;
                                if (rectangle.Y <= 0)
                                {
                                    counter = 0;
                                }
                                break;
                        }
                    }

                }
                else
                {
                    move = false;
                    timer++;
                }
                if (timer >= 180)
                {
                    timer = 0;
                    move = true;
                }
            }
        }

        public new void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isAlive)
            {
                spriteBatch.Draw(image, rectangle, Color.White);
            }
        }
    }
}

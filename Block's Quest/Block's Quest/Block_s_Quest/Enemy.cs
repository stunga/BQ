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
        private int velocityX;
        private int velocityY;
        private Color color;
        private int hitpoints;
        private int size;
        private 
        public Enemy(int vX, int vY, Color col, int hitP, int s)
        {
            velocityX = vX;
            velocityY = vY;
            color = col;
            hitpoints = hitP;
            size = s;
        }
        public int getHitpoints()
        {
            return hitpoints;
        }
        public int getSize()
        {
            return size;
        }
        public void ApplyPhysics()
        {

        }
        public void Update()
        {

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
           
        }
    }
}

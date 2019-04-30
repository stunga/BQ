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
    class LevelNode : Road
    {
        Level level;
        bool active = true;

        public LevelNode(int x, int y) : base(x, y, 2)
        {

        }

        public LevelNode(int x, int y, int index, IServiceProvider Services, Texture2D tex) : base(x, y, 2)
        {
            ContentManager Content = new ContentManager(Services, "Content");
            Level l = new Level(Services, @"Content/Levels/Level" + index + ".txt", Content.Load<Texture2D>("Tiles/Node"));
        }

        public void loadLevel(Level l)
        {
            level = l;
        }

        public void deactivate()
        {
            active = false;
        }

        public bool isActive()
        {
            return active;
        }

        public Level enterLevel()
        {
            return level;
        }

        new public Rectangle getRec()
        {
            return base.getRec();
        }

    }
}

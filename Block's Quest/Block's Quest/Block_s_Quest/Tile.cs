using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Block_s_Quest
{

    class Tile
    {
        public string TileSheetName;
        public int TileSheetIndex;

        public const int Width = 64;
        public const int Height = 64;
        public static readonly Vector2 Size = new Vector2(Width, Height);

        public Tile(string n, int i)
        {
            TileSheetName = n;
            TileSheetIndex = i;
            Collision = c;
        }
    }
}

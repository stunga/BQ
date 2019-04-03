﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Block_s_Quest
{
    class Level
    {
        public int levelIndex;
        private Tile[,] tiles;
        private Dictionary<string, Texture2D> tileSheets;
        public Dictionary<int, Rectangle> TileSourceRecs;
        public List<Rectangle> TileDefinitions;

        private List<Enemy> enemies = new List<Enemy>();
        private List<Enemy> deadEnemies = new List<Enemy>();

        public Dwayne Player
        {
            get { return player; }
        }
        Dwayne player;

        private Vector2 start;

        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        private const int TileWidth = 64;
        private const int TileHeight = 64;
        private const int TilesPerRow = 5;
        private const int NumRowsPerSheet = 5;

        private Random random = new Random(1337);

        public int Width
        {
            get { return tiles.GetLength(0); }
        }

        public int Height
        {
            get { return tiles.GetLength(1); }
        }

        //private List<Collectable> collectables = new List<Collectable>();
        //private List<Collectable> collectedCollectables = new List<Collectable>();

        public Level(IServiceProvider serviceProvider, string path)
        {
            levelIndex = 1;
            content = new ContentManager(serviceProvider, "Content");

            tileSheets = new Dictionary<string, Texture2D>();
            //tileSheets.Add("Blocks", Content.Load<Texture2D>("Tiles/Blocks"));

            TileSourceRecs = new Dictionary<int, Rectangle>();
            for (int i = 0; i < TilesPerRow * NumRowsPerSheet; i++)
            {
                Rectangle rectTile = new Rectangle(
                    (i % TilesPerRow) * TileWidth,
                    (i / TilesPerRow) * TileHeight,
                    TileWidth,
                    TileHeight);
                TileSourceRecs.Add(i, rectTile);
            }
            LoadTiles(path);
        }

        private void LoadTiles(string path)
        {
            int numOfTilesAcross = 0;
            List<string> lines = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line = reader.ReadLine();
                    numOfTilesAcross = line.Length;
                    while (line != null)
                    {
                        lines.Add(line);
                        int nextLineWidth = line.Length;
                        if (nextLineWidth != numOfTilesAcross)
                            throw new Exception(String.Format(
                                "The length of line {0} is different from all preceeding lines.",
                                lines.Count));
                        line = reader.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            tiles = new Tile[numOfTilesAcross, lines.Count];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    string currentRow = lines[y];
                    char tileType = currentRow[x];
                    tiles[x, y] = LoadTile(tileType, x, y);
                }
            }
        }

        private Tile LoadTile(char tileType, int x, int y)
        {
            switch (tileType)
            {
                case '.':
                    return new Tile(String.Empty, 0);
                //Player spawn
                case '+':
                    return LoadStartTile(x, y);
                //Enemies spawns
                case 'e':
                    return LoadEnemyTile(x, y, "e");

                default:
                    throw new NotSupportedException(String.Format(
                        "Unsupported til type character '{0}' at position {1}, {2}.", tileType, x, y));
            }
        }

        private Tile LoadEnemyTile(int _x, int _y, string _enemy)
        {
            Vector2 position = new Vector2((_x * 64) + 48, (_y * 64) + 20);
            //enemies.Add(new Enemy(this, position, _enemy));
            return new Tile(String.Empty, 0);
        }

        private Tile LoadStartTile(int x, int y)
        {
            if (Player != null)
                throw new NotSupportedException("A level may only have one starting point.");

            start = new Vector2((x * 64) + 48, (y * 64) + 16);
           //player = new Dwayne(this, start);
            return new Tile(String.Empty, 0);
        }

        public void Draw(GameTime _gameTime, SpriteBatch _spriteBatch)
        {
            DrawTiles(_spriteBatch);
            player.Draw(_spriteBatch,_gameTime);
            foreach (Enemy enemy in enemies)
                enemy.Draw(_gameTime, _spriteBatch);
        }

        private void DrawTiles(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (tileSheets.ContainsKey(tiles[x, y].TileSheetName))
                    {
                        Vector2 position = new Vector2(x, y) * Tile.Size;
                        spriteBatch.Draw(
                            tileSheets[tiles[x, y].TileSheetName],
                            position,
                            TileSourceRecs[tiles[x, y].TileSheetIndex],
                            Color.White);
                    }
                }
            }
        }

        public bool LevelEnd()
        {
            foreach(Enemy e in enemies)
            {
                if (e.isAlive)
                    return false;
            }

            return true;
        }
    }
}
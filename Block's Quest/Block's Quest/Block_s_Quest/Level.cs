﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace Block_s_Quest
{
    class Level
    {
        public Texture2D enemyT, bossT;
        private Tile[,] tiles;
        private Dictionary<string, Texture2D> tileSheets;
        public Dictionary<int, Rectangle> TileSourceRecs;
        private List<Enemy> enemies = new List<Enemy>();
        private List<Enemy> deadEnemies = new List<Enemy>();
        public Rectangle bossRect = new Rectangle();
        private Overworld ow = new Overworld();
        Road[,] path = new Road[20, 10];
        int levelIndex = 1;
        IServiceProvider Services;

        private Vector2 start;

        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        private const int TileWidth = 100;
        private const int TileHeight = 100;
        private const int TilesPerRow = 20;
        private const int NumRowsPerSheet = 10;

        private Random random = new Random(1337);

        public int Width
        {
            get { return tiles.GetLength(0); }
        }

        public int Height
        {
            get { return tiles.GetLength(1); }
        }

        public Level(IServiceProvider serviceProvider, string path, Texture2D eT)
        {
            Services = serviceProvider;
            content = new ContentManager(serviceProvider, "Content");
            enemyT = eT;
            bossT = Content.Load<Texture2D>("Boss");
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

        public Level(IServiceProvider serviceProvider, string path)
        {
            Services = serviceProvider;
            content = new ContentManager(serviceProvider, "Content");

            tileSheets = new Dictionary<string, Texture2D>();
            tileSheets.Add("Road", Content.Load<Texture2D>("Tiles/Road"));
            tileSheets.Add("Node", Content.Load<Texture2D>("Tiles/Node"));
            tileSheets.Add("Start", Content.Load<Texture2D>("Bullet"));

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
                //Enemies spawns
                case 'e':
                    return LoadEnemyTile(x, y, "e");
                //Road
                case 'r':
                    return LoadVarietyTile("Road", x, y);
                //Node
                case 'l':
                    return LoadVarietyTile("Node", x, y);
                //Start
                case '+':
                    return LoadStartTile(x, y);
               //Boss
                case 'b':
                    return LoadBossTile(x, y, "b");
                default:
                    throw new NotSupportedException(String.Format(
                        "Unsupported til type character '{0}' at position {1}, {2}.", tileType, x, y));
            }
        }

        private Tile LoadEnemyTile(int _x, int _y, string _enemy)
        {
            Vector2 position = new Vector2((_x * 64) + 48, (_y * 180) + 64);
            enemies.Add(new Enemy(enemyT, 5, Color.Green, 10, new Rectangle(_x*80,_y*100, 50, 50)));
            return new Tile(String.Empty, 0);
        }

        private Tile LoadVarietyTile(String tileSheetName, int x, int y)
        {
            if (tileSheetName.Equals("Node"))
            {
                LevelNode ln = new LevelNode(x, y, levelIndex, Services, Content.Load<Texture2D>("Tiles/Node"));
                path[x, y] = ln;
                levelIndex++;
            }
            if(tileSheetName.Equals("Road"))
            {
                Road r = new Road(x, y, 1);
                path[x, y] = r;
            }

            return new Tile(tileSheetName, 0);

        }
        
        public void spawnEnemy(Rectangle bossRec)
        {
            enemies.Add(new Enemy(enemyT, 5, Color.Green, 10, new Rectangle(bossRec.X, bossRec.Y + 70, 50, 50)));
        }
        private Tile LoadStartTile(int x, int y)
        {
            Road r = new Road(x, y, 0);
            path[x, y] = r;
            start = new Vector2((x * 64) + 48, (y * 64) + 64);
            return new Tile("Start", 0);
        }

        private Tile LoadBossTile (int _x, int _y, string _enemy)
        {
            Vector2 position = new Vector2((_x * 64) + 48, (_y * 180) + 64);
            enemies.Add(new Boss(bossT, 11, Color.White, 25, new Rectangle(450,200, 100, 100)));
            return new Tile(String.Empty, 0);
        }
        public void Draw(GameTime _gameTime, SpriteBatch _spriteBatch)
        {
            DrawTiles(_spriteBatch);
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
        public List<Enemy> getEnemies()
        {
            return enemies;
        }

        //Update
        public void Update()
        {
            Type t;
            foreach (Enemy e in enemies)
            {
                e.Update();
                t = e.GetType();
                if (t.Equals(typeof(Boss)))
                {
                    bossRect = e.getRect();
                }
            }
            
        }

        //Level changer
        public bool LevelEnd()
        {
            foreach(Enemy e in enemies)
            {
                if (e.isAlive)
                    return false;
            }
            return true;
        }

        //Return 2D Array or Roads:
        public Tile[,] getTile()
        {
            return tiles;
        }

        public Road[,] getPath()
        {
            return path;
        }
    }
}

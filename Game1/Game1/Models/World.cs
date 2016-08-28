using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace Game1
{
    public class World
    {
        public Tile[,] tiles;
        public System.Drawing.Color[,] level;
        public string levelName;
        public string levelFilePath;
        public Vector2 size;

        public World(string _levelFilePath, string _levelName)
        {
            levelFilePath = _levelFilePath;
            levelName = _levelName;
        }

        public void CreateWorld()
        {
            level = LoadLevel();

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    Tile newTile = Tiles.GetTile(level[x,y]);
                    newTile.position.X = x * 32;
                    newTile.position.Y = y * 32;
                    tiles[x,y] = newTile;
                }
            }
        }

        public System.Drawing.Color[,] LoadLevel()
        {
            Debug.WriteLine("Image file path: " + levelFilePath);
            Bitmap image = new Bitmap(levelFilePath);

            System.Drawing.Color[,] tempArray = new System.Drawing.Color[image.Width, image.Height];
            tiles = new Tile[image.Width, image.Height];
            size.X = image.Width;
            size.Y = image.Height;

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    tempArray[x,y] = image.GetPixel(x, y);
                }
            }

            return tempArray;
        }

        public Tile GetTileAt(int x, int y)
        {
            return tiles[x, y];
        }

        public void SetTile(int x, int y, Tile tile)
        {
            tiles[x, y] = tile;
        }
    }
}

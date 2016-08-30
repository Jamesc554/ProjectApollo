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

namespace ProjectApollo
{
    public class World
    {
        public Tile[,] tiles;
        public Entity character;
        public Entity character1;
        public Entity character2;
        public Entity character3;
        public Entity character4;
        public System.Drawing.Color[,] level;
        public string levelName;
        public string levelFilePath;
        public Vector2 size;
        public Path_TileGraph tileGraph;

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
                    newTile.position.X = x;
                    newTile.position.Y = y;
                    tiles[x,y] = newTile;
                }
            }


            character = new Entity(Tiles.GetTile(2).spriteLocation, new Vector2(), 0, tiles[1,1]);
            character1 = new Entity(Tiles.GetTile(2).spriteLocation, new Vector2(), 0, tiles[1,10]);
            character2 = new Entity(Tiles.GetTile(2).spriteLocation, new Vector2(), 0, tiles[24,4]);
            character3 = new Entity(Tiles.GetTile(2).spriteLocation, new Vector2(), 0, tiles[6,6]);
            character4 = new Entity(Tiles.GetTile(2).spriteLocation, new Vector2(), 0, tiles[10,10]);
            character.destinationTile = tiles[51, 3];
            character1.destinationTile = tiles[51, 3];
            character2.destinationTile = tiles[51, 3];
            character3.destinationTile = tiles[51, 3];
            character4.destinationTile = tiles[51, 3];
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

        public Tile GetTileAt(int x, int y, bool worldCoords = false)
        {
            if (worldCoords)
            {
                Debug.WriteLine("Mouse Clicked at: X:" + x + " Y:" + y);
                Debug.WriteLine("Mouse Clicked at: X:" + x + " Y:" + y);

                x = x / 32;
                y = y / 32;

                Debug.WriteLine("Mouse Clicked at: X:" + x + " Y:" + y);
                Debug.WriteLine("Mouse Clicked at: X:" + x + " Y:" + y);
            }

            if (x >= size.X || x < 0 || y >= size.Y || y < 0)
            {
                //Debug.LogError("Tile ("+x+","+y+") is out of range.");
                return null;
            }

            return tiles[x, y];
        }

        public void SetTile(int x, int y, Tile tile)
        {
            tiles[x, y] = tile;
        }
    }
}

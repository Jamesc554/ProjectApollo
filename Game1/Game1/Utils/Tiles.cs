using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Xml;
using System.Diagnostics;

namespace Game1
{
    public class Tiles
    {
        private static List<Tile> tiles = new List<Tile>();
        private static Dictionary<string, int> tileIdDic = new Dictionary<string, int>();
        private static Dictionary<System.Drawing.Color, int> tileColorDic = new Dictionary<System.Drawing.Color, int>();

        public static void ReadFromXML(string filePath, string spriteFilePath)
        {
            XmlTextReader reader = new XmlTextReader(filePath);

            while (reader.Read())
            {
                if (reader.GetAttribute("R") != null)
                {
                    string tileFilePath;
                    tileFilePath = spriteFilePath + reader.GetAttribute("spriteLocation") + reader.GetAttribute("spriteFile");

                    Debug.WriteLine("Tile loaded: " + reader.GetAttribute("spriteFile"));
                    Tile newTile = new Tile(tileFilePath);
                    System.Drawing.Color color = System.Drawing.Color.FromArgb(255, int.Parse(reader.GetAttribute("R")), int.Parse(reader.GetAttribute("G")), int.Parse(reader.GetAttribute("B")));
                    AddTile(newTile, color);
                }
            }
        }

        public static void AddTile(Tile tile, System.Drawing.Color color)
        {
            tileColorDic.Add(color, tiles.Count);
            tiles.Add(tile);
        }

        public static Tile GetTile(String tileName)
        {
            Tile oldTile = tiles[tileIdDic[tileName]];
            Tile newTile = new Tile(oldTile.spriteLocation);
            return newTile;
        }

        public static Tile GetTile(int tileId)
        {
            Tile oldTile = tiles[tileId];
            Tile newTile = new Tile(oldTile.spriteLocation);
            return newTile;
        }

        public static Tile GetTile(System.Drawing.Color color)
        {
            Tile oldTile = tiles[tileColorDic[color]];
            Tile newTile = new Tile(oldTile.spriteLocation);
            return newTile;
        }
    }
}

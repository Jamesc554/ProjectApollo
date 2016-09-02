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

namespace ProjectApollo
{
    public class Tiles
    {
        private static List<Tile> tiles = new List<Tile>();
        private static Dictionary<string, int> tileIdDic = new Dictionary<string, int>();
        private static Dictionary<System.Drawing.Color, int> tileColorDic = new Dictionary<System.Drawing.Color, int>();

        public static void ReadFromXML(string filePath, string spriteFilePath)
        {
            XmlTextReader reader = new XmlTextReader(filePath);
            Tile newTile = null;
            System.Drawing.Color color = new System.Drawing.Color();
            string spriteLocation = "";
            string tileName = "";

            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case ("name"):
                        reader.Read();
                        newTile = null;
                        tileName = reader.ReadContentAsString();
                        break;
                    case ("spriteFolder"):
                        reader.Read();
                        spriteLocation = spriteFilePath + reader.ReadContentAsString() + "//";
                        break;
                    case ("spriteFile"):
                        reader.Read();
                        spriteLocation += reader.ReadContentAsString();

                        newTile = new Tile(spriteLocation);
                        break;
                    case ("movementCost"):
                        reader.Read();
                        newTile.movementCost = reader.ReadContentAsInt();
                        break;
                    case ("color"):
                        if (reader.IsStartElement())
                            color = System.Drawing.Color.FromArgb(255, int.Parse(reader["R"]), int.Parse(reader["G"]), int.Parse(reader["B"]));
                        break;
                    case ("buildTime"):
                        reader.Read();
                        newTile.buildingTime = reader.ReadContentAsFloat();
                        AddTile(newTile, color, tileName);
                        break;
                    //case ("BuildingRequirements"):

                    //    List<string> invs = new List<string>();

                    //    XmlReader inventoryReader = reader.ReadSubtree();

                    //    while (inventoryReader.Name == "item")
                    //    {
                    //        invs.Add(inventoryReader.GetAttribute("name") + inventoryReader.GetAttribute("amount"));
                    //    }

                    //    break;
                }

                if (newTile != null)
                {
                }
            }

            //while (reader.Read())
            //{
            //    if (reader.GetAttribute("R") != null)
            //    {
            //        string tileFilePath;
            //        tileFilePath = spriteFilePath + reader.GetAttribute("spriteLocation") + reader.GetAttribute("spriteFile");

            //        Debug.WriteLine("Tile loaded: " + reader.GetAttribute("spriteFile"));
            //        Tile newTile = new Tile(tileFilePath);
            //        newTile.movementCost = int.Parse(reader.GetAttribute("movementCost"));
            //        System.Drawing.Color color = System.Drawing.Color.FromArgb(255, int.Parse(reader.GetAttribute("R")), int.Parse(reader.GetAttribute("G")), int.Parse(reader.GetAttribute("B")));
            //        AddTile(newTile, color);
            //    }
            //}
        }

        public static void AddTile(Tile tile, System.Drawing.Color color, string name)
        {
            tileColorDic.Add(color, tiles.Count);
            tileIdDic.Add(name, tiles.Count);
            tiles.Add(tile);
        }

        public static Tile GetTile(String tileName)
        {
            Tile oldTile = tiles[tileIdDic[tileName]];
            Tile newTile = new Tile(oldTile.spriteLocation);
            newTile.movementCost = oldTile.movementCost;
            return (Tile)oldTile.Clone();
        }

        public static Tile GetTile(int tileId)
        {
            Tile oldTile = tiles[tileId];
            Tile newTile = new Tile(oldTile.spriteLocation);
            newTile.movementCost = oldTile.movementCost;
            return (Tile)oldTile.Clone();
        }

        public static Tile GetTile(System.Drawing.Color color)
        {
            Tile oldTile = tiles[tileColorDic[color]];
            Tile newTile = new Tile(oldTile.spriteLocation);
            newTile.movementCost = oldTile.movementCost;
            return (Tile)oldTile.Clone();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1
{
    public class Mod
    {
        public List<World> levels = new List<World>();

        public string modRef;
        public string modName;
        public string modDesc;
        public string modAuthor;
        public string modVersion;
        public string modGameVersion;

        public string modFileLocation;
        public string levelFileLocation;
        public string spriteFileLocation;
        public string luaFileLocation;
        public string xmlFileLocation;

        public Mod(string filePath)
        {
            modFileLocation = filePath;
            LoadMod(filePath);
            LoadTiles();
            LoadLevels();
        }

        public void LoadMod(string filePath)
        {
            XmlTextReader reader = new XmlTextReader(filePath + "//package.xml");

            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case ("ModRef"):
                        reader.Read();
                        modRef = reader.ReadContentAsString();
                        break;
                    case ("ModName"):
                        reader.Read();
                        modName = reader.ReadContentAsString();
                        break;
                    case ("Description"):
                        reader.Read();
                        modDesc = reader.ReadContentAsString();
                        break;
                    case ("Author"):
                        reader.Read();
                        modAuthor = reader.ReadContentAsString();
                        break;
                    case ("ModVersion"):
                        reader.Read();
                        modVersion = reader.ReadContentAsString();
                        break;
                    case ("GameVersion"):
                        reader.Read();
                        modGameVersion = reader.ReadContentAsString();
                        break;
                    case ("LevelFileLocation"):
                        reader.Read();
                        levelFileLocation = reader.ReadContentAsString();
                        break;
                    case ("SpriteFileLocation"):
                        reader.Read();
                        spriteFileLocation = reader.ReadContentAsString();
                        break;
                    case ("LuaFileLocation"):
                        reader.Read();
                        luaFileLocation = reader.ReadContentAsString();
                        break;
                    case ("XmlFileLocation"):
                        reader.Read();
                        xmlFileLocation = reader.ReadContentAsString();
                        break;
                }
            }
        }

        public void LoadTiles()
        {
            Tiles.ReadFromXML(modFileLocation + xmlFileLocation + "//tiles.xml", modFileLocation + spriteFileLocation + "//");
        }

        public void LoadLevels()
        {
            int i = 0;
            DirectoryInfo d = new DirectoryInfo(modFileLocation + levelFileLocation);
            FileInfo[] files = d.GetFiles("*.png");
            foreach (FileInfo file in files)
            {
                World newWorld = new World(file.FullName, "level_" + i);
                newWorld.CreateWorld();
                levels.Add(newWorld);
                Debug.WriteLine("Level Added");
                i++;
            }
        }

    }
}

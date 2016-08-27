using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class WorldController
    {
        public World world;
        public static WorldController instance;
        public ContentManager content;

        public WorldController()
        {
            instance = this;
        }
    }
}

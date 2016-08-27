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
        public int worldSizeX, worldSizeY;

        public WorldController()
        {
            instance = this;
        }

        public void SetWorld(World _world)
        {
            world = _world;
            worldSizeX = (int)_world.size.X;
            worldSizeY = (int)_world.size.Y;
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void UnloadContent(ContentManager content)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}

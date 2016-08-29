using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ProjectApollo
{
    public class WorldController
    {
        public World world;
        public static WorldController instance;
        public ContentManager content;
        public int worldSizeX, worldSizeY;

        public WorldController(ContentManager _content)
        {
            instance = this;
            content = _content;
        }

        public void SetWorld(World _world)
        {
            world = _world;
            worldSizeX = (int)_world.size.X;
            worldSizeY = (int)_world.size.Y;
        }

        public void LoadContent(ContentManager content)
        {
            foreach (Tile t in world.tiles)
            {
                t.LoadContent(content);
            }
        }

        public void UnloadContent(ContentManager content)
        {
            foreach (Tile t in world.tiles)
            {
                t.UnloadContent(content);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Tile t in world.tiles)
            {
                t.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile t in world.tiles)
            {
                t.Draw(spriteBatch);
            }
        }
    }
}

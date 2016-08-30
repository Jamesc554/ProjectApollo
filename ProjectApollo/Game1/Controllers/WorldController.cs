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

            world.character.LoadContent(content);
            world.character1.LoadContent(content);
            world.character2.LoadContent(content);
            world.character3.LoadContent(content);
            world.character4.LoadContent(content);
        }

        public void UnloadContent(ContentManager content)
        {
            foreach (Tile t in world.tiles)
            {
                t.UnloadContent(content);
            }

            world.character.UnloadContent(content);
            world.character1.UnloadContent(content);
            world.character2.UnloadContent(content);
            world.character3.UnloadContent(content);
            world.character4.UnloadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Tile t in world.tiles)
            {
                t.Update(gameTime);
            }

            world.character.Update(gameTime);
            world.character1.Update(gameTime);
            world.character2.Update(gameTime);
            world.character3.Update(gameTime);
            world.character4.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile t in world.tiles)
            {
                t.Draw(spriteBatch);
            }

            world.character.Draw(spriteBatch);
            world.character1.Draw(spriteBatch);
            world.character2.Draw(spriteBatch);
            world.character3.Draw(spriteBatch);
            world.character4.Draw(spriteBatch);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using MoonSharp.Interpreter;

namespace ProjectApollo
{
    [MoonSharpUserData]
    public class WorldController
    {
        public World world;

        public static WorldController instance;
        public MouseController mouseController;

        public ContentManager content;
        public int worldSizeX, worldSizeY;

        public WorldController(ContentManager _content)
        {
            instance = this;
            content = _content;

            mouseController = new MouseController();
        }


         //Stuff
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
                t.LoadContent();
            }

            world.character.LoadContent();
            world.character1.LoadContent();
            world.character2.LoadContent();
            world.character3.LoadContent();
            world.character4.LoadContent();

            world.buttonBuild.LoadContent();
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

            world.buttonBuild.UnloadContent(content);
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

            world.buttonBuild.Update(gameTime);
        }

        public void HandleInput(InputState inputState)
        {
            Vector2 mousePos = new Vector2(inputState.CurrentMouseState.Position.X, inputState.CurrentMouseState.Position.Y);
            MouseState ms = new MouseState();
            if (inputState.IsNewLeftMouseClick(out ms))
            {
                world.character.destinationTile = world.GetTileAt((int)ProjectApollo.camera.ScreenToWorld(mousePos).X, (int)ProjectApollo.camera.ScreenToWorld(mousePos).Y, true);
            }

            mouseController.HandleInput(inputState);
            world.buttonBuild.HandleInput(inputState);
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

            world.buttonBuild.Draw(spriteBatch);
        }
    }
}

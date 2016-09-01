using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace ProjectApollo
{
    [MoonSharpUserData]
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ProjectApollo : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public List<Mod> mods;
        public static readonly Camera camera = new Camera();
        private InputState inputState;

        WorldController worldController;

        public ProjectApollo()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1080,
                SynchronizeWithVerticalRetrace = false
                
            };
            graphics.ApplyChanges();
            //this.IsFixedTimeStep = false;
            this.IsMouseVisible = true;
            Content.RootDirectory = "Content";

            inputState = new InputState();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()    
        {
            UserData.RegisterAssembly();

            // TODO: Add your initialization logic here
            worldController = new WorldController(this.Content);
            mods = LoadMods(); // Load all the mods from the mads folder.
            worldController.SetWorld(mods[0].levels[1]); // Set the current level to the first mods second level.

            camera.viewportWidth = graphics.GraphicsDevice.Viewport.Width;
            camera.viewportHeight = graphics.GraphicsDevice.Viewport.Height;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            worldController.LoadContent(this.Content);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content heres
            worldController.UnloadContent(this.Content);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            inputState.Update();
            camera.HandleInput(inputState, null);
            worldController.HandleInput(inputState);
            worldController.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.TranslationMatrix);

            worldController.Draw(spriteBatch);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public List<Mod> LoadMods()
        {
            List<Mod> mods = new List<Mod>();
            DirectoryInfo d = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory +  "//Mods");
            DirectoryInfo[] dList = d.GetDirectories();
            foreach(DirectoryInfo dir in dList)
            {
                Mod newMod = new Mod(dir.FullName);
                mods.Add(newMod);
                Debug.WriteLine("Mod Loaded");
            }

            return mods;
        }
    }
}

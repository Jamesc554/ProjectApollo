using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApollo
{
    public class MouseController
    {
        public Tile currentTile;
        Vector2 mousePos;

        public void Place(int x, int y)
        {
            if (currentTile != null)
            {
                WorldController.instance.world.SetTile(x, y, currentTile);
                currentTile = null;
            }
            else
            {

            }
        }

        public void Update(GameTime gameTime)
        {
            if (currentTile != null)
            {
                currentTile.position.X = mousePos.X;
                currentTile.position.Y = mousePos.Y;
            }
        }

        public void HandleInput(InputState inputState)
        {
            mousePos = new Vector2(inputState.CurrentMouseState.Position.X, inputState.CurrentMouseState.Position.Y);
            MouseState ms = new MouseState();

            PlayerIndex pi = new PlayerIndex();

            if (inputState.IsNewKeyPress(Keys.B,null,out pi))
            {
                currentTile = Tiles.GetTile("brick");
                Debug.WriteLine("B has been pressed, Movement Cost: " + currentTile.movementCost);
            }

            if (inputState.IsNewKeyPress(Keys.T, null, out pi))
            {
                currentTile = Tiles.GetTile("tile");
            }

            if (inputState.IsNewLeftMouseClick(out ms))
            {
                if ((currentTile != null && WorldController.instance.world.GetTileAt((int)ProjectApollo.camera.ScreenToWorld(mousePos).X, (int)ProjectApollo.camera.ScreenToWorld(mousePos).Y, true) != null))
                {
                    Place((int)ProjectApollo.camera.ScreenToWorld(mousePos).X / 32, (int)ProjectApollo.camera.ScreenToWorld(mousePos).Y / 32);
                }
            }
        }
    }
}

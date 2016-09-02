using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApollo
{
    [MoonSharpUserData]
    public class Button : GameObject
    {
        public Action<Button> onClicked;
        public Action<Button> onUpdate;
        public string luaClickedFunction;
        public string luaUpdateFunction;

        public Button(string _spriteLocation, float _angle = 0) : base(_spriteLocation, new Vector2(), _angle)
        {

        }

        public bool EnterButton(Vector2 mousePos)
        {
            Vector2 buttonPos = ProjectApollo.camera.WorldToScreen(new Vector2(X, Y));
            
            if (mousePos.X < buttonPos.X + sprite.Width &&
                    mousePos.X > buttonPos.X &&
                    mousePos.Y < buttonPos.Y + sprite.Height &&
                    mousePos.Y > buttonPos.Y)
            {
                return true;
            }
            return false;
        }

        public void HandleInput(InputState inputState)
        {
            Vector2 mousePos = new Vector2(inputState.CurrentMouseState.Position.X, inputState.CurrentMouseState.Position.Y);
            MouseState ms = new MouseState();
            if (EnterButton(mousePos) && inputState.IsNewLeftMouseClick(out ms))
            {
                Debug.WriteLine("Button has been clicked");
                onClicked(this);
            }
        }

        public override void Update(GameTime gameTime)
        {
            onUpdate?.Invoke(this);
        }

    }
}

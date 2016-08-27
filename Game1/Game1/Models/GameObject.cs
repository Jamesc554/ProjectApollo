using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    public class GameObject
    {
        private Texture2D sprite;
        private string spriteLocation;
        private Vector2 position;
        private float angle; // Value between 0 - 360 (Degrees)

        public GameObject(string _spriteLocation, Vector2 _position, float _angle = 0)
        {
            spriteLocation = _spriteLocation;
            position = _position;
            angle = _angle;
        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(spriteLocation);
        }

        public void UnloadContent(ContentManager content)
        {
            content.Unload();
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, new Rectangle(0, 0, sprite.Width, sprite.Height), Color.White, angle, new Vector2(sprite.Width / 2, sprite.Height / 2), 1.0f, SpriteEffects.None, 1);
        }
    }
}

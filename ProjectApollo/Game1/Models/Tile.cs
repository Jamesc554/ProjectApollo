using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectApollo
{
    public class Tile : GameObject
    {
        public int movementCost = 1;
        public int timesSteppedOn;

        public Tile(string _spriteLocation, float _angle = 0) : base(_spriteLocation, new Vector2(), _angle)
        {

        }

        public Tile[] GetNeighbours(bool diagOkay = false)
        {
            Tile[] ns;

            if (diagOkay == false)
            {
                ns = new Tile[4];   // Tile order: N E S W
            }
            else
            {
                ns = new Tile[8];   // Tile order : N E S W NE SE SW NW
            }

            Tile n;

            n = WorldController.instance.world.GetTileAt(X, Y + 1);
            ns[0] = n;  // Could be null, but that's okay.
            n = WorldController.instance.world.GetTileAt(X + 1, Y);
            ns[1] = n;  // Could be null, but that's okay.
            n = WorldController.instance.world.GetTileAt(X, Y - 1);
            ns[2] = n;  // Could be null, but that's okay.
            n = WorldController.instance.world.GetTileAt(X - 1, Y);
            ns[3] = n;  // Could be null, but that's okay.

            if (diagOkay == true)
            {
                n = WorldController.instance.world.GetTileAt(X + 1, Y + 1);
                ns[4] = n;  // Could be null, but that's okay.
                n = WorldController.instance.world.GetTileAt(X + 1, Y - 1);
                ns[5] = n;  // Could be null, but that's okay.
                n = WorldController.instance.world.GetTileAt(X - 1, Y - 1);
                ns[6] = n;  // Could be null, but that's okay.
                n = WorldController.instance.world.GetTileAt(X - 1, Y + 1);
                ns[7] = n;  // Could be null, but that's okay.
            }

            return ns;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 newPos = new Vector2((position.X * 32) + (sprite.Width / 2), (position.Y * 32) + (sprite.Height / 2));
            spriteBatch.Draw(sprite, newPos, new Rectangle(0, 0, sprite.Width, sprite.Height), Color.White, angle, new Vector2(sprite.Width / 2, sprite.Height / 2), 1.0f, SpriteEffects.None, 1);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1
{
    public class Tile : GameObject
    {
        public Tile(string _spriteLocation, float _angle = 0) : base(_spriteLocation, new Vector2(), _angle)
        {

        }

        public Tile[] GetNeighbours()
        {
            Tile[] tiles = new Tile[8];
            World world = WorldController.instance.world;
            int i = 0;

            for (int xx = (int)position.X - 1; xx < (int)position.X + 1; xx++)
            {
                for (int yy = (int)position.Y - 1; yy < (int)position.Y + 1; yy++)
                {
                    if (world.GetTileAt(xx, yy) != null)
                    {
                        if (!(xx == 0 && yy == 0))
                        {
                            tiles[i] = world.GetTileAt(xx, yy);
                            i++;
                        }
                    }
                }
            }

            return tiles;
        }
    }
}

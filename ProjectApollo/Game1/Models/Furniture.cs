using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApollo
{
    public class Furniture : GameObject
    {
        public Furniture(string _spriteLocation, float _angle = 0) : base(_spriteLocation, new Vector2(), _angle)
        {

        }
    }
}

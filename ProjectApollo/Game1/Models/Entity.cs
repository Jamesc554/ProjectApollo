using Microsoft.Xna.Framework;
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
    public class Entity : GameObject
    {
        public Vector2 velocity;
        public float speed;
        public Tile currentTile;
        public Tile destinationTile;
        public Tile nextTile;
        public Job currentJob;

        private Path_AStar pathAStar;
        float movementPrecentage;

        public Entity(string _spriteLocation, Vector2 position, float _angle, Tile tile) : base(_spriteLocation, position, _angle)
        {
            speed = 10f;
            currentTile = destinationTile = nextTile = tile;
        }

        public override void Update(GameTime gameTime)
        {
            Update_DoMovement(gameTime);
            position.X = currentTile.position.X * 32;
            position.Y = currentTile.position.Y * 32;
        }

        public void Update_DoMovement(GameTime gameTime)
        {
            if (currentTile == destinationTile)
            {
                pathAStar = null;
                return;
            }

            if (nextTile == null || nextTile == currentTile)
            {
                if (pathAStar == null || pathAStar.Length() == 0)
                {
                    pathAStar = new Path_AStar(WorldController.instance.world, currentTile, destinationTile);
                    if (pathAStar.Length() == 0)
                    {
                        //Debug.WriteLine("THE PATH IS EMPTY");
                        return;
                    }

                    //Debug.WriteLine("Moving to new tile, X: " + nextTile.position.X + " Y: " + nextTile.position.Y);
                    nextTile = pathAStar.Dequeue();
                }

                nextTile = pathAStar.Dequeue();
            }

            float distToTravel = (float)Math.Sqrt(
                                 Math.Pow(currentTile.position.X - nextTile.position.X, 2) +
                                 Math.Pow(currentTile.position.Y - nextTile.position.Y, 2));

            float distThisFrame = speed / nextTile.movementCost * (float)gameTime.ElapsedGameTime.TotalSeconds;
            float precThisFrame = distThisFrame / distToTravel;

            movementPrecentage += precThisFrame;

            if (movementPrecentage >= 1)
            {
                //Debug.WriteLine("Moving to new tile, X: " + nextTile.position.X + " Y: " + nextTile.position.Y);

                currentTile = nextTile;
                movementPrecentage = 0;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApollo
{
    public class Path_AStar
    {
        private Queue<Tile> path;

        public Path_AStar(Queue<Tile> path)
        {
            if (path == null || !path.Any())
            {

            }

            this.path = path;
        }

        public Path_AStar(World world, Tile tileStart, Tile tileEnd)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (world.tileGraph == null)
            {
                world.tileGraph = new Path_TileGraph(world);
            }

            Dictionary<Tile, Path_Node<Tile>> nodes = world.tileGraph.nodes;

            if (nodes.ContainsKey(tileStart) == false)
            {
                Debug.WriteLine("Path_AStar: The starting tile isn't in the list of nodes!");
                return;
            }

            Path_Node<Tile> start = nodes[tileStart];
            Path_Node<Tile> goal = null;
            if (tileEnd != null)
            {
                if (nodes.ContainsKey(tileEnd) == false)
                {
                    Debug.WriteLine("Path_AStar: The ending tile isn't in the list of nodes!");
                    return;
                }

                goal = nodes[tileEnd];
            }

            HashSet<Path_Node<Tile>> closedSet = new HashSet<Path_Node<Tile>>();
            SimplePriorityQueue<Path_Node<Tile>> openSet = new SimplePriorityQueue<Path_Node<Tile>>();
            openSet.Enqueue(start, 0);

            Dictionary<Path_Node<Tile>, Path_Node<Tile>> cameFrom = new Dictionary<Path_Node<Tile>, Path_Node<Tile>>();

            Dictionary<Path_Node<Tile>, float> gScore = new Dictionary<Path_Node<Tile>, float>();
            gScore[start] = 0;

            Dictionary<Path_Node<Tile>, float> fScore = new Dictionary<Path_Node<Tile>, float>();
            fScore[start] = Heuristic_cost_estimate(start, goal);

            while (openSet.Count > 0)
            {
                Path_Node<Tile> current = openSet.Dequeue();
                Debug.WriteLine("Next Node: X:" + current.data.position.X + " Y:" + current.data.position.Y);

                if (goal != null)
                {
                    if (current == goal)
                    {
                        reconstructPath(cameFrom, current);
                        return;
                    }
                }

                closedSet.Add(current);
                
                foreach(Path_Edge<Tile> edgeNeighbour in current.edges)
                {
                    Path_Node<Tile> neighbour = edgeNeighbour.node;

                    if (closedSet.Contains(neighbour))
                        continue;

                    float movementCostToNeighbour = neighbour.data.movementCost * Dist_between(current, neighbour);
                    float tentativeGScore = gScore[current] + movementCostToNeighbour;

                    if (openSet.Contains(neighbour) && tentativeGScore >= gScore[neighbour])
                        continue;

                    cameFrom[neighbour] = current;
                    gScore[neighbour] = tentativeGScore;
                    fScore[neighbour] = gScore[neighbour] + Heuristic_cost_estimate(neighbour, goal);

                    if (openSet.Contains(neighbour) == false)
                    {
                        openSet.Enqueue(neighbour, fScore[neighbour]);
                    }
                    else
                    {
                        openSet.UpdatePriority(neighbour, fScore[neighbour]);
                    }
                }
            }
        }

        public Tile Dequeue()
        {
            if (path == null)
            {
                Debug.WriteLine("Attempting to dequeue from an null path.");
                return null;
            }

            if (path.Count <= 0)
            {
                Debug.WriteLine("what???");
                return null;
            }

            Tile t = path.Dequeue();

            Debug.WriteLine("New tile selected X:" + t.position.X + " Y: " + t.position.Y);

            return t;
        }

        public int Length()
        {
            if (path == null)
                return 0;

            return path.Count;
        }

        public Tile EndTile()
        {
            if (path == null || path.Count == 0)
            {
                Debug.WriteLine("Path is null or empty.");
                return null;
            }

            return path.Last();
        }

        private float Heuristic_cost_estimate(Path_Node<Tile> a, Path_Node<Tile> b)
        {
            if (b == null)
            {
                return 0f;
            }

            return (float)Math.Sqrt(Math.Pow(a.data.position.X - b.data.position.X, 2) + Math.Pow(a.data.position.Y - b.data.position.Y, 2));
        }

        private float Dist_between(Path_Node<Tile> a, Path_Node<Tile> b)
        {
            // We can make assumptions because we know we're working
            // on a grid at this point.

            // Hori/Vert neighbours have a distance of 1
            if (Math.Abs(a.data.position.X - b.data.position.X) + Math.Abs(a.data.position.Y - b.data.position.Y) == 1)
            {
                return 1f;
            }

            // Diag neighbours have a distance of 1.41421356237
            if (Math.Abs(a.data.position.X - b.data.position.X) == 1 && Math.Abs(a.data.position.Y - b.data.position.Y) == 1)
            {
                return 1.4f;
            }

            // Otherwise, do the actual math.
            return (float)Math.Sqrt(
                Math.Pow(a.data.position.X - b.data.position.X, 2) +
                Math.Pow(a.data.position.Y - b.data.position.Y, 2));
        }

        private void reconstructPath(Dictionary<Path_Node<Tile>, Path_Node<Tile>> cameFrom, Path_Node<Tile> current)
        {
            Queue<Tile> totalPath = new Queue<Tile>();
            totalPath.Enqueue(current.data);

            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Enqueue(current.data);
                Debug.WriteLine("Moving to new tile, X: " + current.data.position.X + " Y: " + current.data.position.Y);
            }


            path = new Queue<Tile>(totalPath.Reverse());
            Debug.WriteLine("Path size: " + path.Count);
        }
    }
}

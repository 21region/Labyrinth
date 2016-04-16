using System;
using System.Collections.Generic;

using Labyrinth.Labyrinth;

namespace Labyrinth.MazeGenerator
{
    public class MazeGeneratorUtils
    {
        public static List<Coordinate.Direction> getUnvisitedWays(bool[,] vis, Coordinate coord)
        {
            int x = coord.X;
            int y = coord.Y;
            List<Coordinate.Direction> res = new List<Coordinate.Direction>();

            if (x > 0 && vis[x - 1, y] == false) res.Add(Coordinate.Direction.LEFT);
            if (y > 0 && vis[x, y - 1] == false) res.Add(Coordinate.Direction.UP);
            if (x < vis.GetLength(0) - 1 && vis[x + 1, y] == false) res.Add(Coordinate.Direction.RIGHT);
            if (y < vis.GetLength(1) - 1 && vis[x, y + 1] == false) res.Add(Coordinate.Direction.DOWN);
            return res;
        }

        public static bool isVisited(bool[,] vis, int x, int y)
        {
            if (x < 0 || y < 0 || x >= vis.GetLength(0) || y >= vis.GetLength(1)) return false;
            return vis[x, y];
        }
    }
}

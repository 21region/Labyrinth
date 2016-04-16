using System;
using System.Collections.Generic;

using Labyrinth.Labyrinth;

namespace Labyrinth.MazeGenerator
{
    class BackTrackGenerator : IMazeGenerator
    {
        public Labyrinth generate(int size)
        {
            Labyrinth lab = new Labyrinth(size);
            bool[,] visited = new bool[size, size];
            Stack<Coordinate> stack = new Stack<Coordinate>();
            Random rnd = new Random();
        
            stack.Push(new Coordinate(rnd.Next(size), rnd.Next(size)));
        
            while(stack.Count > 0){
                Coordinate coord = stack.Peek();
                visited[coord.X, coord.Y] = true;
            
                List<Coordinate.Direction> dirs = MazeGeneratorUtils.getUnvisitedWays(visited, coord);
                if(dirs.Count > 0){
                    int ind = rnd.Next(dirs.Count);
                    Coordinate.Direction dir = dirs[ind];
                    lab.breakWall(coord, dir);
                    stack.Push(new Coordinate(coord, dir));
                }else{
                    stack.Pop();
                } 
            }
            return lab;
        }
    }
}

using System;

using Labyrinth.Labyrinth;

namespace Labyrinth.MazeGenerator
{
    class DivideEtImperaGenerator
    {
        public Labyrinth generate(int size)
        {
            Labyrinth lab = new Labyrinth(size);
            divideEtImpera(lab, 0, 0, size - 1, size - 1, new Random());
            return lab;
        }

        private static void divideEtImpera(Labyrinth lab, int x1, int y1, int x2, int y2, Random rnd)
        {
            if (x2 - x1 == 0)
            {
                for (int i = y1; i < y2; i++)
                {
                    lab.breakWall(x1, i, Coordinate.Direction.DOWN);
                }
                return;
            }

            if (y2 - y1 == 0)
            {
                for (int i = x1; i < x2; i++)
                {
                    lab.breakWall(i, y1, Coordinate.Direction.RIGHT);
                }
                return;
            }

            int xLine, yLine;
            xLine = rnd.Next(x2 - x1) + x1;
            yLine = rnd.Next(y2 - y1) + y1;

            int wallInd = rnd.Next(4);
            
            if (wallInd != 0)
            {
                int portY = rnd.Next(yLine - y1 + 1) + y1;
                lab.breakWall(xLine, portY, Coordinate.Direction.RIGHT);
            }
 
            if (wallInd != 1)
            {
                int portX = rnd.Next(x2 - xLine) + xLine + 1;
                lab.breakWall(portX, yLine, Coordinate.Direction.DOWN);
            }
            
            if (wallInd != 2)
            {
                int portY = rnd.Next(y2 - yLine) + yLine + 1;
                lab.breakWall(xLine, portY, Coordinate.Direction.RIGHT);
            }
            
            if (wallInd != 3)
            {
                int portX = rnd.Next(xLine - x1 + 1) + x1;
                lab.breakWall(portX, yLine, Coordinate.Direction.DOWN);
            }

            divideEtImpera(lab, x1, y1, xLine, yLine, rnd);
            divideEtImpera(lab, xLine + 1, y1, x2, yLine, rnd);
            divideEtImpera(lab, x1, yLine + 1, xLine, y2, rnd);
            divideEtImpera(lab, xLine + 1, yLine + 1, x2, y2, rnd);
        }
    }
}

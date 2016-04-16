using System;
using Labyrinth.Labyrinth;
using System.Collections.Generic;

namespace Labyrinth.MazeGenerator
{

    class Labyrinth
    {
        public delegate int GameEndedDelegate();

        private GameEndedDelegate callBack;
        public GameEndedDelegate CallBack
        {
            set { callBack = value; }
        }

        private int size;
        public int Size
        {
            get { return size; }
        }
        private Cell[][] labyrinth;
        private Coordinate playerCoordinate;
        private bool gameInProgress = false;

        public Coordinate getPlayerCoordinate()
        {
            return playerCoordinate;
        }


        public Labyrinth(int size)
        {
            this.size = size;
            labyrinth = new Cell[size][];
            for (int i = 0; i < size; i++)
            {
                labyrinth[i] = new Cell[size];
                for (int j = 0; j < size; j++)
                {
                    labyrinth[i][j] = new Cell();
                }
            }
        }
        
        public void initializeGame()
        {
            gameInProgress = true;
            Tuple<Coordinate, Coordinate> pts = findDiametr();
            getCell(pts.Item1).content = Cell.CellContent.PLAYER;
            playerCoordinate = pts.Item1;

            getCell(pts.Item2).content = Cell.CellContent.GOAL;
        }
        
        public void movePlayer(Coordinate.Direction dir)
        {
            if (gameInProgress)
            {
                //check whether we can move this direction
                if (!checkWall(playerCoordinate, dir))
                {
                    getCell(playerCoordinate).content = Cell.CellContent.NONE;

                    playerCoordinate = new Coordinate(playerCoordinate, dir);
                    if (getCell(playerCoordinate).content == Cell.CellContent.GOAL)
                    {
                        callBack();
                        gameInProgress = false;
                    }
                    getCell(playerCoordinate).content = Cell.CellContent.PLAYER;
                }
            }
        }

        public Cell getCell(int x, int y)
        {
            return labyrinth[x][y];
        }

        public Cell getCell(Coordinate coord)
        {
            return labyrinth[coord.X][coord.Y];
        }

        public void breakWall(int x, int y, Coordinate.Direction dir)
        {
            switch (dir)
            {
                case Coordinate.Direction.UP:
                    labyrinth[x][y - 1].down = false;
                    break;

                case Coordinate.Direction.RIGHT:
                    labyrinth[x][y].right = false;
                    break;

                case Coordinate.Direction.DOWN:
                    labyrinth[x][y].down = false;
                    break;

                case Coordinate.Direction.LEFT:
                    labyrinth[x - 1][y].right = false;
                    break;
            }
        }

        public void breakWall(Coordinate coord, Coordinate.Direction dir)
        {
            breakWall(coord.X, coord.Y, dir);
        }

        public bool checkWall(int x, int y, Coordinate.Direction dir)
        {
            switch (dir)
            {
                case Coordinate.Direction.UP:
                    if (y > 0 && labyrinth[x][y - 1].down == false) return false;
                    break;

                case Coordinate.Direction.RIGHT:
                    if (labyrinth[x][y].right == false) return false;
                    break;

                case Coordinate.Direction.DOWN:
                    if (labyrinth[x][y].down == false) return false;
                    break;

                case Coordinate.Direction.LEFT:
                    if (x > 0 && labyrinth[x - 1][y].right == false) return false;
                    break;
            }
            return true;
        }

        public bool checkWall(Coordinate coord, Coordinate.Direction dir)
        {
            return checkWall(coord.X, coord.Y, dir);
        }

        public Coordinate farestCell(Coordinate begin)
        {
            int[,] length = new int[this.size, this.size];
            int inf = this.size * this.size + 1;
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    length[i, j] = inf;
                }
            }
            length[begin.X, begin.Y] = 0;

            Queue<Coordinate> cells = new Queue<Coordinate>();
            int maxPath = 0;
            Coordinate farestPoint = begin;

            cells.Enqueue(begin);
            while (cells.Count != 0)
            {
                Coordinate curCoord = cells.Dequeue();
                int x = curCoord.X;
                int y = curCoord.Y;
                int curLength = length[x, y];
                if (curLength > maxPath)
                {
                    farestPoint = curCoord;
                    maxPath = curLength;
                }
                if (!checkWall(curCoord, Coordinate.Direction.UP))
                {
                    if (length[x, y - 1] > curLength + 1)
                    {
                        cells.Enqueue(new Coordinate(curCoord, Coordinate.Direction.UP));
                        length[x, y - 1] = curLength + 1;
                    }
                }

                if (!checkWall(curCoord, Coordinate.Direction.RIGHT))
                {
                    if (length[x + 1, y] > curLength + 1)
                    {
                        cells.Enqueue(new Coordinate(curCoord, Coordinate.Direction.RIGHT));
                        length[x + 1, y] = curLength + 1;
                    }
                }

                if (!checkWall(curCoord, Coordinate.Direction.DOWN))
                {
                    if (length[x, y + 1] > curLength + 1)
                    {
                        cells.Enqueue(new Coordinate(curCoord, Coordinate.Direction.DOWN));
                        length[x, y + 1] = curLength + 1;
                    }
                }

                if (!checkWall(curCoord, Coordinate.Direction.LEFT))
                {
                    if (length[x - 1, y] > curLength + 1)
                    {
                        cells.Enqueue(new Coordinate(curCoord, Coordinate.Direction.LEFT));
                        length[x - 1, y] = curLength + 1;
                    }
                }
            }

            return farestPoint;
        }

        public Tuple<Coordinate, Coordinate> findDiametr()
        {
            Coordinate diamCell1 = farestCell(new Coordinate(0, 0));
            Coordinate diamCell2 = farestCell(diamCell1);
            Tuple<Coordinate, Coordinate> res = new Tuple<Coordinate, Coordinate>(diamCell1, diamCell2);
            return res;
        }
    }
}

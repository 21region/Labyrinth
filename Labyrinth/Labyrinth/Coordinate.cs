using System;

namespace Labyrinth.Labyrinth
{
    class Coordinate
    {
        public enum Direction { UP, RIGHT, DOWN, LEFT }

        private int x, y;
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Coordinate(Coordinate from, Direction dir)
        {
            switch (dir)
            {
                case Direction.UP:
                    this.x = from.x;
                    this.y = from.y - 1;
                    break;

                case Direction.RIGHT:
                    this.x = from.x + 1;
                    this.y = from.y;
                    break;

                case Direction.DOWN:
                    this.x = from.x;
                    this.y = from.y + 1;
                    break;

                case Direction.LEFT:
                    this.x = from.x - 1;
                    this.y = from.y;
                    break;
            }
        }
    }
}

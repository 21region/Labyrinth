using System;


namespace Labyrinth.Labyrinth
{
    public class Cell
    {
        public enum CellContent { NONE, PLAYER, GOAL }

        public bool down = true,
                    right = true;

        public CellContent content = CellContent.NONE;

        public Cell() { }
        public Cell(bool down, bool right)
        {
            this.down = down;
            this.right = right;
        }
    }
}

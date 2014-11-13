using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class GameEngine
    {
        int size;
        public enum CellSelection { N, O, X };
        private int[,] grid;
        private bool playerWon = false; // This will be set correctly when the game is won
        public GameEngine(int i)
        {
            this.size = i;
            this.grid = new int[3, 3];
        }

        public bool gameOver()
        {
            for (int i = 0; i < this.size; i++)
            {
                int row = grid[i, 0] + grid[i, 1] + grid[i, 2];
                int col = grid[0, i] + grid[1, i] + grid[2, i];
                if (row == 3 || col == 3)
                {
                    return true;
                }
                else if (row == 12 || col == 12)
                {
                    this.playerWon = true;
                    return true;
                }
            }
            int cross1 = grid[0, 0] + grid[1, 1] + grid[2, 2];
            int cross2 = grid[0, 2] + grid[1, 1] + grid[2, 0];
            if (cross1 == 3 || cross2 == 3)
            {
                return true;
            }
            else if (cross1 == 12 || cross2 == 12)
            {
                this.playerWon = true;
                return true;
            }
            return false;
        }

        public void clearBoard()
        {
            this.grid = new int[3, 3];
        }

        public int[,] getGrid()
        {
            return this.grid;
        }

        public int getValue(CellSelection cs)
        {
            switch (cs)
            {
                case CellSelection.N:
                    return 0;
                case CellSelection.O:
                    return 1;
                case CellSelection.X:
                    return 4;
                default:
                    return 0;
            }
        }

        public bool makePlayerMove(int i, int j)
        {
            if (this.grid[i, j] != 0) return false;
            else
            {
                this.grid[i, j] = 4;
                return true;
            }
        }

        public int[] makeCompMove()
        {
            // For now, just emulate this stuff. Look for the first open spot
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    if (this.grid[i, j] == 0)
                    {
                        this.grid[i, j] = 1;
                        int[] returnArr = new int[2];
                        returnArr[0] = i; returnArr[1] = j;
                        return returnArr;
                    }
                }
            }
            return null;
        }

        public bool didPlayerWin()
        {
            return this.playerWon;
        }


    }
}

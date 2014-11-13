using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                    MessageBox.Show("You lost!");
                    return true;
                }
                else if (row == 12 || col == 12)
                {
                    this.playerWon = true;
                    MessageBox.Show("You won!");
                    return true;
                }
            }
            int cross1 = grid[0, 0] + grid[1, 1] + grid[2, 2];
            int cross2 = grid[0, 2] + grid[1, 1] + grid[2, 0];
            if (cross1 == 3 || cross2 == 3)
            {
                MessageBox.Show("You lost!");
                return true;
            }
            else if (cross1 == 12 || cross2 == 12)
            {
                MessageBox.Show("You won!");
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

        public bool makePlayerMove(int i, int j)
        {
            if (this.grid[i, j] != 0) return false;
            else
            {
                this.grid[i, j] = 4;
                return true;
            }
        }

        public void makeCompMove()
        {
            // For now, just emulate this stuff. Look for the first open spot
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    if (this.grid[j, i] == 0)
                    {
                        this.grid[j, i] = 1;
                        return;
                    }
                }
            }
        }

        public bool didPlayerWin()
        {
            return this.playerWon;
        }


    }
}

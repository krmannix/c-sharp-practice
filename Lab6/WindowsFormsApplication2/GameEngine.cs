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
        private int[,] grid;
        private bool playerWon = false; // This will be set correctly when the game is won
        private bool gameOver = false;
        private Form window;
        private Random rnd = new Random();
        public GameEngine(Form f, int i)
        {
            this.size = i;
            this.grid = new int[3, 3];
            this.window = f;
        }

        public bool gameOverCheck()
        {
            bool[] boardFull = new bool[3];
            for (int i = 0; i < this.size; i++)
            {
                boardFull[i] = (grid[i, 0] > 0) && (grid[i, 1] > 0) && (grid[i, 2] > 0);
                int row = grid[i, 0] + grid[i, 1] + grid[i, 2];
                int col = grid[0, i] + grid[1, i] + grid[2, i];
                if (row == 3 || col == 3)
                {
                    MessageBox.Show("You lost!");
                    this.gameOver = true;
                    return true;
                }
                else if (row == 12 || col == 12)
                {
                    this.playerWon = true;
                    this.gameOver = true;
                    MessageBox.Show("You won!");
                    return true;
                }
            }
            int cross1 = grid[0, 0] + grid[1, 1] + grid[2, 2];
            int cross2 = grid[0, 2] + grid[1, 1] + grid[2, 0];
            if (cross1 == 3 || cross2 == 3)
            {
                MessageBox.Show("You lost!");
                this.gameOver = true;
                return true;
            }
            else if (cross1 == 12 || cross2 == 12)
            {
                MessageBox.Show("You won!");
                this.playerWon = true;
                this.gameOver = true;
                return true;
            }
            if (boardFull[0] && boardFull[1] && boardFull[2])
            {
                this.gameOver = true;
                MessageBox.Show("You tied...");
            }
            return false;
        }

        public void clearBoard()
        {
            this.gameOver = false;
            this.grid = new int[3, 3];
            window.Invalidate();
        }

        public int[,] getGrid()
        {
            return this.grid;
        }

        public void makePlayerMove(int i, int j)
        {
            if (this.grid[i, j] != 0) MessageBox.Show("Illegal Move!");
            else
            {
                this.grid[i, j] = 4;
                window.Invalidate();
                this.gameOverCheck();
                if (!this.gameOver)
                {
                    this.makeCompMove();
                }
            }
        }

        public void makeCompMove()
        {
            this.gameOverCheck();

            // Column and row check
            for (int i = 0; i < this.size; i++)
            {
                if ((this.grid[i, 0] + this.grid[i, 1] + this.grid[i, 2]) == 2) // This means there are exactly 2 spots filled
                {
                    for (int j = 0; j < this.size; j++)
                    {
                        if (this.grid[i, j] == 0)
                        {
                            this.grid[i, j] = 1;
                            window.Invalidate();
                            this.gameOverCheck();
                            return;
                        }
                    }
                }
                if ((this.grid[0, i] + this.grid[1, i] + this.grid[2, i]) == 2)
                {
                    for (int j = 0; j < this.size; j++)
                    {
                        if (this.grid[j, i] == 0)
                        {
                            this.grid[j, i] = 1;
                            window.Invalidate();
                            this.gameOverCheck();
                            return;
                        }
                    }
                }
            }
            for (int i = 0; i < this.size; i++)
            {
                if ((this.grid[i, 0] + this.grid[i, 1] + this.grid[i, 2]) == 8) // This means there are exactly 2 spots filled
                {
                    for (int j = 0; j < this.size; j++)
                    {
                        if (this.grid[i, j] == 0)
                        {
                            this.grid[i, j] = 1;
                            window.Invalidate();
                            this.gameOverCheck();
                            return;
                        }
                    }
                }
                if ((this.grid[0, i] + this.grid[1, i] + this.grid[2, i]) == 8)
                {
                    for (int j = 0; j < this.size; j++)
                    {
                        if (this.grid[j, i] == 0)
                        {
                            this.grid[j, i] = 1;
                            window.Invalidate();
                            this.gameOverCheck();
                            return;
                        }
                    }
                }
            }
            // Diagonal win check
            if ((this.grid[0, 0] + this.grid[1, 1] + this.grid[2, 2]) == 2)
            {
                for (int i = 0; i < this.size; i++)
                {
                    if (this.grid[i, i] == 0)
                    {
                        this.grid[i, i] = 1;
                        window.Invalidate();
                        this.gameOverCheck();
                        return;
                    }
                }
            }
            if ((this.grid[2, 0] + this.grid[1, 1] + this.grid[0, 2]) == 2)
            {
                for (int i = 0; i < this.size; i++)
                {
                    if (this.grid[this.size - 1 - i, i] == 0)
                    {
                        this.grid[this.size - 1 - i, i] = 1;
                        window.Invalidate();
                        this.gameOverCheck();
                        return;
                    }
                }
            }
            if ((this.grid[0, 0] + this.grid[1, 1] + this.grid[2, 2]) == 8)
            {
                for (int i = 0; i < this.size; i++)
                {
                    if (this.grid[i, i] == 0)
                    {
                        this.grid[i, i] = 1;
                        window.Invalidate();
                        this.gameOverCheck();
                        return;
                    }
                }
            }
            if ((this.grid[2, 0] + this.grid[1, 1] + this.grid[0, 2]) == 8)
            {
                for (int i = 0; i < this.size; i++)
                {
                    if (this.grid[this.size - 1 - i, i] == 0)
                    {
                        this.grid[this.size - 1 - i, i] = 1;
                        window.Invalidate();
                        this.gameOverCheck();
                        return;
                    }
                }
            }
            // Now, randomly choose a spot.
            int k = 0, h = 0;
            do
            {
                int n = rnd.Next(0, 9);
                k = n / 3;
                h = n % 3;
                if (this.grid[k, h] == 0)
                {
                    this.grid[k, h] = 1;
                    this.gameOverCheck();
                    return;
                }
            } while (true);
        }

        public bool didPlayerWin()
        {
            return this.playerWon;
        }

        public bool isGameOver()
        {
            return this.gameOver;
        }
    }
}

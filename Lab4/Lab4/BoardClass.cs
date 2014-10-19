using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab4
{
    class BoardClass
    {
        Graphics g;
        int arraySize;
        int queensOnBoard;
        Cell[,] boardArray;
        Pen p;
        bool hintsOn;

        public BoardClass()
        {
            this.arraySize = 8;
            this.boardArray = new Cell[arraySize, arraySize];
            this.p = new Pen(Color.Black, 1);
            this.p.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            this.queensOnBoard = 0;
            this.hintsOn = false;
            initializeBoard();
        }

        public void setGraphics(Graphics g) {
            this.g = g;
        }


        public void toggleHints() {
            this.hintsOn = !this.hintsOn;
        }

        private bool isEven(int i) {
            return ((i % 2) == 0) ? true : false;
        }

        public void allDraw() {
            getHintBoard();
            for (int i = 0; i < this.arraySize; i++) {
                for (int j = 0; j < this.arraySize; j++) {
                    g.FillRectangle(boardArray[i, j].getColor(), boardArray[i, j].getRect());
                    g.DrawRectangle(p, boardArray[i, j].getRect());
                    if (boardArray[i, j].hasQueen()) {
                        StringFormat fmt = new StringFormat();
                        fmt.Alignment = StringAlignment.Center;
                        fmt.LineAlignment = StringAlignment.Center;
                        if (boardArray[i, j].isWhite()) {
                            g.DrawString("Q", new Font("Arial", 16), Brushes.Black, boardArray[i, j].getRect(),
                                fmt);
                        } else {
                            g.DrawString("Q", new Font("Arial", 16), Brushes.White, boardArray[i, j].getRect(),
                                fmt);
                        }
                    }
                }
            }
        }

        public void initializeBoard() {
            Console.WriteLine("In initializeBoard()");
            int X = 100;
            int Y = 100;
            for (int i = 0; i < this.arraySize; i++) {
                X = 100;
                for (int j = 0; j < this.arraySize; j++) {
                    Rectangle r = new Rectangle(X, Y, 50, 50);
                    boardArray[i, j] = new Cell();
                    if (isEven(i)) {
                        if (isEven(j)) {
                            boardArray[i, j].setRect(r);
                            boardArray[i, j].setWhite();
                        } else {
                            boardArray[i, j].setRect(r);
                            boardArray[i, j].setBlack();
                        }
                    } else {
                        if (isEven(j)) {
                            boardArray[i, j].setRect(r);
                            boardArray[i, j].setBlack();
                        } else {
                            boardArray[i, j].setRect(r);
                            boardArray[i, j].setWhite();
                        }
                    }
                    X += 50;
                }
                Y += 50;
            }
        }

        public void getHintBoard() {
            if (hintsOn) {
                for (int i = 0; i < arraySize; i++) {
                    for (int j = 0; j < arraySize; j++) {
                        if (!isSafe(i, j)) {
                            boardArray[i, j].setRed();
                        } else {
                            boardArray[i, j].setNotRed();
                        }
                    }
                }

            }
        }

        

        public int[] getSquare(int x, int y) {
            x -= 100; y -= 100;
            int column = (x / 50);
            int row = (y / 50);
            Console.WriteLine("x is " + x + " column is " + column);
            Console.WriteLine("y is " + y + " row is " + row);
            if (0 <= row && row < 8 && 0 <= column && column < 8) {
                return new int[] {column, row};
            } else {
                return new int[] {-1, -1};
            }
        }

        public void clearBoard() {
            this.queensOnBoard = 0;
            for (int i = 0; i < arraySize; i++) {
                for (int j = 0; j < arraySize; j++) {
                    boardArray[i, j].removeQueen();
                }
            }
        }

        public void clickOnBoard(int x, int y, bool leftClick) {
            Console.WriteLine("clickOnBoard");
            int[] box = getSquare(x, y);
            int column = box[0], row = box[1];
            if (column != -1) { // Means they clicked on the board
                if (boardArray[column, row].hasQueen()) {
                    if (leftClick) {
                        Console.WriteLine("Pre existing");
                        System.Media.SystemSounds.Beep.Play();
                    } else {
                        Console.WriteLine("Remove Queen");
                        queensOnBoard--;
                        boardArray[column, row].removeQueen();
                    }
                } else {
                    if (isSafe(column, row)) {
                        Console.WriteLine("Safe");
                        if (queensOnBoard < 8) {
                            Console.WriteLine("Draw Queen");
                            queensOnBoard++;
                            boardArray[column, row].addQueen();
                            if (queensOnBoard == 8) {
                                // Display success message
                            }
                        }
                    } else {
                        Console.WriteLine("4th case");
                        System.Media.SystemSounds.Beep.Play();
                    }
                }
            } else {
                Console.WriteLine("-1, -1");
            }
        }

        public bool isSafe(int column, int row) {
            for (int i = 0; i <= column; i++)
            {
                if (boardArray[i, row].hasQueen() || (i <= row && boardArray[column - i, row - i].hasQueen()) || (row + i < 8 && boardArray[column - i, row + i].hasQueen()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

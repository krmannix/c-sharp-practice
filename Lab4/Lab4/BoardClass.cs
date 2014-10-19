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

        public int numQueens() {
            return this.queensOnBoard;
        }

        public void toggleHints() {
            this.hintsOn = !this.hintsOn;
        }

        private bool isEven(int i) {
            return ((i % 2) == 0) ? true : false;
        }

        public void allDraw() {
            getSafeBoxes();
            getHintBoard();
            for (int i = 0; i < this.arraySize; i++) {
                for (int j = 0; j < this.arraySize; j++) {
                    g.FillRectangle(boardArray[i, j].getColor(), boardArray[i, j].getRect());
                    g.DrawRectangle(p, boardArray[i, j].getRect());
                    if (boardArray[i, j].hasQueen()) {
                        StringFormat fmt = new StringFormat();
                        fmt.Alignment = StringAlignment.Center;
                        fmt.LineAlignment = StringAlignment.Center;
                        if (boardArray[i, j].isWhite() || hintsOn) {
                            
                            g.DrawString("Q", new Font("Arial", 30), Brushes.Black, boardArray[i, j].getRect(),
                                fmt);
                        } else {
                            g.DrawString("Q", new Font("Arial", 30), Brushes.White, boardArray[i, j].getRect(),
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

        public int[] getSquare(int x, int y) {
            x -= 100; y -= 100;
            int column = (x / 50);
            int row = (y / 50);
            if (0 <= row && row < 8 && 0 <= column && column < 8) {
                return new int[] {row, column};
            } else {
                return new int[] {-1, -1};
            }
        }

        public void clearBoard() {
            this.queensOnBoard = 0;
            for (int i = 0; i < arraySize; i++) {
                for (int j = 0; j < arraySize; j++) {
                    boardArray[i, j].removeQueen();
                    boardArray[i, j].setNotRed();
                }
            }
        }

        public void clickOnBoard(int x, int y, bool leftClick) {
            int[] box = getSquare(x, y);
            int column = box[0], row = box[1];
            if (column != -1) { // Means they clicked on the board
                if (boardArray[column, row].hasQueen()) {
                    if (leftClick) {
                        System.Media.SystemSounds.Beep.Play();
                    } else {
                        queensOnBoard--;
                        boardArray[column, row].removeQueen();
                    }
                } else if (!leftClick) {
                    // Ignore the right clicks
                } else {
                    if (boardArray[column, row].isSafe()) {
                        if (queensOnBoard < 8) {
                            queensOnBoard++;
                            boardArray[column, row].addQueen();
                            if (queensOnBoard == 8) {
                                // Display success message
                            }
                        }
                    } else {
                        System.Media.SystemSounds.Beep.Play();
                    }
                }
            } else {
                Console.WriteLine("-1, -1");
            }
        }

        public void getSafeBoxes() {
            for (int i = 0; i < this.arraySize; i++) {
                for (int j = 0; j < this.arraySize; j++) {
                    if (queenInRow(i) || queenInColumn(j) || queenInDiag1(i, j)
                        || queenInDiag2(i, j) || queenInDiag3(i, j) || queenInDiag4(i, j))
                    {
                        boardArray[i, j].setNotSafe();
                    }
                    else
                    {
                        boardArray[i, j].setSafe();
                    }
                }
            }
        }

        // All the queen checking methods are below

        public bool queenInRow(int i) {
            for (int j = 0; j < this.arraySize; j++) {
                if (boardArray[i, j].hasQueen()) {
                    return true;
                }
            }
            return false;
        }

        public bool queenInColumn(int j) {
            for (int i = 0; i < this.arraySize; i++) {
                if (boardArray[i, j].hasQueen()) {
                    return true;
                }
            }
            return false;
        }

        public bool queenInDiag1(int i, int j) {
            while (i-- > 0 && j-- > 0) {
                if (boardArray[i, j].hasQueen()) return true;
            }
            return false;
        }

        public bool queenInDiag2(int i, int j) {
            while (i-- > 0 && j++ < 7)
            {
                if (boardArray[i, j].hasQueen()) return true;
            }
            return false;
        }

        public bool queenInDiag3(int i, int j)
        {
            while (j-- > 0 && i++ < 7)
            {
                if (boardArray[i, j].hasQueen()) return true;
            }
            return false;
        }

        public bool queenInDiag4(int i, int j)
        {
            while (i++ < 7 && j++ < 7)
            {
                if (boardArray[i, j].hasQueen()) return true;
            }
            return false;
        }

        public void getHintBoard() {
            if (hintsOn) {
                for (int i = 0; i < arraySize; i++) { 
                    for (int j = 0; j < arraySize; j++) {
                        if (!boardArray[i, j].isSafe()) {
                            boardArray[i, j].setRed();
                        } else {
                            boardArray[i, j].setNotRed();
                        }
                    }
                }
            }
            else {
                for (int i = 0; i < arraySize; i++) {
                    for (int j = 0; j < arraySize; j++) {

                         boardArray[i, j].setNotRed();
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab4
{
    class Cell
    {
        bool queen;
        bool red;
        bool white;
        Rectangle rect;
        bool safe;

        public Cell() {
            this.white = false;
            this.red = false;
            this.safe = true;
        }

        public Brush getColor() {
            if (red) return Brushes.Red;
            else if (white) return Brushes.White;
            else return Brushes.Black;
        }

        public void setRect(Rectangle r) {
            this.rect = r;
        }

        public Rectangle getRect() {
            return this.rect;
        }

        public bool isWhite() {
            return this.white;
        }

        public void setWhite() {
            this.white = true;
            this.red = false;
        }

        public void setBlack() {
            this.white = false;
            this.red = false;
        }

        public void setRed() {
            this.red = true;
        }

        public void setNotRed() {
            this.red = false;
        }

        public void addQueen() {
            this.queen = true;
        }

        public void removeQueen() {
            this.queen = false;
        }

        public bool hasQueen() {
            return this.queen;
        }

        public void setSafe() {
            this.safe = true;
        }

        public void setNotSafe() {
            this.safe = false;
        }

        public bool isSafe() {
            return this.safe;
        }
    }
}

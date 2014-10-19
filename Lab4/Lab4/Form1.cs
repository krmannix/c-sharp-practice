using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        BoardClass board;
        public Form1()
        {
            InitializeComponent();
            this.board = new BoardClass();
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            this.board.setGraphics(g);
            this.board.allDraw();
            textBox1.Text = "You have " + this.board.numQueens() + " Queens on the board";
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Form 1");
            int x, y;
            x = e.X; y = e.Y;
            if (e.Button == MouseButtons.Left) {
                this.board.clickOnBoard(x, y, true);
            }
            else if (e.Button == MouseButtons.Right) {
                this.board.clickOnBoard(x, y, false);
            }
            this.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Clearing board");
            this.board.clearBoard();
            this.Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            board.toggleHints();
            this.Invalidate();
        }

    }
}

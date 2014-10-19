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
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Form 1");
            int x, y;
            x = e.X; y = e.Y;
            if (e.Button == MouseButtons.Left) {
                this.board.clickOnBoard(x, y, true);
            } else if (e.Button == MouseButtons.Right) {
                this.board.clickOnBoard(x, y, false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.board.clearBoard();
            this.Invalidate();
        }

    }
}

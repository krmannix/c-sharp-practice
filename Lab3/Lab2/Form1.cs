using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;  
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Lab2
{
    public partial class Form1 : Form
    {

        int Width = 10;
        private ArrayList locations = new ArrayList();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int x, y;
            x = e.X;
            y = e.Y;
            if (e.Button == MouseButtons.Left)
            {
                this.locations.Add(new PointInfo(x, y));
                this.Invalidate();
                this.Update();
            } else if (e.Button == MouseButtons.Right) {
                Boolean existingPoint = false;
                foreach (PointInfo p in this.locations) {
                    if ((x < p.getX() + Width/2) && (x > p.getX() - Width/2)) {
                        if ((y < p.getY() + Width/2) && (y > p.getY() - Width/2)) {
                            existingPoint = true;
                            if (p.getClear())
                            {
                                p.unClear();
                            } 
                            else if (p.getRed())
                            {
                                p.makeClear();
                            }
                            else
                            {
                                p.makeRed();
                            }
                        }
                    }
                }
                if (!existingPoint)
                {
                    this.locations.Clear();
                }
                this.Invalidate();
                this.Update();
            }
        }

     private void button1_Click(object sender, EventArgs e)
        {
            this.locations.Clear();
            this.Invalidate();
        }

     private void clearToolStripMenuItem_Click(object sender, EventArgs e)
     {
         this.locations.Clear();
         this.Invalidate();
     }

     private void Form1_Paint(object sender, PaintEventArgs e)
     {
         Graphics g = e.Graphics;
         foreach (PointInfo p in this.locations)
         {
             if (!p.getClear())
             {
                 Brush br = Brushes.Black;
                 if (p.getRed())
                 {
                     br = Brushes.Red;
                 }
                 g.FillEllipse(br, p.getX() - Width / 2, p.getY() - Width / 2, Width, Width);
             }
         }
     }
    }
}

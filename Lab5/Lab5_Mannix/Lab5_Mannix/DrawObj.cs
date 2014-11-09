using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab5_Mannix
{
    class DrawObj
    {

        public DrawObj()
        {

        }

        public virtual void Draw(Graphics g)
        {

        }

        public System.Drawing.Rectangle getRectangle(Point a, Point b)
        {
            int topLeftX, topLeftY;
            if (a.X > b.X)
            {
                if (a.Y > b.Y)
                {
                    topLeftX = (int) b.X ;
                    topLeftY = (int) b.Y ;
                }
                else
                {
                    topLeftX = (int) b.X ;
                    topLeftY = (int) a.Y;
                }
            }
            else
            {
                if (a.Y >  b.Y)
                {
                    topLeftX = (int)a.X;
                    topLeftY = (int)b.Y ;
                }
                else
                {
                    topLeftX = (int) a.X;
                    topLeftY = (int) a.Y;
                }
            }
            int width = (int) Math.Abs(Math.Round((double)( b.X - a.X )));
            int height = (int) Math.Abs(Math.Round((double)( b.Y - a.Y )));
            return new System.Drawing.Rectangle(topLeftX, topLeftY, width, height);
        }

        public System.Drawing.RectangleF getRectangleF(Point a, Point b)
        {
            int topLeftX, topLeftY;
            if (a.X > b.X)
            {
                if (a.Y > b.Y)
                {
                    topLeftX = (int) b.X ;
                    topLeftY = (int) b.Y ;
                }
                else
                {
                    topLeftX = (int) b.X ;
                    topLeftY = (int) a.Y;
                }
            }
            else
            {
                if (a.Y >  b.Y)
                {
                    topLeftX = (int)a.X;
                    topLeftY = (int)b.Y ;
                }
                else
                {
                    topLeftX = (int)a.X;
                    topLeftY = (int) a.Y;
                }
            }
            int width = (int) Math.Abs(Math.Round((double)( b.X - a.X )));
            int height = (int) Math.Abs(Math.Round((double)( b.Y - a.Y )));
            return new System.Drawing.RectangleF(topLeftX, topLeftY, width, height);
        }

    }

    class Line : DrawObj
    {
        public Point a, b;
        public Pen p;

        public Line(Point a, Point b, Pen p)
        {
            this.a = a;
            this.b = b;
            this.p = p;
        }

        public override void Draw(Graphics g)
        {
            g.DrawLine(p, a, b);
        }
    }

    class Rectangle : DrawObj
    {

        Point a, b;
        Pen p;
        SolidBrush sb;
        public Rectangle(Point a, Point b, Pen p, SolidBrush sb)
        {
            this.a = a;
            this.b = b;
            this.p = p;
            this.sb = sb;
        }

        public override void Draw(Graphics g)
        {
            if (sb != null)
            {
                g.FillRectangle(this.sb, getRectangle(this.a, this.b));
            }
            if (p != null)
            {
                g.DrawRectangle(this.p, getRectangle(this.a, this.b));
            }
        }
    }

    class Ellipse : DrawObj
    {
        Point a, b;
        Pen p;
        SolidBrush sb;
        public Ellipse(Point a, Point b, Pen p, SolidBrush sb)
        {
            this.a = a;
            this.b = b;
            this.p = p;
            this.sb = sb;
        }

        public override void Draw(Graphics g)
        {
            if (sb != null)
            {
                g.FillEllipse(this.sb, getRectangle(this.a, this.b));
            }
            if (p != null)
            {
                g.DrawEllipse(this.p, getRectangle(this.a, this.b));
            }
        }
    }

    class Text : DrawObj
    {
        Brush sb;
        String t;
        Point a, b;
        public Text(Point a, Point b, String t, Brush sb)
        {
            this.t = t;
            this.sb = sb;
            this.a = a;
            this.b = b;
        }

        public override void Draw(Graphics g)
        {
            Console.WriteLine("This is the text: " + this.t);
            g.DrawString(this.t, new System.Drawing.Font("Arial", 16),
                        this.sb, getRectangleF(this.a, this.b));
        }
    }


}

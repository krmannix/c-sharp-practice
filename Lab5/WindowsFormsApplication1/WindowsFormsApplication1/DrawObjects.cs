using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{


    class DrawObjects
    {
        float x1, x2, y1, y2;
        int type;
        int penState, fillState, drawState, penWidth;
        bool fill, outline;
        String text;

        public DrawObjects(float x1, float x2, float y1, float y2, int penState, int fillState, 
            int drawState, int penWidth, String text, bool fill, bool outline)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
            this.penState = penState;
            this.fillState = fillState;
            this.drawState = drawState;
            this.penWidth = penWidth;
            this.text = text;
            this.fill = fill;
            this.outline = outline;
        }

        private System.Drawing.Rectangle getRectangle()
        {
            int topLeftX, topLeftY;
            if (this.x1 > this.x2)
            {
                if (this.y1 > this.y2)
                {
                    topLeftX = (int) this.x1;
                    topLeftY = (int) this.y1;
                }
                else
                {
                    topLeftX = (int)this.x1;
                    topLeftY = (int)this.y2;
                }
            }
            else
            {
                if (this.y1 > this.y2)
                {
                    topLeftX = (int)this.x2;
                    topLeftY = (int)this.y1;
                }
                else
                {
                    topLeftX = (int)this.x2;
                    topLeftY = (int)this.y2;
                }
            }
            int width = (int) Math.Abs(Math.Round(this.x2 - this.x1));
            int height = (int) Math.Abs(Math.Round(this.y2 - this.y1));
            return new System.Drawing.Rectangle(topLeftX, topLeftY, width, height);
        }

        private System.Drawing.RectangleF getRectangleF()
        {
            int topLeftX, topLeftY;
            if (this.x1 > this.x2)
            {
                if (this.y1 > this.y2)
                {
                    topLeftX = (int)this.x1;
                    topLeftY = (int)this.y1;
                }
                else
                {
                    topLeftX = (int)this.x1;
                    topLeftY = (int)this.y2;
                }
            }
            else
            {
                if (this.y1 > this.y2)
                {
                    topLeftX = (int)this.x2;
                    topLeftY = (int)this.y1;
                }
                else
                {
                    topLeftX = (int)this.x2;
                    topLeftY = (int)this.y2;
                }
            }
            int width = (int)Math.Abs(Math.Round(this.x2 - this.x1));
            int height = (int)Math.Abs(Math.Round(this.y2 - this.y1));
            return new System.Drawing.RectangleF(topLeftX, topLeftY, width, height);
        }

        public void Draw(System.Drawing.Graphics g)
        {
            System.Drawing.Color penColor = System.Drawing.Color.Black;
            System.Drawing.Color fillColor = System.Drawing.Color.Black;
            // Get pen Color
            switch (this.penState)
            {
                case 0:
                    penColor = System.Drawing.Color.Black;
                    break;
                case 1:
                    penColor = System.Drawing.Color.Red;
                    break;
                case 2:
                    penColor = System.Drawing.Color.Blue;
                    break;
                case 3:
                    penColor = System.Drawing.Color.Green;
                    break;
                default:
                    penColor = System.Drawing.Color.Black;
                    break;
            }
            // Fill color
            switch (fillState)
            {
                case 0:
                    fillColor = System.Drawing.Color.White;
                    break;
                case 1:
                    fillColor = System.Drawing.Color.Black;
                    break;
                case 2:
                    fillColor = System.Drawing.Color.Red;
                    break;
                case 3:
                    fillColor = System.Drawing.Color.Blue;
                    break;
                case 4:
                    fillColor = System.Drawing.Color.Green;
                    break;
                default:
                    fillColor = System.Drawing.Color.Black;
                    break;
            }

            System.Drawing.Pen pen = new System.Drawing.Pen(penColor, penWidth);
            switch (drawState)
            {
                case 0:
                    g.DrawLine(pen, x1, y1, x2, y2);
                    break;
                case 1:
                    if (fill) g.FillRectangle(new System.Drawing.SolidBrush(fillColor), getRectangle());
                    if (outline) g.DrawRectangle(pen, getRectangle());
                    break;
                case 2:
                    if (fill) g.FillRectangle(new System.Drawing.SolidBrush(fillColor), getRectangle());
                    if (outline) g.DrawEllipse(pen, getRectangle());
                    break;
                case 3:
                    g.DrawString(text, new System.Drawing.Font("Arial", 16),
                        new System.Drawing.SolidBrush(penColor), getRectangleF());
                    
                    break;
                default:
                    Console.WriteLine("Draw State is invalid");
                    break;
            }
        }

    }
}

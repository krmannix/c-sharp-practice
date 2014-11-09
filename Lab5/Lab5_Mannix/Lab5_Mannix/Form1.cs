using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5_Mannix
{
    public partial class Form1 : Form
    {
        int drawState = 0;
        int penState = 0;
        int fillState = 0;
        int penWidth = 1;
        bool fill = false;
        bool outline = false;
        String text = "";
        float x1 = -1, y1 = -1;
        float x2, y2;
        System.Collections.Generic.List<DrawObj> drawObjs = new System.Collections.Generic.List<DrawObj>();

        // The two points that determine the position of the object
        private Point pointA, pointB;
        Boolean firstClick = false;

        public Form1()
        {
            InitializeComponent();
            drawPanel.Refresh();
            listBox1.SetSelected(0, true);
            listBox2.SetSelected(0, true);
            listBox3.SetSelected(0, true);
            lineButton.Select();
        }

        private void drawPanel_Paint(object sender, PaintEventArgs e)
        {
            // Graphics object g
            Graphics g = e.Graphics;

            // Draw each graphics element
            foreach (DrawObj drawObj in this.drawObjs)
            {
                drawObj.Draw(g);
            }

        }

        private void drawPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (firstClick)
            {
                firstClick = false; // reset first click for next object
                pointB = new Point(e.X, e.Y);

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
                SolidBrush brush = null;
                switch (drawState)
                {
                    case 0:
                        drawObjs.Add(new Line(pointA, pointB, pen));
                        break;
                    case 1:
                        if (fill) {
                            brush = new System.Drawing.SolidBrush(fillColor);
                        }
                        if (!outline)
                        {
                            pen = null;
                        }
                        if (pen == null && brush == null) { }
                        else { drawObjs.Add(new Rectangle(pointA, pointB, pen, brush)); }
                        break;
                    case 2:
                        if (fill) {
                            brush = new System.Drawing.SolidBrush(fillColor);
                        }
                        if (!outline)
                        {
                            pen = null;
                        }
                        if (pen == null && brush == null) { }
                        else { drawObjs.Add(new Ellipse(pointA, pointB, pen, brush)); }
                        break;
                    case 3:
                        drawObjs.Add(new Text(pointA, pointB, text, new System.Drawing.SolidBrush(penColor)));
                        break;
                    default:
                        this.Invalidate();
                        break;
                }

                this.Invalidate();
            }
            // if this is the first click, record for start pointA, set firstClick to true to draw
            else
            {
                pointA = new Point(e.X, e.Y);
                firstClick = true;
                this.Invalidate();
            }

            drawPanel.Refresh();
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            // This is the event handler for all radio buttons in the group
            if (lineButton.Checked) drawState = 0;
            if (recButton.Checked) drawState = 1;
            if (ellipseButton.Checked) drawState = 2;
            if (textButton.Checked) drawState = 3;

            this.Invalidate();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawObjs.Clear();
            drawPanel.Invalidate();
            this.Invalidate();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drawObjs.Count > 0)
            {
                drawObjs.RemoveAt(drawObjs.Count - 1);
            }
            drawPanel.Refresh();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            penState = listBox1.SelectedIndex;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillState = listBox2.SelectedIndex;
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            penWidth = listBox3.SelectedIndex + 1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Text changeddd");
            Console.WriteLine(textBox1.Text);
            text = textBox1.Text;
        }

        // Fill check box
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb != null)
            {
                if (cb.Checked)
                {
                    fill = true;
                }
                else
                {
                    fill = false;
                }
            }
            else
            {
                fill = false;
            }
        }

        // Outline check box
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb != null)
            {
                if (cb.Checked)
                {
                    outline = true;
                }
                else
                {
                    outline = false;
                }
            }
            else
            {
                fill = false;
            }
        }


    }
}

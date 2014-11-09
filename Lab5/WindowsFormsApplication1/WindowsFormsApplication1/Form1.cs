using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
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
        System.Collections.ArrayList drawObjs = new System.Collections.ArrayList();


        public Form1()
        {
            InitializeComponent();
            // Initialize default values
            listBox1.SetSelected(0, true);
            listBox2.SetSelected(0, true);
            listBox3.SetSelected(0, true);
        }

        

        /* * * * * * * * * * * * * * * * * * * * * *
         * 
         *  All event listeners
         * 
         * * * * * * * * * * * * * * * * * * * * * */

        

        // Pen color
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            penState = listBox1.SelectedIndex;
        }

        // Fill color
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillState = listBox2.SelectedIndex;
        }

        // Pen width
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            penWidth = listBox3.SelectedIndex + 1;
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

        // Line Radio
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    drawState = 0;
                }
            }
        } 

        // Rectangle Radio
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    drawState = 1;
                }
            }
        }

        // Elipse Radio
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    drawState = 2;
                }
            }
        }

        // Text Radio
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    drawState = 3;
                }
            }
        }

        // Textbox Input
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                text = textBox.Text;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawObjs.Clear();
            Invalidate();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawObjs.RemoveAt(drawObjs.Count - 1);
            Invalidate();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (x1 < 0)
            {
                x1 = MousePosition.X;
                y1 = MousePosition.Y;
            }
            else
            {
                Console.WriteLine("CLIKED TWICE");
                x2 = MousePosition.X;
                y2 = MousePosition.Y;
                drawObjs.Add(new DrawObjects(x1, y1, x2, y2, penState, fillState, drawState, penWidth, text, fill, outline));
                x1 = -1;
                y1 = -1;
                Invalidate();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Console.WriteLine("Painting");
            foreach (DrawObjects objs in drawObjs)
            {
                objs.Draw(e.Graphics);
            }
        }

        
    }
}

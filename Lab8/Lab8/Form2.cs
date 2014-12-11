using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8
{
    public partial class Form2 : Form
    {

        ArrayList files;
        int interval;
        System.Windows.Forms.Timer timer1;


        public Form2(ArrayList files, int interval)
        {
            InitializeComponent();
            this.files = files;
            this.interval = interval;
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000 * this.interval;
            timer1.Tick += new EventHandler(timer1_Tick);
            this.showPictures();
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            // Picture needs to change here. If there are no more pictures to remove from array list, exit
            if (this.files.Count == 0)
            {
                timer1.Stop();
                this.Close();
            }
            else
            {
                String filePath = (String)this.files[0];
                this.files.RemoveAt(0);
                pictureBox1.Dock = DockStyle.Fill;
                setPictureBox(filePath);
            }
        }

        private void showPictures()
        {
            // Set up first picture
            String filePath = (String) this.files[0];
            this.files.RemoveAt(0);
            pictureBox1.Dock = DockStyle.Fill;
            setPictureBox(filePath);
            timer1.Start();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void setPictureBox(String fileName)
        {
            pictureBox1.Image = new Bitmap(fileName);
        }
    }
}

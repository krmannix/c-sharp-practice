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
using System.IO;

namespace Lab8
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // Get the dialog and add to the listbox
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) listBox1.Items.Add(openFileDialog1.FileName);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            // Delete all selected files
            for (int i = listBox1.SelectedIndices.Count - 1; i >= 0; i--) 
                listBox1.Items.RemoveAt(listBox1.SelectedIndices[i]);
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
                MessageBox.Show("No images to show.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                int interval = checkInterval();
                if (interval > 0)
                {
                    // Go through strings
                    ArrayList files = new ArrayList();
                    foreach (string s in listBox1.Items)
                    {
                        files.Add(s);
                    }
                    Form2 form = new Form2(files, interval) { Owner = this };
                    form.ShowDialog();
                }
            }
        }

        private void openCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            // Then add all the items
            openFileDialog1.Filter = "*.pix|*.pix";
            openFileDialog1.FilterIndex = 2;

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                String fileName = openFileDialog1.FileName;
                String line;
                try
                {
                    StreamReader file = new StreamReader(fileName);
                    while ((line = file.ReadLine()) != null)
                    {
                        listBox1.Items.Add(line);
                    }
                    file.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("File does not exist.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
                MessageBox.Show("No files to save.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else {
                saveFileDialog1.Filter = "*.pix|*.pix";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    String filePath = getSaveExtension();
                    String file = "";
                    foreach (string line in listBox1.Items)
                    {
                        file += line + "\n";
                    }
                    try
                    {
                        using (Stream s = File.Open(filePath, FileMode.CreateNew))
                        using (TextWriter sw = new StreamWriter(s))
                        {
                            sw.Write(file);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("File does not exist.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private int checkInterval()
        {
            int x = 0;

            if (Int32.TryParse(intervalInput.Text, out x))
            {
                if (x <= 0) {
                    MessageBox.Show("Please enter an integer time interval > 0.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return x;
            }
            else
            {
                MessageBox.Show("Please enter an integer time interval > 0.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private String getSaveExtension()
        {
            String filename = saveFileDialog1.FileName;
            Console.WriteLine("filename is " + filename);
            if (Path.HasExtension(filename))
            {
                if (Path.GetExtension(filename) != ".pix")
                {
                    Console.WriteLine("Wrong Extension");
                    Path.ChangeExtension(filename, ".pix");
                }
            }
            else
            {
                Console.WriteLine("No extension");
                Path.ChangeExtension(filename, ".pix");
            }
            Console.WriteLine("Returning");
            return filename;
        }
    }
}

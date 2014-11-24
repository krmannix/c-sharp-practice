using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab7
{
    public partial class Form1 : Form
    {
        String keyText = "";
        String filePathText = "";

        public Form1()
        {
            InitializeComponent();
        }

        /* * * * * * * * * * * * * * *
         * 
         * Button Clicks
         * 
         * * * * * * * * * * * * * * */

        private void encryptButton_Click(object sender, EventArgs e)
        {
            if(textInputsValid()) {

            }
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            if (textInputsValid()) {

            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK)  {
                string file = openFileDialog1.FileName;
                try {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                    fileNameInput.Text = openFileDialog1.FileName;
                }
                catch (Exception) {
                }
            }
        }

        /* * * * * * * * * * * * * * *
         * 
         * Text Change
         * 
         * * * * * * * * * * * * * * */

        private void keyInput_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                keyText = textBox.Text;
            }
        }

        private void fileNameInput_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                filePathText = textBox.Text;
            }
        }

        /* * * * * * * * * * * * * * *
         * 
         * Helper Methods
         * 
         * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * *
         * 
         * Correctness Checks
         * 
         * * * * * * * * * * * * * * */

        private bool textInputsValid()
        {
            if (filePathText == "")
            {
                MessageBox.Show("Please enter a file path.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1); 
                return false;
            }
            else if (keyText == "")
            {
                MessageBox.Show("Please enter a key.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                return false;
            }
            else return true;
            
        }

        private bool checkPathExists()
        {
            if (File.Exists(filePathText)) { return true; }
            MessageBox.Show("File does not exist.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            return false;

        }

        private bool checkForDESFileExtension()
        {
            if (Path.GetExtension(filePathText) != ".des")
            {
                MessageBox.Show("Cennot decrypt non-.des file.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1); 
                return false;
            }
            else return true;
        }

        private bool checkForOverwriteFile()
        {
            if (File.Exists(Path.ChangeExtension(filePathText, null)))
            {
                var result = MessageBox.Show("File already exists. Overwrite?",
                    "Error",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes) return true;
                else return false;
            }
            return true;
        }

    }
}
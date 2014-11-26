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
using System.Security.Cryptography;

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
            if (textInputsValid() && checkPathExists() && checkForOverwriteFile(true))
            {
                try
                {
                    string filePath = filePathText;
                    string text = File.ReadAllText(filePath);
                    int size = text.Length;
                    byte[] key = createByteArray();
                    byte[] encrypted = EncryptStringToBytes(text, key, key);
                    saveEncryptedFile(encrypted, filePath);
                    //resetTextBoxes();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("Could not open source of destination file.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                }
                
            }
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            if (checkForDESFileExtension() && textInputsValid() && checkPathExists() && checkForOverwriteFile(false)                    )
            {
                try
                {
                    string filePath = filePathText;
                    byte[] key = createByteArray();
                    String de = DecryptStringFromBytes(readEncryptedFile(filePath), key, key);
                    saveDecryptedFile(de, filePath);
                    //resetTextBoxes();
                }
                catch (Exception c)
                {
                    Console.WriteLine(c.ToString());
                    MessageBox.Show("Could not open source of destination file.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK)  {
                string file = openFileDialog1.FileName;
                try {
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

        private void resetTextBoxes()
        {
            fileNameInput.Text = "";
            keyInput.Text = "";
        }

        private byte[] createByteArray()
        {
            byte[] byteArr = new byte[8];
            char[] key_temp = this.keyText.ToCharArray();
            for (int i = 0; i < key_temp.Length; i++)
            {
                byte c = (byte) key_temp[i];
                byteArr[i % 8] = (byte) (byteArr[i % 8] + c); 
            }
            return byteArr;
        }

        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an TripleDESCryptoServiceProvider object 
            // with the specified key and IV. 
            using (DESCryptoServiceProvider tdsAlg = new DESCryptoServiceProvider())
            {
                tdsAlg.Key = Key;
                tdsAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = tdsAlg.CreateEncryptor(tdsAlg.Key, tdsAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream. 
            return encrypted;

        }

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an TripleDESCryptoServiceProvider object 
            // with the specified key and IV. 
            using (DESCryptoServiceProvider tdsAlg = new DESCryptoServiceProvider())
            {
                tdsAlg.Key = Key;
                tdsAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = tdsAlg.CreateDecryptor(tdsAlg.Key, tdsAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        private void saveEncryptedFile(byte[] bArr, String path)
        {
            String newFilePath = path + ".des";
            File.WriteAllBytes(newFilePath, bArr);
        }

        private void saveDecryptedFile(String file, String path)
        {
            path = path.Remove(path.Length - 3); // remove .des
            File.WriteAllText(path, file);
        }

        private byte[] readEncryptedFile(String path)
        {
            return File.ReadAllBytes(path);
        }

        /* * * * * * * * * * * * * * *
         * 
         * Correctness Checks
         * 
         * * * * * * * * * * * * * * */

        private bool textInputsValid()
        {
            if (this.keyText == "")
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
            if (File.Exists(this.filePathText)) { return true; }
            MessageBox.Show("Could not open source of destination file.",
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
                MessageBox.Show("Not a .des file.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1); 
                return false;
            }
            else return true;
        }

        private bool checkForOverwriteFile(bool addDES)
        {
            String filePath;
            if (addDES) filePath = this.filePathText + ".des";
            else filePath = this.filePathText.Remove(this.filePathText.Length - 3);
            if (File.Exists(filePath))
            {
                var result = MessageBox.Show("Output file exists. Overwrite?",
                    "Error",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes) return true;
                else return false;
            }
            return true;
        }

    }
}
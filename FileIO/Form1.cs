using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace FileIO
{
    public partial class FileEncrypter : Form
    {
        public FileEncrypter()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.InitialDirectory = GetFilePath();
            file.Filter = "Text File|*.txt";

            if(file.ShowDialog() == DialogResult.OK)
            {
                rtbFile.Clear();
                StringBuilder data = new StringBuilder();
                data.Append(File.ReadAllText(file.FileName).ToString());

                rtbFile.AppendText(data.ToString());
            }
        }

        private string GetFilePath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            StringBuilder contents = new StringBuilder();
            contents.AppendLine(rtbFile.Text);

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Data File|*.txt";
            save.RestoreDirectory = true;

            if(save.ShowDialog() == DialogResult.OK)
            {
                string path = save.FileName;
                if (path == string.Empty)
                    return;
                File.WriteAllText(path, contents.ToString());
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            StringBuilder contents = new StringBuilder();
            contents.Append(rtbFile.Text);
            string contentsToString = contents.ToString();
            rtbFile.Clear();
            rtbFile.Text = RijndaelSimple.Decrypt(contentsToString, "Bananas", "LeagueOfLegends", "SHA1", 2, "1234567890123456", 192);

        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            StringBuilder contents = new StringBuilder();
            contents.Append(rtbFile.Text);
            string contentsToString = contents.ToString();
            rtbFile.Clear();
            rtbFile.Text = RijndaelSimple.Encrypt(contentsToString, "Bananas", "LeagueOfLegends", "SHA1", 2, "1234567890123456", 192);
        }
    }
}

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

namespace IMSEnterprise
{
    public partial class PreviewCurrentLogFile : Form
    {
        private String text;
        public PreviewCurrentLogFile(String filePath)
        {
            InitializeComponent();
            try
            {
                this.richTextBox1.Text = File.ReadAllText(filePath);
                this.text = File.ReadAllText(filePath);

                this.textBox1.Text = filePath;

                FileInfo f = new FileInfo(filePath);
                this.Text = f.Name;
            }
            catch(Exception e)
            {
                MessageBox.Show("Log-file was not found! Message: " + e.Message);
            }
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.text);
        }
    }
}
